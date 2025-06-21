using SSHProject.ParametersFolder.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class Messages
    {
        public static string Message(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, string ramUsedParameters, int sync, TimeSpan datetime, int network) //Сообщение
        {
            string message = "";

            if (ramUsedPercent >= 50.0) //При нагрузке RAM >= 50%
            {
                message += $"{Constants.ActiveProblem.MemoryProblem} {Math.Round((decimal)ramUsedPercent, 0)}% ({ramUsedParameters})   ";
            }
            if (diskUsageParameter >= 50.0) //При нагрузке диска >= 50%
            {
                message += $"{Constants.ActiveProblem.StorageProblem} {diskUsageParameter}%   ";
            }
            if (cpuUsageParameter >= 50.0) //При нагрузке CPU >= 50%
            {
                message += $"{Constants.ActiveProblem.CPUProblem} {cpuUsageParameter}%   ";
            }
            if (sync == 0)
            {
                message += $"{Constants.ActiveProblem.SyncProblem} {sync}   ";
            }
            if (datetime.TotalSeconds >= 15)
            {
                message += $"{Constants.ActiveProblem.DataProblem} {Math.Round((decimal)datetime.TotalSeconds, 0)}   ";
            }
            if (network == 0) 
            {
                message += $"{Constants.ActiveProblem.NetworkProblem} {network}";
            }
            return message;
        }

        public static string MessageCloseSolution(double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, string ramUsedParameters, int sync, TimeSpan datetime, int network) //Сообщение близкое к решению
        {
            string message = "";

            if (ramUsedPercent >= 40.0) //При нагрузке RAM >= 40%
            {
                message += $"{Constants.ActiveProblem.MemoryProblem} {Math.Round((decimal)ramUsedPercent, 0)}% ({ramUsedParameters})   ";
            }
            if (diskUsageParameter >= 40.0) //При нагрузке диска >= 40%
            {
                message += $"{Constants.ActiveProblem.StorageProblem} {diskUsageParameter}%   ";
            }
            if (cpuUsageParameter >= 40.0) //При нагрузке CPU >= 40%
            {
                message += $"{Constants.ActiveProblem.CPUProblem} {cpuUsageParameter}%   ";
            }
            if (sync == 0)
            {
                message += $"{Constants.ActiveProblem.SyncProblem}   ";
            }
            if (datetime.TotalSeconds >= 10)
            {
                message += $"{Constants.ActiveProblem.DataProblem} {Math.Round((decimal)datetime.TotalSeconds, 0)}   ";
            }
            if (network == 0)
            {
                message += $"{Constants.ActiveProblem.NetworkProblem} {network}";
            }
            return message;
        }
    }
}
