using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class ServerStatusUpdate
    {
        public static void ServerStatusUpd(ServerMonitoringContext pc, Server server, bool serverStatus) //Обновление статуса проблемы
        {
            try
            {
                server.State = serverStatus;

                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
