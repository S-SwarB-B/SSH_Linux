using Microsoft.EntityFrameworkCore;
using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class SearchProblemConnect
    {
        public static bool SearchInProblemConnect(ServerMonitoringContext sc, Server server) //Поиск ошибок подключения
        {
            return new ServerMonitoringContext().Errors
                .Where(x => x.ServerId == server.Id)
                .Include(x => x.Server)
                .Any(x => x.State == false && x.Server.State == false 
                    && x.Message == Constants.ActiveProblem.MessageFailedConnect);
        }
        public static Error SearchCertainProblemConnect (ServerMonitoringContext sc, Server server) //Поиск конкретной ошибки подключения
        {
            return sc.Errors.Include(x => x.Server).First(x => x.ServerId == server.Id
                   && x.Server.State == false
                   && x.State == false
                   && x.Message == Constants.ActiveProblem.MessageFailedConnect
                   );
        }
    }
}
