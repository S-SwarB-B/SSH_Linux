using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class Memory
    {
        public static double MemoryUsedParameter(string[] result, int countInResult) //MEMORY
        {
            string[] memoryUsedParameters = result[countInResult].Split($"{Constants.Tags.TagMEMORY} "); //Обработка результата
            return Convert.ToDouble(memoryUsedParameters[1]);                                            //
        }
    }
}
