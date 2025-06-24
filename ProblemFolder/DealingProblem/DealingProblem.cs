using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class DealingProblem
    {
        public static void ProblemAdd(ServerMonitoringContext pc, Guid IdServer_, int ErrorImportance_, string message, DateTime startProgram) //Добавление новой проблемы
        {
            try
            {
                Error newProblem = new Error()
                {
                    CreatedAt = startProgram,
                    FinishedAt = null,
                    Importance = ErrorImportance_,
                    State = false,
                    ServerId = IdServer_,
                    Message = message
                };
                pc.Errors.Add(newProblem);
                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ProblemUpdate(ServerMonitoringContext pc, Error problem, int ErrorImportance_, string message) //Измение существующей проблемы
        {
            try
            {
                problem.Message = message;
                problem.Importance = ErrorImportance_;

                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ProblemSolving(ServerMonitoringContext pc, Error problem, string message, DateTime startProgram) //Решение проблеммы
        {
            try
            {
                problem.Message = message;
                problem.FinishedAt = startProgram;
                problem.State = true;

                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
