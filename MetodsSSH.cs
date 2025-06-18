using System.Dynamic;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Esf;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Org.BouncyCastle.Tls;
using Renci.SshNet;

namespace SSHProject;

public class MethodsSSH
{
    const string Login = "braws"; //Константа для логина
    const string Password = "1234567890"; //Константа для пароля
    const string Path = "shopt -s globstar && cd ~/**/LinuxMonitor/LinuxMonitor/LinuxMonitor && dotnet run"; //Константа для пути запуска программы
    public void SSHConnectAllServers() //Подключение ко всем серверам
    {
        List<Server> serverList = GetServersFromDataBase(); //Получение серверов из базы

        Parallel.ForEach(serverList, serverInformation => //Паралельный вызов для каждого из серверов
        {
            SSHConnect(serverInformation.IdServer, serverInformation.IpAdress, Login, //Подключение к серверам
            Password);
        });
    }

    private List<Server> GetServersFromDataBase()
    {
        using (var serverList = new PostgresContext())
        {
            return serverList.Servers.ToList(); //Получение серверов из базы
        }
    }

    private void SSHConnect(Guid idServer, string ip, string login, string password)
    {

        PostgresContext pc = new PostgresContext();

        var server = pc.Servers.First(x => x.IdServer == idServer);

        using (var serverConnect = new SshClient(ip, login, password)) //Подключение к серверу
        {
            string connectMessage = ""; //Для вывода ошибок подключения
            try
            {
                serverConnect.Connect(); //Попытка подключения
            }
            catch (Exception ex)
            {
                connectMessage = ex.Message; //Сообщение при ошибке подключения
            }
            if (serverConnect.IsConnected)
            {
                bool searchInProblemConnect = pc.Problems.       //Поиск ошибки подключения этого сервера
                Where(x => x.IdServer == server.IdServer)
                .Include(x => x.IdServerNavigation)
                .Any(x => x.StatusProblem == false && x.IdServerNavigation.ServerStatus == false);

                if (searchInProblemConnect)
                {
                    var problem = pc.Problems.Include(x => x.IdServerNavigation).First(x => x.IdServer == server.IdServer //Поиск этой проблемы
                    && x.IdServerNavigation.ServerStatus == false
                    && x.StatusProblem == false
                    && !x.MessageProblem.Contains("RAM")
                    && !x.MessageProblem.Contains("MEMORY")
                    && !x.MessageProblem.Contains("CPU")
                    );
                    ProblemSolving(pc, problem, "Удалось установить подключение"); //Решение проблемы
                }

                ServerStatusUdpate(pc, server, true); //Смена статуса на активированный

                var cmd = serverConnect.CreateCommand(Path); //Ввод команды в CMD
                string[] result = cmd.Execute().Split("\n"); //Получение данных из CMD

                string[] ramUsed = new string[2]; //Получение информации о загруженности оперативной памяти (RAM)
                string[] ramUsedParameters = new string[2]; //Разделение числовых значений
                double ramUsedParameter = 0; //Объем занятой оперативной памяти (RAM)
                double ramUsedParameterMax = 0; //Максимальный объем оперативной памяти (RAM)

                double ramUsedPercent = 0; //Получение процента занятости оперативной памяти (RAM)

                string[] cpuUsed = new string[2]; //Получение информации о загруженности центрального процессора (CPU)
                double cpuUsageParameter = 0; //Процент загруженности центрального процессора (CPU)

                string[] diskUsage = new string[2]; //Получение информации о загруженности диска
                double diskUsageParameter = 0; //Процент загруженности диска

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i].Contains("[MEMORY]"))
                    {
                        ramUsed = result[i].Split(": "); //Получение информации о загруженности оперативной памяти (RAM)
                        ramUsedParameters = ramUsed[1].Split(" / "); //Разделение числовых значений
                        ramUsedParameter = Convert.ToDouble(
                           ramUsedParameters[0].Trim(new char[] { 'G', 'i', ' ' })); //Объем занятой оперативной памяти (RAM)
                        ramUsedParameterMax = Convert.ToDouble(
                            ramUsedParameters[1].Trim(new char[] { 'G', 'i', ' ' })); //Максимальный объем оперативной памяти (RAM)
                        ramUsedPercent = ramUsedParameter / ramUsedParameterMax * 100; //Получение процента занятости оперативной памяти (RAM)
                    }
                    else if (result[i].Contains("[STORAGE]"))
                    {
                        diskUsage = result[i].Split(": "); //Получение информации о загруженности центрального процессора (CPU)
                        diskUsageParameter = Convert.ToDouble( //Процент загруженности центрального процессора (CPU)
                             diskUsage[1].Trim(new char[] { '%' }));
                    }
                    else if (result[i].Contains("[CPU]"))
                    {
                        cpuUsed = result[i].Split(": "); //Получение информации о загруженности центрального процессора (CPU)
                        cpuUsageParameter = Convert.ToDouble( //Процент загруженности центрального процессора (CPU)
                            cpuUsed[1].Trim(new char[] { '%' }));
                    }
                }

