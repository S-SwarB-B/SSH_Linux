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
        public static void Parameters(
            string[] result,
            ref string[] memoryUsed, ref double memoryUsedPercent,
            ref double storageUsageParameter,
            ref double cpuUsageParameter,
            ref int sync,
            ref int network
            )
        {
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Contains(Constants.Tags.TagMEMORY))
                {
                    memoryUsed = result[i].Split(": ");
                    memoryUsedPercent = Memory.MemoryUsedParameter(memoryUsed);
                }
                else if (result[i].Contains(Constants.Tags.TagSTORAGE))
                {
                    storageUsageParameter = Storage.StorageUsedParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagCPU))
                {
                    cpuUsageParameter = CPU.CPUUsedParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagSYNCTIME))
                {
                    sync = Sync.SyncParameter(result, i);
                }
                else if (result[i].Contains(Constants.Tags.TagNETWORK))
                {
                    network = Network160.NetworkParameter(result, i);
                }
            }
        }
    }
}
