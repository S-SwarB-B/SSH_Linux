using Renci.SshNet;
using SSHProject.ParametersFolder;
using SSHProject.ParametersFolder.Parameters;
using SSHProject.ProblemFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class SSHConnect
    {
        public void SSH(Guid idServer, string ip, string login, string password, string path, SSHContext sc)
        {
            var server = sc.Servers.First(x => x.IdServer == idServer);

            using (var serverConnect = new SshClient(ip, login, password)) //Подключение к серверу
            {
                try { serverConnect.Connect(); } catch { } //Попытка подключения

                if (serverConnect.IsConnected)
                {
                    bool searchInProblemConnect = SearchProblemConnect.SearchInProblemConnect(sc, server);

                    if (searchInProblemConnect)
                    {
                        var problem = SearchProblemConnect.SearchCertainProblemConnect(sc, server);
                        DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulConnect); //Решение проблемы
                    }

                    ServerStatusUpdate.ServerStatusUpd(sc, server, true); //Смена статуса на активированный

                    string? cmdReturnStr = null;
                    SshCommand? cmd = OpenFile.StartServerMonitoringAgent(serverConnect, path, ref cmdReturnStr);

                    if (cmd != null && cmdReturnStr != null
                        && cmdReturnStr.Contains(Constants.Tags.TagCPU)
                        && cmdReturnStr.Contains(Constants.Tags.TagMEMORY)
                        && cmdReturnStr.Contains(Constants.Tags.TagSTORAGE))
                    {
                        bool searchInProblemFile = SearchProblemFile.SearchInProblemFile(sc, server);

                        if (searchInProblemFile)
                        {
                            var problem = SearchProblemFile.SearchCertainProblemFile(sc, server);
                            DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulFile); //Решение проблемы
                        }

                        string[] result = cmdReturnStr.Split("\n"); //Получение данных из CMD

                        string[] memoryUsed = new string[2]; //Получение информации о загруженности оперативной памяти (RAM)               
                        double memoryUsedPercent = 0;

                        double cpuUsageParameter = 0; //Процент загруженности центрального процессора (CPU)

                        double storageUsageParameter = 0; //Процент загруженности диска

                        string[] sysTimeInfo = new string[2]; //Получение информации о системном времени
                        int sync = 0;
                        string[] dateTimeInfo = new string[5];
                        TimeSpan dateTime = new TimeSpan();

                        int network = 0;

                        ParametersCompletion.Parameters(result,
                            ref memoryUsed, ref memoryUsedPercent,
                            ref storageUsageParameter,
                            ref cpuUsageParameter,
                            ref sync,
                            ref network
                        );

                        CurrentParameters.AddParametersInDataBase(sc, server.IdServer, memoryUsedPercent, cpuUsageParameter, storageUsageParameter); //Заполнение параметров серверов

                        string message = Messages.Message(memoryUsedPercent, cpuUsageParameter, storageUsageParameter,
                        memoryUsed[1], sync, dateTime, network); //Получение сообщения о нагруженности

                        ProblemDefinition.ProblemDef(sc, message, server, memoryUsedPercent, memoryUsed, cpuUsageParameter, storageUsageParameter, dateTime, sync, network);

                    }
                    else
                    {
                        bool searchInProblemFile = SearchProblemFile.SearchInProblemFile(sc, server);

                        if (!searchInProblemFile)
                        {
                            DealingProblem.ProblemAdd(sc, server.IdServer, 5, Constants.ActiveProblem.MessageFailedFile); //Запись проблемы
                        }
                    }
                }
                else
                {
                    bool searchInProblemConnect = SearchProblemConnect.SearchInProblemConnect(sc, server);

                    ServerStatusUpdate.ServerStatusUpd(sc, server, false); //Смена статуса на деактивированный

                    if (!searchInProblemConnect)
                    {
                        DealingProblem.ProblemAdd(sc, server.IdServer, 5, Constants.ActiveProblem.MessageFailedConnect); //Запись проблемы
                    }
                }
                serverConnect.Disconnect(); //Отключение от сервера
            }
        }
    }
}
