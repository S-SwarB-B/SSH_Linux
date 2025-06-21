using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class SearchProblemConnect
    {
        public static bool SearchInProblemConnect(SSHContext sc, Server server)
        {
            return sc.Problems
                .Where(x => x.IdServer == server.IdServer)
                .Include(x => x.IdServerNavigation)
                .Any(x => x.StatusProblem == false && x.IdServerNavigation.ServerStatus == false 
                    && x.MessageProblem == Constants.MessageFailedConnect);
        }
        public static Problem SearchCertainProblemConnect (SSHContext sc, Server server)
        {
            return sc.Problems.Include(x => x.IdServerNavigation).First(x => x.IdServer == server.IdServer
                   && x.IdServerNavigation.ServerStatus == false
                   && x.StatusProblem == false
                   && x.MessageProblem == Constants.MessageFailedConnect
                   );
        }
    }
}
