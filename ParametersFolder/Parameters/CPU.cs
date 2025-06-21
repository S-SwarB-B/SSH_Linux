using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class CPU
    {
        public static double CPUUsedParameter(string[] result, int countInResult)
        {
            string[] CPUUsage = result[countInResult].Split(": ");
            return Convert.ToDouble(CPUUsage[1].Trim(new char[] { '%' }));
        }
    }
}
