using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class Network160
    {
        public static int NetworkParameter(string[] result, int countInResult)
        {
            string[] networkInfo = result[countInResult].Split($"{Constants.Tags.TagNETWORK} ");
            return Convert.ToInt32(networkInfo[1]);
        }
    }
}
