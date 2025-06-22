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
                    string parametersServer = ParametersServer.ParametersForServer(sc, server);
                    SshCommand? cmd = OpenFile.StartServerMonitoringAgent(serverConnect, path, "", ref cmdReturnStr);

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
         
                        double memoryUsedPercent = 0;

                        double cpuUsageParameter = 0; //Процент загруженности центрального процессора (CPU)

                        double storageUsageParameter = 0; //Процент загруженности диска

                        int sync = -1;

                        int systime = -1;

                        int network = -1;

                        ParametersCompletion.Parameters(result,
                            ref memoryUsedPercent,
                            ref storageUsageParameter,
                            ref cpuUsageParameter,
                            ref sync,
                            ref systime,
                            ref network
                        );

                        CurrentParameters.AddParametersInDataBase(sc, server.IdServer, memoryUsedPercent, cpuUsageParameter, storageUsageParameter); //Заполнение параметров серверов

                        string message = Messages.Message(memoryUsedPercent, cpuUsageParameter, storageUsageParameter,
                        sync, systime, network); //Получение сообщения о нагруженности

                        ProblemDefinition.ProblemDef(sc, message, server, memoryUsedPercent, cpuUsageParameter, storageUsageParameter, systime, sync, network);

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
