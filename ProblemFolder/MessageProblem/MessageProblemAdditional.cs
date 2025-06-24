using SSHProject.DB;
using SSHProject.ParametersFolder.Parameters;
using SSHProject.ProblemFolder.SearchProblem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ProblemFolder.MessageProblem
{
    internal class MessageProblemAdditional
    {
        public static void AdditionalMessage(ServerMonitoringContext sc, Server server,
            int metric,
            int importance,
            string messageProblem,
            string messageSolutionProblem,
            DateTime startProgram)
        {
            if (metric == 0 && !SearchProblemServer.SearchInProblemServer(sc, server, messageProblem))
            {
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{messageProblem}", startProgram);
            }
            else if (metric == 0 && SearchProblemServer.SearchInProblemServer(sc, server, messageProblem))
            {
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, messageProblem),
                    importance, $"{messageProblem}");
            }
            else if (metric == 1 && SearchProblemServer.SearchInProblemServer(sc, server, messageProblem))
            {
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, messageProblem),
                    $"{messageSolutionProblem}", startProgram);
            }
        }
    }
}
