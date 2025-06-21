using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class Memory
    {
        public static double MemoryUsedParameter(string[] memoryUsed)
        {
            string[] memoryUsedParameters = memoryUsed[1].Split(" / "); //Разделение числовых значений
            double memoryUsedParameter = Convert.ToDouble(
               memoryUsedParameters[0].Trim(new char[] { 'G', 'i', ' ', 'M', 'K' })); //Объем занятой оперативной памяти (RAM)
            double memoryUsedParameterMax = Convert.ToDouble(
                memoryUsedParameters[1].Trim(new char[] { 'G', 'i', ' ', 'M', 'K' })); //Максимальный объем оперативной памяти (RAM)

            return memoryUsedParameter / memoryUsedParameterMax * 100; //Получение процента занятости оперативной памяти (RAM)
        }
    }
}
