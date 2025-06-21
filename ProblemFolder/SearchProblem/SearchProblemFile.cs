using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class SearchProblemFile
    {
        public static bool SearchInProblemFile(SSHContext sc, Server server)
        {
            return sc.Problems 
                    .Where(x => x.IdServer == server.IdServer)
                    .Any(x => x.StatusProblem == false && x.MessageProblem == Constants.ActiveProblem.MessageFailedFile);
        }
        public static Problem SearchCertainProblemFile(SSHContext sc, Server server)
        {
            return sc.Problems.First(x => x.IdServer == server.IdServer
                   && x.StatusProblem == false && x.MessageProblem == Constants.ActiveProblem.MessageFailedFile);
        }
    }
}
