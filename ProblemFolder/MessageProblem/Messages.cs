using SSHProject.DB;
using SSHProject.ParametersFolder.Parameters;
using SSHProject.ProblemFolder;
using SSHProject.ProblemFolder.MessageProblem;
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
            int ramUsedPercent,
            int cpuUsageParameter,
            int diskUsageParameter,
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
            int ddmWebApi
            )
        {
            int importance = ProblemDefinition.ErrorImportance(ramUsedPercent, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.MemoryProblem);
            MessageProblemStandart.StandartMessage(sc, server,
                ramUsedPercent, importance,
                Constants.ActiveProblem.MemoryProblem, Constants.SolutionsProblem.MessageSuccessfulMemory,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, diskUsageParameter, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.StorageProblem);
            MessageProblemStandart.StandartMessage(sc, server,
                diskUsageParameter, importance,
                Constants.ActiveProblem.StorageProblem, Constants.SolutionsProblem.MessageSuccessfulStorage,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, cpuUsageParameter, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.CPUProblem);
            MessageProblemStandart.StandartMessage(sc, server,
                cpuUsageParameter, importance,
                Constants.ActiveProblem.CPUProblem, Constants.SolutionsProblem.MessageSuccessfulCPU,
                startProgram
                );
            
            importance = ProblemDefinition.ErrorImportance(0, 0, 0, sync, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.SyncProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                sync, importance,
                Constants.ActiveProblem.SyncProblem, Constants.SolutionsProblem.MessageSuccessfulSync,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, systime, -1, -1, -1, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.SyncProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                network, importance,
                Constants.ActiveProblem.SystimeProblem, Constants.SolutionsProblem.MessageSuccessfulSystime,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, network, -1, -1, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.SyncProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                systime, importance,
                Constants.ActiveProblem.NetworkProblem, Constants.SolutionsProblem.MessageSuccessfulNetwork,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, demonDocker, -1, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.DemonDockerProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                demonDocker, importance,
                Constants.ActiveProblem.DemonDockerProblem, Constants.SolutionsProblem.MessageSuccessfulDemonDocker,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, -1, containerSDU, -1, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.SduProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                containerSDU, importance,
                Constants.ActiveProblem.SduProblem, Constants.SolutionsProblem.MessageSuccessfulSdu,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, -1, -1, fileStorage, -1, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.FileStorageProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                fileStorage, importance,
                Constants.ActiveProblem.FileStorageProblem, Constants.SolutionsProblem.MessageSuccessfulFileStorage,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, -1, -1, -1, containerPostgres, -1, -1, -1, -1, sc, server, Constants.ActiveProblem.ContainerPostgresProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                containerPostgres, importance,
                Constants.ActiveProblem.ContainerPostgresProblem, Constants.SolutionsProblem.MessageSuccessfulContainerPostgres,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, -1, -1, -1, -1, containerETCD, -1, -1, -1, sc, server, Constants.ActiveProblem.ETCDProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                containerETCD, importance,
                Constants.ActiveProblem.ETCDProblem, Constants.SolutionsProblem.MessageSuccessfulETCD,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, ddmWebAdmin, -1, -1, sc, server, Constants.ActiveProblem.DDMWebAdminProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                ddmWebAdmin, importance,
                Constants.ActiveProblem.DDMWebAdminProblem, Constants.SolutionsProblem.MessageSuccessfulDDMWebAdmin,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, ddmWeb, -1, sc, server, Constants.ActiveProblem.DDMWebUiProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                ddmWeb, importance,
                Constants.ActiveProblem.DDMWebUiProblem, Constants.SolutionsProblem.MessageSuccessfulDDMWebUi,
                startProgram
                );

            importance = ProblemDefinition.ErrorImportance(0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, ddmWebApi, sc, server, Constants.ActiveProblem.DDMWebApiProblem);
            MessageProblemAdditional.AdditionalMessage(sc, server,
                ddmWebApi, importance,
                Constants.ActiveProblem.DDMWebApiProblem, Constants.SolutionsProblem.MessageSuccessfulDDMWebApi,
                startProgram
                );

        }
    }
}
