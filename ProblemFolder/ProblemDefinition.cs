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
        public static void ProblemDef(SSHContext sc, string message, Server server, 
            double memoryUsedPercent, string[] memoryUsed,
            double cpuUsageParameter, 
            double storageUsageParameter,
            TimeSpan dateTime,
            int sync
            )
        {
            int ErrorImportance_ = ErrorImportance(memoryUsedPercent, cpuUsageParameter, storageUsageParameter, sc);

            bool searchInProblem = SearchProblemServer.SearchInProblemServer(sc, server);

            if (message != "" && !searchInProblem)
            {
                DealingProblem.ProblemAdd(sc, server.IdServer, ErrorImportance_, message);
            }
            else if (message != "" && searchInProblem)
            {
                var problem = SearchProblemServer.SearchCertainProblemServer(sc, server);
                DealingProblem.ProblemUpdate(sc, problem, ErrorImportance_, message);
            }
            else if (message == "" && searchInProblem &&
            (memoryUsedPercent >= 40 || storageUsageParameter >= 40 || cpuUsageParameter >= 40 || dateTime.TotalSeconds >= 10))
            {
                message = Messages.MessageCloseSolution(memoryUsedPercent, storageUsageParameter, cpuUsageParameter,
                memoryUsed[1], sync, dateTime);
                var problem = SearchProblemServer.SearchCertainProblemServer(sc, server);
                DealingProblem.ProblemUpdate(sc, problem, ErrorImportance_, message);
            }
            else if (message == "" && searchInProblem && memoryUsedPercent <= 40 && storageUsageParameter <= 40
            && cpuUsageParameter <= 40 && sync == 1 && dateTime.TotalSeconds <= 10)
            {
                var problem = SearchProblemServer.SearchCertainProblemServer(sc, server);
                DealingProblem.ProblemSolving(sc, problem, Constants.SolutionsProblem.MessageSuccessfulProblem);
            }
        }

        private static int ErrorImportance(double memoryUsedPercent, double cpuUsageParameter, double storageUsageParameter, SSHContext pc) //Критичность ошибки
        {
            if (memoryUsedPercent >= Constants.ErrorImportanceClass.Critical.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.Critical.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.Critical.Storage) //Критическая
            {
                return 5;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.VeryHight.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.VeryHight.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.VeryHight.Storage) //Очень высокая
            {
                return 4;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.Hight.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.Hight.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.Hight.Storage) //Высокая
            {
                return 3;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.Medium.Memory
                || cpuUsageParameter >= Constants.ErrorImportanceClass.Medium.CPU
                || storageUsageParameter >= Constants.ErrorImportanceClass.Medium.Storage) //Умеренная
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
