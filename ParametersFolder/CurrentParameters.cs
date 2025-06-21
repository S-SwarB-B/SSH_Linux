using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder
{
    internal class CurrentParameters
    {
        public static void AddParametersInDataBase(SSHContext pc, Guid idServer_, double ramUsedPercent, double cpuUsageParameter, double diskUsageParameter) //Заполнение параметров
        {
            try
            {
                Parameter newParameter = new Parameter()
                {
                    CreatedAt = DateTime.Now,
                    IdServer = idServer_,
                    RamMb = Convert.ToInt32(Math.Round((decimal)ramUsedPercent, 0)),
                    CpuPercent = Convert.ToInt32(Math.Round((decimal)cpuUsageParameter, 0)),
                    RomMb = Convert.ToInt32(Math.Round((decimal)diskUsageParameter, 0))
                };
                pc.Parameters.Add(newParameter); //Добавление в бд
                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
