using SSHProject.ParametersFolder.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class ParametersCompletion
    {
        public static void Parameters(                   //
            string[] result,                             //
            ref double memoryUsedPercent,                //
            ref double storageUsageParameter,            //
            ref double cpuUsageParameter,                // Получение информации о всех параметрах
            ref int sync,                                //
            ref int systime,                             //
            ref int network                              //
            )                                            //
        {
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Contains(Constants.Tags.TagMEMORY)) //MEMORY
                {
                    memoryUsedPercent = Memory.MemoryUsedParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagSTORAGE)) //STORAGE
                {
                    storageUsageParameter = Storage.StorageUsedParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagCPU)) //CPU
                {
                    cpuUsageParameter = CPU.CPUUsedParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagSYSTIME)) //SYSTIME
                {
                    systime = SysTime.SysTimeParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagSYNCTIME)) //SYNCTIME
                {
                    sync = Sync.SyncParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagNETWORK)) //NETWORK
                {
                    network = Network160.NetworkParameter(result, i);
                }
            }
        }
    }
}
