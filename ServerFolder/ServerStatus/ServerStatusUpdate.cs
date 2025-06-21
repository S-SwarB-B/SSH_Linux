using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class ServerStatusUpdate
    {
        public static void ServerStatusUpd(SSHContext pc, Server server, bool serverStatus) //Обновление статуса проблемы
        {
            try
            {
                server.ServerStatus = serverStatus;

                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
