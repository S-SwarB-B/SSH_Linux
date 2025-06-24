using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class ProblemDeleteInterval
    {
        public static void DeleteSolutionProblem(ServerMonitoringContext sc, int deleteSolutionProblemIntervalDay) //Очистка решенных проблем через определенный промежуток времени
        {
            var problems = sc.Errors.Where(x => DateTime.Now - x.FinishedAt > new TimeSpan(deleteSolutionProblemIntervalDay, 0, 0, 0) && x.State == true).ToList();
            foreach (var problem in problems)
            {
                sc.Errors.Remove(problem);
            }
        }
        public static void DeleteParameters(ServerMonitoringContext sc, int deleteParametersIntervalHour) //Очистка параметров через определенный промежуток времени
        {
            var parameters = sc.Metrics.Where(x => DateTime.Now - x.CreatedAt > new TimeSpan(deleteParametersIntervalHour, 0, 0)).ToList();
            foreach (var parameter in parameters)
            {
                sc.Metrics.Remove(parameter);
            }
        }
    }
}
