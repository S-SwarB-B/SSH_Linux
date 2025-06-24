using Renci.SshNet;
using SSHProject.DB;
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
        public void SSH(Guid idServer, string ip, string login, string password, string path, ServerMonitoringContext sc, DateTime startProgram)
        {
            var server = sc.Servers.First(x => x.Id == idServer);

            int memoryUsedPercent = 0; //Процент загруженности оперативной памяти

            int cpuUsageParameter = 0; //Процент загруженности центрального процессора (CPU)

            int storageUsageParameter = 0; //Процент загруженности диска

            using (var serverConnect = new SshClient(ip, login, password)) //Подключение к серверу
            {
                try { serverConnect.Connect(); } catch { } //Попытка подключения

                if (serverConnect.IsConnected)
                {
                    bool searchInProblemConnect = SearchProblemConnect.SearchInProblemConnect(sc, server); //Поиск проблем с подключением этого сервера

                    if (searchInProblemConnect)
                    {
                        var problem = SearchProblemConnect.SearchCertainProblemConnect(sc, server); //Конкретная проблема
                        DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulConnect, startProgram); //Решение проблемы
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
                            DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulFile, startProgram); //Решение проблемы
                        }

                        string[] result = cmdReturnStr.Split("\n"); //Получение данных из CMD
                           
                        int sync = -1; //Синхронизация

                        int systime = -1; //Системное время

                        int network = -1; //IP

                        int demonDocker = -1; //Демон докера

                        int containerSDU = -1; //Контейнер SDU

                        int fileStorage = -1; //Файловое хранилище

                        int containerPostgres = -1; //Контейнер постгрес

                        int containerETCD = -1; //Контейнер ETCD

                        int ddmWebAdmin = -1; //Контейнер DDMWebAdmin

                        int ddmWeb = -1; //Контейнер DDMWebUi

                        int ddmWebApi = -1; //Контейнер DDMWebApi

                        ParametersCompletion.Parameters(result, 
                            ref memoryUsedPercent,              
                            ref storageUsageParameter,          
                            ref cpuUsageParameter,              
                            ref sync,                           
                            ref systime,                        
                            ref network,
                            ref demonDocker,
                            ref containerSDU,
                            ref fileStorage,
                            ref containerPostgres,
                            ref containerETCD,
                            ref ddmWebAdmin,
                            ref ddmWeb,
                            ref ddmWebApi
                        );                                      

                        CurrentParameters.AddParametersInDataBase(sc, server.Id, memoryUsedPercent, cpuUsageParameter, storageUsageParameter, startProgram); //Заполнение параметров серверов

                        Messages.ProblemMessage(sc, server, startProgram, memoryUsedPercent, cpuUsageParameter, storageUsageParameter,
                        sync, systime, network, demonDocker, containerSDU, fileStorage, containerPostgres, containerETCD, ddmWebAdmin, ddmWeb, ddmWebApi); //Получение проблем

                    }
                    else //Файл не найден
                    {
                        bool searchInProblemFile = SearchProblemFile.SearchInProblemFile(sc, server);

                        if (!searchInProblemFile)
                        {
                            DealingProblem.ProblemAdd(sc, server.Id, 5, Constants.ActiveProblem.MessageFailedFile, startProgram); //Запись проблемы
                        }

                        CurrentParameters.AddParametersInDataBase(sc, server.Id, memoryUsedPercent, cpuUsageParameter, storageUsageParameter, startProgram); //Заполнение параметров серверов
                    }
                }
                else //Подключение не установлено
                {
                    bool searchInProblemConnect = SearchProblemConnect.SearchInProblemConnect(sc, server);

                    ServerStatusUpdate.ServerStatusUpd(sc, server, false); //Смена статуса на деактивированный

                    if (!searchInProblemConnect)
                    {
                        DealingProblem.ProblemAdd(sc, server.Id, 5, Constants.ActiveProblem.MessageFailedConnect, startProgram); //Запись проблемы
                    }

                    CurrentParameters.AddParametersInDataBase(sc, server.Id, memoryUsedPercent, cpuUsageParameter, storageUsageParameter, startProgram); //Заполнение параметров серверов
                }
                serverConnect.Disconnect(); //Отключение от сервера
            }
        }
    }
}
