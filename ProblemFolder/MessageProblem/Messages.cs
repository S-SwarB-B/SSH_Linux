using SSHProject.DB;
using SSHProject.ParametersFolder.Parameters;
using SSHProject.ProblemFolder;
using SSHProject.ProblemFolder.SearchProblem;
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
        public static void ProblemMessage(ServerMonitoringContext sc, Server server, DateTime startProgram, 
            double ramUsedPercent, 
            double cpuUsageParameter,
            double diskUsageParameter,
            int sync,
            int systime, 
            int network) 
        {
            if (ramUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Low && !SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.MemoryProblem)) //При нагрузке RAM
            {
                int importance = ProblemDefinition.ErrorImportance(ramUsedPercent, 0, 0, -1, -1, -1, sc);
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{Constants.ActiveProblem.MemoryProblem} {Message(importance)}", startProgram);
            }
            else if (ramUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.Low && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.MemoryProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(ramUsedPercent, 0, 0, -1, -1, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.MemoryProblem),
                    importance, $"{Constants.ActiveProblem.MemoryProblem} {Message(importance)}");
            }
            else if (ramUsedPercent <= Constants.ErrorImportanceClass.ImportanceStandart.Low
                && ramUsedPercent >= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.MemoryProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(ramUsedPercent, 0, 0, -1, -1, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.MemoryProblem),
                    importance, $"{Constants.ActiveProblem.MemoryProblem} {Message(importance)}");
            }
            else if (ramUsedPercent <= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.MemoryProblem)) 
            {
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.MemoryProblem),
                    $"{Constants.SolutionsProblem.MessageSuccessfulMemory}", startProgram);
            }

//###########################################################################################################################################################

            if (diskUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low && !SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.StorageProblem)) //При нагрузке диска
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, diskUsageParameter, -1, -1, -1, sc);
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{Constants.ActiveProblem.StorageProblem} {Message(importance)}", startProgram);
            }
            else if (diskUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.StorageProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, diskUsageParameter, -1, -1, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.StorageProblem),
                    importance, $"{Constants.ActiveProblem.StorageProblem} {Message(importance)}");
            }
            else if (diskUsageParameter <= Constants.ErrorImportanceClass.ImportanceStandart.Low
                && diskUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.StorageProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, diskUsageParameter, -1, -1, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.StorageProblem),
                    importance, $"{Constants.ActiveProblem.StorageProblem} {Message(importance)}");
            }
            else if (ramUsedPercent <= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.StorageProblem))
            {
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.StorageProblem),
                    $"{Constants.SolutionsProblem.MessageSuccessfulStorage}", startProgram);
            }

//###########################################################################################################################################################

            if (cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low && !SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.CPUProblem)) //При нагрузке CPU
            {
                int importance = ProblemDefinition.ErrorImportance(0, cpuUsageParameter, 0, -1, -1, -1, sc);
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{Constants.ActiveProblem.CPUProblem} {Message(importance)}", startProgram);
            }
            else if (cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.Low && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.CPUProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, cpuUsageParameter, 0, -1, -1, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.CPUProblem),
                    importance, $"{Constants.ActiveProblem.CPUProblem} {Message(importance)}");
            }
            else if (cpuUsageParameter <= Constants.ErrorImportanceClass.ImportanceStandart.Low
                && cpuUsageParameter >= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.CPUProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, cpuUsageParameter, 0, -1, -1, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.CPUProblem),
                    importance, $"{Constants.ActiveProblem.CPUProblem} {Message(importance)}");
            }
            else if (ramUsedPercent <= Constants.ErrorImportanceClass.ImportanceStandart.VeryLow
                && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.CPUProblem))
            {
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.CPUProblem),
                    $"{Constants.SolutionsProblem.MessageSuccessfulCPU}", startProgram);
            }

//###########################################################################################################################################################

            if (sync == 0 && !SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.SyncProblem)) //При отсутсвии синхронизации
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, sync, -1, -1, sc);
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{Constants.ActiveProblem.SyncProblem}", startProgram);
            }
            else if (sync == 0 && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.SyncProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, sync, -1, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.SyncProblem),
                    importance, $"{Constants.ActiveProblem.SyncProblem}");
            }
            else if (sync == 1 && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.SyncProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, sync, -1, -1, sc);
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.SyncProblem),
                    $"{Constants.SolutionsProblem.MessageSuccessfulSync}", startProgram);
            }

//###########################################################################################################################################################

            if (systime == 0 && !SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.SystimeProblem)) //При отствании времени
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, systime, -1, sc);
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{Constants.ActiveProblem.SystimeProblem}", startProgram);
            }
            else if (systime == 0 && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.SystimeProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, systime, -1, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.SystimeProblem),
                    importance, $"{Constants.ActiveProblem.SystimeProblem}");
            }
            else if (systime == 1 && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.SystimeProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, systime, -1, sc);
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.SystimeProblem),
                    $"{Constants.SolutionsProblem.MessageSuccessfulSystime}", startProgram);
            }

//###########################################################################################################################################################


            if (network == 0 && !SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.NetworkProblem)) //В конце IP не 160
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, network, sc);
                DealingProblem.ProblemAdd(sc, server.Id, importance, $"{Constants.ActiveProblem.NetworkProblem}", startProgram);
            }
            else if (network == 0 && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.NetworkProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, network, sc);
                DealingProblem.ProblemUpdate(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.NetworkProblem),
                    importance, $"{Constants.ActiveProblem.NetworkProblem}");
            }
            else if (network == 1 && SearchProblemServer.SearchInProblemServer(sc, server, Constants.ActiveProblem.NetworkProblem))
            {
                int importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, network, sc);
                DealingProblem.ProblemSolving(sc,
                    SearchProblemServer.SearchCertainProblemServer(sc, server, Constants.ActiveProblem.NetworkProblem),
                    $"{Constants.SolutionsProblem.MessageSuccessfulNetwork}", startProgram);
            }
        }

        private static string Message(int importance)
        {
            if(importance == 5)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Critical}";
            }
            else if (importance == 4)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.VeryHight}";
            }
            else if (importance == 3)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Hight}";
            }
            else if (importance == 2)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Medium}";
            }
            else if (importance == 1)
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.Low}";
            }
            else
            {
                return $"=> {Constants.ErrorImportanceClass.ImportanceStandart.VeryLow}";
            }
        }
    }
}
