using Renci.SshNet.Messages;
using SSHProject.DB;
using SSHProject.ProblemFolder.SearchProblem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ProblemFolder
{
    internal class ProblemDefinition
    {
        public static int ErrorImportance(double memoryUsedPercent, double cpuUsageParameter, double storageUsageParameter, int sync, int systime, int network, ServerMonitoringContext pc) //Критичность ошибки
        {
            if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Critical
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Critical
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Critical
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 5 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 5 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 5 && systime == 0)) //Критическая
            {
                return 5;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.VeryHight
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.VeryHight
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.VeryHight
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 4 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 4 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 4 && systime == 0)) //Очень высокая
            {
                return 4;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Hight
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Hight
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Hight
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 3 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 3 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 3 && systime == 0)) //Высокая
            {
                return 3;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Medium
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Medium
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Medium
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 2 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 2 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 2 && systime == 0)) //Умеренная
            {
                return 2;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Low
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 1 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 1 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 1 && systime == 0)) //Умеренная
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
