using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder
{
    internal class CurrentParameters
    {
        public static void AddParametersInDataBase(ServerMonitoringContext pc, Guid idServer_, double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter, DateTime startProgram) //Заполнение параметров
        {
            try
            {
                Metric newParameter = new Metric() //Заполнение нового параметра
                {
                    CreatedAt = startProgram,
                    ServerId = idServer_,
                    Ram = Convert.ToInt32(Math.Round((decimal)ramUsedPercent, 0)),
                    Cpu = Convert.ToInt32(Math.Round((decimal)cpuUsageParameter, 0)),
                    Strorage = Convert.ToInt32(Math.Round((decimal)diskUsageParameter, 0))
                };
                pc.Metrics.Add(newParameter); //Добавление в бд
                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
