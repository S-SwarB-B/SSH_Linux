using Org.BouncyCastle.Bcpg.OpenPgp;
using Renci.SshNet.Messages;
using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ProblemFolder.SearchProblem
{
    internal class SearchProblemServer
    {
        public static bool SearchInProblemServer(ServerMonitoringContext sc, Server server, string message) //Поиск ошибок
        {
            return sc.Errors.
                    Where(x => x.ServerId == server.Id)
                    .Any(x => x.State == false && x.Message.Contains(message));
        }
        public static Error SearchCertainProblemServer(ServerMonitoringContext sc, Server server, string message) //Поиск конкретной ошибки
        {
            return sc.Errors.First( 
                x => x.ServerId == server.Id &&
                x.State == false && x.Message.Contains(message));
        }
    }
}
