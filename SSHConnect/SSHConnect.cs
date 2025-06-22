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
                    bool searchInProblemConnect = SearchProblemConnect.SearchInProblemConnect(sc, server); //Поиск проблем с подключением этого сервера

                    if (searchInProblemConnect)
                    {
                        var problem = SearchProblemConnect.SearchCertainProblemConnect(sc, server); //Конкретная проблема
                        DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulConnect); //Решение проблемы
                    }

                    ServerStatusUpdate.ServerStatusUpd(sc, server, true); //Смена статуса на активированный

                    string? cmdReturnStr = null;
                    string parametersServer = ParametersServer.ParametersForServer(sc, server); //Получение строки с уникальными параметрами
                    SshCommand? cmd = OpenFile.StartServerMonitoringAgent(serverConnect, path, parametersServer, ref cmdReturnStr); //Открытие файла

                    if (cmd != null && cmdReturnStr != null //Файл найден
                        && cmdReturnStr.Contains(Constants.Tags.TagCPU)
                        && cmdReturnStr.Contains(Constants.Tags.TagMEMORY)
                        && cmdReturnStr.Contains(Constants.Tags.TagSTORAGE))
                    {
                        bool searchInProblemFile = SearchProblemFile.SearchInProblemFile(sc, server); //Поиск проблемы отсутствия файла

                        if (searchInProblemFile)
                        {
                            var problem = SearchProblemFile.SearchCertainProblemFile(sc, server); //Конкретная проблема
                            DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulFile); //Решение проблемы
                        }

                        string[] result = cmdReturnStr.Split("\n"); //Получение данных из CMD
         
                        double memoryUsedPercent = 0; //Процент загруженности оперативной памяти

                        double cpuUsageParameter = 0; //Процент загруженности центрального процессора (CPU)

                        double storageUsageParameter = 0; //Процент загруженности диска

                        int sync = -1; //Синхронизация

                        int systime = -1; //Системное время

                        int network = -1; //IP
                        
                        ParametersCompletion.Parameters(result, //
                            ref memoryUsedPercent,              //
                            ref storageUsageParameter,          //
                            ref cpuUsageParameter,              // Получение параметров
                            ref sync,                           //
                            ref systime,                        //
                            ref network                         //
                        );                                      //

                        CurrentParameters.AddParametersInDataBase(sc, server.IdServer, memoryUsedPercent, cpuUsageParameter, storageUsageParameter); //Заполнение параметров серверов

                        string message = Messages.Message(memoryUsedPercent, cpuUsageParameter, storageUsageParameter,
                        sync, systime, network); //Получение сообщения о нагруженности

                        ProblemDefinition.ProblemDef(sc, message, server, memoryUsedPercent, cpuUsageParameter, storageUsageParameter, systime, sync, network); //Работа с проблемами

                    }
                    else //Файл не найден
                    {
                        bool searchInProblemFile = SearchProblemFile.SearchInProblemFile(sc, server);

                        if (!searchInProblemFile)
                        {
                            DealingProblem.ProblemAdd(sc, server.IdServer, 5, Constants.ActiveProblem.MessageFailedFile); //Запись проблемы
                        }
                    }
                }
                else //Подключение не установлено
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
