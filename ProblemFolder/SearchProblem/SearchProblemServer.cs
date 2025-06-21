using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ProblemFolder.SearchProblem
{
    internal class SearchProblemServer
    {
        public static bool SearchInProblemServer(SSHContext sc, Server server)
        {
            return sc.Problems.
                    Where(x => x.IdServer == server.IdServer)
                    .Any(x => x.StatusProblem == false);
        }
        public static Problem SearchCertainProblemServer(SSHContext sc, Server server)
        {
            return sc.Problems.First( 
                x => x.IdServer == server.IdServer &&
                x.StatusProblem == false);
        }
    }
}
