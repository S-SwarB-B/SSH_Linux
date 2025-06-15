using Renci.SshNet;
using SSHProject.DB;
using System.Dynamic;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Esf;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Org.BouncyCastle.Tls;

namespace SSHProject;

public class MethodsSSH
{

    public void SSHConnectAllServers() //Подключение ко всем серверам
    {
        while (true)
        {
            List<Server> serverList = GetServersFromDataBase(); //Получение серверов из базы

            Parallel.ForEach(serverList, serverInformation => //Паралельный вызов для каждого из серверов
            {
                SSHConnect(serverInformation.IdServer, serverInformation.IpAdress, serverInformation.Login,
                serverInformation.Password);
            });
            Thread.Sleep(5000); //Задержка перед получением новых данных
        }
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
        try
        {
            PostgresContext pc = new PostgresContext();

            var serverConnect = new SshClient(ip, login, password); //Подключение к серверу
            serverConnect.Connect();
            var cmd = serverConnect.CreateCommand("shopt -s globstar && cd ~/**/LinuxMonitor/LinuxMonitor/LinuxMonitor && dotnet run"); //Ввод команды в CMD
            string[] result = cmd.Execute().Split("\n"); //Получение данных из CMD

            string[] ramUsed = result[2].Split(": "); //Получение информации о загруженности оперативной памяти (RAM)
            string[] ramUsedParameters = ramUsed[1].Split(" / "); //Разделение числовых значений
            double ramUsedParameter = Convert.ToDouble(
                ramUsedParameters[0].Trim(new char[] { 'G', 'i', ' ' })); //Объем занятой оперативной памяти (RAM)
            double ramUsedParameterMax = Convert.ToDouble(
                ramUsedParameters[1].Trim(new char[] { 'G', 'i', ' ' })); //Максимальный объем оперативной памяти (RAM)

            double ramUsedPercent = ramUsedParameter / ramUsedParameterMax * 100; //Получение процента занятости оперативной памяти (RAM)

            string[] cpuUsed = result[5].Split(": "); //Получение информации о загруженности центрального процессора (CPU)
            double cpuUsageParameter = Convert.ToDouble( //Процент загруженности центрального процессора (CPU)
                cpuUsed[1].Trim(new char[] { '%' }));

            string[] diskUsage = result[8].Split(": "); //Получение информации о загруженности диска
            double diskUsageParameter = Convert.ToDouble(  //Процент загруженности диска
                diskUsage[1].Trim(new char[] { '%' }));

            string message = Message(ramUsedPercent, cpuUsageParameter, diskUsageParameter, ramUsed[1]); //Получение сообщения о нагруженности

            bool searchInProblem = pc.Problems. //Проверка на проблемы этого сервера
            Where(x => x.IdServer == idServer)
            .Any(x => x.StatusProblem == false);

            if (message != "" && !searchInProblem) //Отсутствие проблем этого сервера
            {
                Guid IdErrorImportance_ = ErrorImportanceID(ramUsedPercent, cpuUsageParameter, diskUsageParameter, pc); //Получение критичности проблемы
                ProblemAdd(pc, idServer, IdErrorImportance_, message); //Добавление новой проблемы
            }
            else if (message != "" && searchInProblem) //Проблемы были и до этого
            {
                Guid IdErrorImportance_ = ErrorImportanceID(ramUsedPercent, cpuUsageParameter, diskUsageParameter, pc); //Получение критичности проблемы
                var problem = pc.Problems.First(x => x.IdServer == idServer); //Поиск этой проблемы
                ProblemUpdate(pc, problem, IdErrorImportance_, message); //Изменение проблемы
            }
            else if (message == "" && searchInProblem && (ramUsedPercent >= 55 || diskUsageParameter >= 55 || cpuUsageParameter >= 55)) //Близкая к решению проблема
            {
                message = MessageCloseSolution(ramUsedPercent, diskUsageParameter, cpuUsageParameter, ramUsed[1]); //Новое сообщение
                Guid IdErrorImportance_ = ErrorImportanceID(ramUsedPercent, cpuUsageParameter, diskUsageParameter, pc); //Получение критичности проблемы
                var problem = pc.Problems.First(x => x.IdServer == idServer);
                ProblemUpdate(pc, problem, IdErrorImportance_, message); //Изменение проблемы
            }
            else if (message == "" && searchInProblem && ramUsedPercent <= 55 && diskUsageParameter <= 55 && cpuUsageParameter <= 55) //Проблема решена
            {
                var problem = pc.Problems.First(x => x.IdServer == idServer); //Поиск проблемы
                ProblemSolving(pc, problem, message); //Изменение статуса проблемы
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private string Message(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, string ramUsedParameters) //Сообщение
    {
        string message = "";

        if (ramUsedPercent >= 60.0) //При нагрузке RAM >= 60%
        {
            message += $"RAM занята на {ramUsedPercent}% ({ramUsedParameters});\n";
        }
        if (diskUsageParameter >= 60.0) //При нагрузке диска >= 60%
        {
            message += $"Диск занят на {diskUsageParameter}%;\n";
        }
        if (cpuUsageParameter >= 60.0) //При нагрузке CPU >= 60%
        {
            message += $"CPU загружен на {cpuUsageParameter}%;";
        }
        return message;
    }

    private string MessageCloseSolution(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, string ramUsedParameters) //Сообщение близкое к решению
    {
        string message = "";

        if (ramUsedPercent >= 55.0) //При нагрузке RAM >= 55%
        {
            message += $"RAM занята на {ramUsedPercent}% ({ramUsedParameters});\n";
        }
        if (diskUsageParameter >= 55.0) //При нагрузке диска >= 55%
        {
            message += $"Диск занят на {diskUsageParameter}%;\n";
        }
        if (cpuUsageParameter >= 55.0) //При нагрузке CPU >= 55%
        {
            message += $"CPU загружен на {cpuUsageParameter}%;";
        }
        return message;
    }

    private Guid ErrorImportanceID(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, PostgresContext pc) //Критичность ошибки
    {
        if (ramUsedPercent >= 90.0 || cpuUsageParameter >= 90.0 || diskUsageParameter >= 90.0) //Чрезмерно высокая
        {
            return pc.ErrorImportances
            .Where(i => i.NameErrorImportances == "Чрезмерно высокая нагрузка")
            .Select(i => i.IdErrorImportance)
            .FirstOrDefault();
        }
        else if (ramUsedPercent >= 75.0 || cpuUsageParameter >= 75.0 || diskUsageParameter >= 75.0) //Высокая
        {
            return pc.ErrorImportances
            .Where(i => i.NameErrorImportances == "Высокая нагрузка")
            .Select(i => i.IdErrorImportance)
            .FirstOrDefault();
        }
        else //Умеренная
        {
            return pc.ErrorImportances
            .Where(i => i.NameErrorImportances == "Умеренная нагрузка")
            .Select(i => i.IdErrorImportance)
            .FirstOrDefault();
        }
    }

    private void ProblemAdd(PostgresContext pc, Guid IdServer_, Guid IdErrorImportance_, string message) //Добавление новой проблемы
    {
        try
        {
            Problem newProblem = new Problem()
            {
                DateTimeProblem = DateTime.Now,
                DateProblemSolution = null,
                IdErrorImportance = IdErrorImportance_,
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

    private void ProblemUpdate(PostgresContext pc, Problem problem, Guid IdErrorImportance_, string message) //Измение существующей проблемы
    {
        try
        {
            problem.MessageProblem = message;
            problem.IdErrorImportance = IdErrorImportance_;

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
}
