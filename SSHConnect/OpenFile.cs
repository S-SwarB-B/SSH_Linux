using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class OpenFile
    {
        public static SshCommand? StartServerMonitoringAgent(SshClient serverConnect, string path, ref string? cmdReturnStr)
        {
            SshCommand? cmd = null;
            try
            {
                cmd = serverConnect.CreateCommand(path); //Ввод команды в CMD
                cmdReturnStr = cmd.Execute();
            }
            catch
            { 
            
            }
            return cmd;
        }
    }
}
