using SSHProject.DB;
using SSHProject.ProblemFolder.SearchProblem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ProblemFolder.MessageProblem
{
    internal class MessageProblemStandart
    {
        public static void StandartMessage(ServerMonitoringContext sc, Server server,
            int metric,
            int importance,
            string messageProblem,
            string messageSolutionProblem,
            DateTime startProgram)
        {
            if (metric >= Constants.ErrorImportanceClass.ImportanceStandart.Low && !SearchProblemServer.SearchInProblemServer(sc, server, messageProblem)) //При нагрузке RAM
            {
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{messageProblem} {Message(importance)}", startProgram);
            }
            else if (metric >= Constants.ErrorImportanceClass.ImportanceStandart.Low && SearchProblemServer.SearchInProblemServer(sc, server, messageProblem))
            {
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, messageProblem),
                    importance, $"{messageProblem} {Message(importance)}");
            }
            else if (metric <= Constants.ErrorImportanceClass.ImportanceStandart.Low
                && metric >= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, messageProblem))
            {
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, messageProblem),
                    importance, $"{messageProblem} {Message(importance)}");
            }
            else if (metric <= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, messageProblem))
            {
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, messageProblem),
                    $"{messageSolutionProblem}", startProgram);
            }
        }
        private static string Message(int importance)
        {
            if (importance == 5)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Critical}";
            }
            else if (importance == 4)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.VeryHight}";
            }
            else if (importance == 3)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Hight}";
            }
            else if (importance == 2)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Medium}";
            }
            else if (importance == 1)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Low}";
            }
            else
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.VeryLow}";
            }
        }
    }
}
