using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class ParameterReceive
    {
        public static int MetricAPP(string[] result, int countInResult, string tag) //CPU
        {
            string[] metric = result[countInResult].Split($"{tag} "); //Обработка результата
            return Convert.ToInt32(metric[1]);                                         //
        }
    }
}
