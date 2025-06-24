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
        public static int ErrorImportance(
            int memoryUsedPercent, 
            int cpuUsageParameter, 
            int storageUsageParameter,
            int sync, 
            int systime, 
            int network,
            int demonDocker,
            int containerSDU,
            int fileStorage,
            int containerPostgres,
            int containerETCD,
            int ddmWebAdmin,
            int ddmWeb,
            int ddmWebApi,
            ServerMonitoringContext pc,
            Server server,
            string message) //Критичность ошибки
        {
            if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Critical
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Critical
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Critical
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 5 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 5 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 5 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DemonDocker == 5 && demonDocker == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerSDU == 5 && containerSDU == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.FileStorage == 5 && fileStorage == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerPostrges == 5 && containerPostgres == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ConteinerETCD == 5 && containerETCD == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebAdmin == 5 && ddmWebAdmin == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWeb == 5 && ddmWeb == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebApi == 5 && ddmWebApi == 0)) //Критическая
            {
                return 5;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.VeryHight
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.VeryHight
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.VeryHight
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 4 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 4 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 4 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 4 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 4 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 4 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DemonDocker == 4 && demonDocker == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerSDU == 4 && containerSDU == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.FileStorage == 4 && fileStorage == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerPostrges == 4 && containerPostgres == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ConteinerETCD == 4 && containerETCD == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebAdmin == 4 && ddmWebAdmin == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWeb == 4 && ddmWeb == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebApi == 4 && ddmWebApi == 0)) //Очень высокая
            {
                return 4;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Hight
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Hight
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Hight
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 3 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 3 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 3 && systime == 0) 
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 3 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 3 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 3 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DemonDocker == 3 && demonDocker == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerSDU == 3 && containerSDU == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.FileStorage == 3 && fileStorage == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerPostrges == 3 && containerPostgres == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ConteinerETCD == 3 && containerETCD == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebAdmin == 3 && ddmWebAdmin == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWeb == 3 && ddmWeb == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebApi == 3 && ddmWebApi == 0)) //Высокая
            {
                return 3;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Medium
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Medium
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Medium
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 2 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 2 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 2 && systime == 0) 
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 2 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 2 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 2 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DemonDocker == 2 && demonDocker == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerSDU == 2 && containerSDU == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.FileStorage == 2 && fileStorage == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerPostrges == 2 && containerPostgres == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ConteinerETCD == 2 && containerETCD == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebAdmin == 2 && ddmWebAdmin == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWeb == 2 && ddmWeb == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebApi == 2 && ddmWebApi == 0)) //Умеренная
            {
                return 2;
            }
            else if (memoryUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Low
                || cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low
                || storageUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 1 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 1 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 1 && systime == 0) 
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 1 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 1 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 1 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DemonDocker == 1 && demonDocker == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerSDU == 1 && containerSDU == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.FileStorage == 1 && fileStorage == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerPostrges == 1 && containerPostgres == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ConteinerETCD == 1 && containerETCD == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebAdmin == 1 && ddmWebAdmin == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWeb == 1 && ddmWeb == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebApi == 1 && ddmWebApi == 0)) //Низкая
            {
                return 1;
            }
            else if (SearchProblemServer.SearchInProblemServer(pc, server, message) 
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 0 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 0 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 0 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Sync == 0 && sync == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.Network == 0 && network == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.SysTime == 0 && systime == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DemonDocker == 0 && demonDocker == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerSDU == 0 && containerSDU == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.FileStorage == 0 && fileStorage == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ContainerPostrges == 0 && containerPostgres == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.ConteinerETCD == 0 && containerETCD == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebAdmin == 0 && ddmWebAdmin == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWeb == 0 && ddmWeb == 0)
                || (Constants.ErrorImportanceClass.ImportanceAdditional.DDMWebApi == 0 && ddmWebApi == 0)) //Очень низкая
            {
                return 0;
            }
            else 
            { 
                return -1; 
            }
        }
    }
}
