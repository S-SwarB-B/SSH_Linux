using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class ProblemDeleteInterval
    {
        public static void DeleteSolutionProblem(SSHContext sc, int deleteSolutionProblemIntervalDay)
        {
            var problems = sc.Problems.Where(x => DateTime.Now - x.DateProblemSolution > new TimeSpan(deleteSolutionProblemIntervalDay, 0, 0, 0) && x.StatusProblem == true).ToList();
            foreach (var problem in problems)
            {
                sc.Problems.Remove(problem);
            }
        }
        public static void DeleteParameters(SSHContext sc, int deleteParametersIntervalHour)
        {
            var parameters = sc.Parameters.Where(x => DateTime.Now - x.CreatedAt > new TimeSpan(deleteParametersIntervalHour, 0, 0)).ToList();
            foreach (var parameter in parameters)
            {
                sc.Parameters.Remove(parameter);
            }
        }
    }
}
