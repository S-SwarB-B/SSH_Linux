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
            ref int memoryUsedPercent,                
            ref int storageUsageParameter,            
            ref int cpuUsageParameter,                
            ref int sync,                             
            ref int systime,                             
            ref int network,
            ref int demonDocker,
            ref int containerSDU,
            ref int fileStorage,
            ref int containerPostgres,
            ref int containerETCD,
            ref int ddmWebAdmin,
            ref int ddmWeb,
            ref int ddmWebApi
            )                                           
        {
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Contains(Constants.Tags.TagMEMORY)) //MEMORY
                {
                    memoryUsedPercent = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagMEMORY);
                }
                else if (result[i].Contains(Constants.Tags.TagSTORAGE)) //STORAGE
                {
                    storageUsageParameter = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagSTORAGE);
                }
                else if (result[i].Contains(Constants.Tags.TagCPU)) //CPU
                {
                    cpuUsageParameter = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagCPU);
                }
                else if (result[i].Contains(Constants.Tags.TagSYSTIME)) //SYSTIME
                {
                    systime = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagSYSTIME);
                }
                else if (result[i].Contains(Constants.Tags.TagSYNCTIME)) //SYNCTIME
                {
                    sync = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagSYNCTIME);
                }
                else if (result[i].Contains(Constants.Tags.TagNETWORK)) //NETWORK
                {
                    network = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagNETWORK);
                }
                else if (result[i].Contains(Constants.Tags.TagDEMONDOCKER)) //DEMONDOCKER
                {
                    demonDocker = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagDEMONDOCKER);
                }                                                                                                                
                else if (result[i].Contains(Constants.Tags.TagCONSDU)) //CONSDU                                                  
                {                                                                                                                
                    containerSDU = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagCONSDU);                              
                }                                                                                                                                                                                                                               //ddmWebApi
                else if (result[i].Contains(Constants.Tags.TagFILESTORAGE)) //FILESTORAGE
                {
                    fileStorage = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagFILESTORAGE);
                }
                else if (result[i].Contains(Constants.Tags.TagCONPOSTGRES)) //CONPOSTGRES
                {
                    containerPostgres = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagCONPOSTGRES);
                }
                else if (result[i].Contains(Constants.Tags.TagCONETCD)) //CONETCD
                {
                    containerETCD = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagCONETCD);
                }
                else if (result[i].Contains(Constants.Tags.TagDDMWEBADMIN)) //DDMWEBADMIN
                {
                    ddmWebAdmin = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagDDMWEBADMIN);
                }
                else if (result[i].Contains(Constants.Tags.TagDDMWEB)) //TagDDMWEB
                {
                    ddmWeb = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagDDMWEB);
                }
                else if (result[i].Contains(Constants.Tags.TagDDMWEBAPI)) //DDMWEBAPI
                {
                    ddmWebApi = ParameterReceive.MetricAPP(result, i, Constants.Tags.TagDDMWEBAPI);
                }
            }
        }
    }
}
