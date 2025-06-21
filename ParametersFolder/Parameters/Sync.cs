using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class Sync
    {
        public static int SyncParameter(string[] result, int countInResult)
        {
            string[] syncInfo = result[countInResult].Split(": ");
            if (syncInfo[1].Contains("yes")) { return 1; }
            else { return 0; }
        }
    }
}
