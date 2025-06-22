using Renci.SshNet.Messages;
using SSHProject.ProblemFolder.SearchProblem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ProblemFolder
{
    internal class ProblemDefinition
    {
        public static void ProblemDef(SSHContext sc, string message, Server server,  // 
            double memoryUsedPercent,                                                //
            double cpuUsageParameter,                                                //
            double storageUsageParameter,                                            // Работа с ошибками
            int sync,                                                                //
            int systime,                                                             //
            int network                                                              //
            )                                                                        //
        {
            int ErrorImportance_ = ErrorImportance(memoryUsedPercent, cpuUsageParameter, storageUsageParameter,sync, systime, network, sc); //Получение критичности ошибки

            bool searchInProblem = SearchProblemServer.SearchInProblemServer(sc, server); //Поиск проблем этого сервера

            if (message != "" && !searchInProblem) //Если проблем нет
            {
                DealingProblem.ProblemAdd(sc, server.IdServer, ErrorImportance_, message);
            }
            else if (message != "" && searchInProblem) //Если проблема существует
            {
                var problem = SearchProblemServer.SearchCertainProblemServer(sc, server); //Конкретная проблема
                DealingProblem.ProblemUpdate(sc, problem, ErrorImportance_, message); //Изменение
            }
            else if (message == "" && searchInProblem &&
            (memoryUsedPercent >= 40 || storageUsageParameter >= 40 || cpuUsageParameter >= 40)) //Если проблема близка к решению
            {
                message = Messages.MessageCloseSolution(memoryUsedPercent, storageUsageParameter, cpuUsageParameter,
                sync, systime, network); //Получение сообщения
                var problem = SearchProblemServer.SearchCertainProblemServer(sc, server); //Поиск конкретной проблемы
                DealingProblem.ProblemUpdate(sc, problem, ErrorImportance_, message); //Изменение
            }
            else if (message == "" && searchInProblem && memoryUsedPercent <= 40 && storageUsageParameter <= 40 //Проблема решена
            && cpuUsageParameter <= 40 && sync == 1 && network == 1 && systime == 1)
            {
                var problem = SearchProblemServer.SearchCertainProblemServer(sc, server); //Поиск конкретной проблемы
                DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulProblem); //Завершение проблемы
            }
        }

        private static int ErrorImportance(double memoryUsedPercent, double cpuUsageParameter, double storageUsageParameter, int sync, int systime, int network, SSHContext pc) //Критичность ошибки
        {
            if (memoryUsedPercent >= Constants.ErrorImportanceClass.Critical.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.Critical.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.Critical.Storage
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 5 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 5 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 5 && systime == 0)) //Критическая
            {
                return 5;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.VeryHight.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.VeryHight.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.VeryHight.Storage
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 4 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 4 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 4 && systime == 0)) //Очень высокая
            {
                return 4;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.Hight.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.Hight.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.Hight.Storage
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 3 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 3 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 3 && systime == 0)) //Высокая
            {
                return 3;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.Medium.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.Medium.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.Medium.Storage
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 2 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 2 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 2 && systime == 0)) //Умеренная
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}
