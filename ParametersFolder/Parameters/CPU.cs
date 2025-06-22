using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class CPU
    {
        public static double CPUUsedParameter(string[] result, int countInResult) //CPU
        {
            string[] CPUUsage = result[countInResult].Split($"{Constants.Tags.TagCPU} "); //Обработка результата
            return Convert.ToDouble(CPUUsage[1]);                                         //
        }
    }
}