                CurrentParameters(pc, server.IdServer, ramUsedPercent, cpuUsageParameter, diskUsageParameter); //Заполнение параметров серверов

                string message = Message(ramUsedPercent, cpuUsageParameter, diskUsageParameter, ramUsed[1]); //Получение сообщения о нагруженности

                bool searchInProblem = pc.Problems. //Проверка на проблемы этого сервера
                Where(x => x.IdServer == server.IdServer)
                .Any(x => x.StatusProblem == false);

                if (message != "" && !searchInProblem) //Отсутствие проблем этого сервера
                {
                    int ErrorImportance_ = ErrorImportance(ramUsedPercent, cpuUsageParameter, diskUsageParameter, pc); //Получение критичности проблемы
                    ProblemAdd(pc, server.IdServer, ErrorImportance_, message); //Добавление новой проблемы
                }
                else if (message != "" && searchInProblem) //Проблемы были и до этого
                {
                    int ErrorImportance_ = ErrorImportance(ramUsedPercent, cpuUsageParameter, diskUsageParameter, pc); //Получение критичности проблемы
                    var problem = pc.Problems.First( //Поиск этой проблемы
                        x => x.IdServer == server.IdServer &&
                        x.StatusProblem == false
                    );
                    ProblemUpdate(pc, problem, ErrorImportance_, message); //Изменение проблемы
                }
                else if (message == "" && searchInProblem && (ramUsedPercent >= 40 || diskUsageParameter >= 40 || cpuUsageParameter >= 40)) //Близкая к решению проблема
                {
                    message = MessageCloseSolution(ramUsedPercent, diskUsageParameter, cpuUsageParameter, ramUsed[1]); //Новое сообщение
                    int ErrorImportance_ = ErrorImportance(ramUsedPercent, cpuUsageParameter, diskUsageParameter, pc); //Получение критичности проблемы
                    var problem = pc.Problems.First( //Поиск этой проблемы
                        x => x.IdServer == server.IdServer &&
                        x.StatusProblem == false
                    );
                    ProblemUpdate(pc, problem, ErrorImportance_, message); //Изменение проблемы
                }
                else if (message == "" && searchInProblem && ramUsedPercent <= 40 && diskUsageParameter <= 40 && cpuUsageParameter <= 40) //Проблема решена
                {
                    var problem = pc.Problems.First( //Поиск этой проблемы
                        x => x.IdServer == server.IdServer &&
                        x.StatusProblem == false
                    );
                    ProblemSolving(pc, problem, "Проблема решена"); //Изменение статуса проблемы
                }
            }
            else
            {
                bool searchInProblemConnect = pc.Problems. //Поиск ошибки подключения этого сервера
                Where(x => x.IdServer == server.IdServer)
                .Include(x => x.IdServerNavigation)
                .Any(x => x.StatusProblem == false && x.IdServerNavigation.ServerStatus == false);

                ServerStatusUdpate(pc, server, false); //Смена статуса на деактивированный

                if (!searchInProblemConnect)
                {
                    ProblemAdd(pc, server.IdServer, 5, connectMessage); //Запись проблемы
                }
                else
                {
                    var problem = pc.Problems.Include(x => x.IdServerNavigation).First(x => x.IdServer == server.IdServer //Поиск проблемы
                    && x.IdServerNavigation.ServerStatus == false
                    && !x.MessageProblem.Contains("RAM")
                    && !x.MessageProblem.Contains("MEMORY")
                    && !x.MessageProblem.Contains("CPU")
                    );
                    ProblemUpdate(pc, problem, 5, connectMessage); //Обновление проблемы
                }
            }
            serverConnect.Disconnect(); //Отключение от сервера
        }
    }
    private void CurrentParameters(PostgresContext pc, Guid idServer_, double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter) //Заполнение параметров
    {
        try
        {
            Parameter newParameter = new Parameter()
            {
                CreatedAt = DateTime.Now,
                IdServer = idServer_,
                RamMb = Convert.ToInt32(Math.Round((decimal)ramUsedPercent, 0)),
                CpuPercent = Convert.ToInt32(Math.Round((decimal)cpuUsageParameter, 0)),
                RomMb = Convert.ToInt32(Math.Round((decimal)diskUsageParameter, 0))
            };
            pc.Parameters.Add(newParameter); //Добавление в бд
            pc.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private string Message(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, string ramUsedParameters) //Сообщение
    {
        string message = "";

        if (ramUsedPercent >= 50.0) //При нагрузке RAM >= 50%
        {
            message += $"RAM: {Math.Round((decimal)ramUsedPercent, 0)}% ({ramUsedParameters})   ";
        }
        if (diskUsageParameter >= 50.0) //При нагрузке диска >= 50%
        {
            message += $"MEMORY: {diskUsageParameter}%   ";
        }
        if (cpuUsageParameter >= 50.0) //При нагрузке CPU >= 50%
        {
            message += $"CPU: {cpuUsageParameter}%";
        }
        return message;
    }

    private string MessageCloseSolution(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, string ramUsedParameters) //Сообщение близкое к решению
    {
        string message = "";

        if (ramUsedPercent >= 40.0) //При нагрузке RAM >= 40%
        {
            message += $"RAM: {Math.Round((decimal)ramUsedPercent, 0)}% ({ramUsedParameters})   ";
        }
        if (diskUsageParameter >= 40.0) //При нагрузке диска >= 40%
        {
            message += $"MEMORY: {diskUsageParameter}%   ";
        }
        if (cpuUsageParameter >= 40.0) //При нагрузке CPU >= 40%
        {
            message += $"CPU: {cpuUsageParameter}%";
        }
        return message;
    }

    private int ErrorImportance(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, PostgresContext pc) //Критичность ошибки
    {
        if (ramUsedPercent >= 90.0 || cpuUsageParameter >= 90.0 || diskUsageParameter >= 90.0) //Критическая
        {
            return 5;
        }
        else if (ramUsedPercent >= 80.0 || cpuUsageParameter >= 80.0 || diskUsageParameter >= 80.0) //Очень высокая
        {
            return 4;
        }
        else if (ramUsedPercent >= 70.0 || cpuUsageParameter >= 70.0 || diskUsageParameter >= 70.0) //Высокая
        {
            return 3;
        }
        else if (ramUsedPercent >= 60.0 || cpuUsageParameter >= 60.0 || diskUsageParameter >= 60.0) //Умеренная
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private void ProblemAdd(PostgresContext pc, Guid IdServer_, int ErrorImportance_, string message) //Добавление новой проблемы
    {
        try
        {
            Problem newProblem = new Problem()
            {
                DateTimeProblem = DateTime.Now,
                DateProblemSolution = null,
                ErrorImportance = ErrorImportance_,
                StatusProblem = false,
                IdServer = IdServer_,
                MessageProblem = message
            };
            pc.Problems.Add(newProblem);
            pc.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void ProblemUpdate(PostgresContext pc, Problem problem, int ErrorImportance_, string message) //Измение существующей проблемы
    {
        try
        {
            problem.MessageProblem = message;
            problem.ErrorImportance = ErrorImportance_;

            pc.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void ProblemSolving(PostgresContext pc, Problem problem, string message) //Решение проблеммы
    {
        try
        {
            problem.MessageProblem = message;
            problem.StatusProblem = true;

            pc.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void ServerStatusUdpate(PostgresContext pc, Server server, bool serverStatus) //Обновление статуса проблемы
    {
        try
        {
            server.ServerStatus = serverStatus;

            pc.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}