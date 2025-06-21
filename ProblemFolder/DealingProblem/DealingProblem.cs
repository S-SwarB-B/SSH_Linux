using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class DealingProblem
    {
        public static void ProblemAdd(SSHContext pc, Guid IdServer_, int ErrorImportance_, string message) //Добавление новой проблемы
        {
            try
            {
                Problem newProblem = new Problem()
                {
                    DateTimeProblem = DateTime.Now,
                    DateProblemSolution = null,
                    ErrorImportance = ErrorImportance_,
                    StatusProblem = false,
                    IdServer = IdServer_,
                    MessageProblem = message
                };
                pc.Problems.Add(newProblem);
                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ProblemUpdate(SSHContext pc, Problem problem, int ErrorImportance_, string message) //Измение существующей проблемы
        {
            try
            {
                problem.MessageProblem = message;
                problem.ErrorImportance = ErrorImportance_;

                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ProblemSolving(SSHContext pc, Problem problem, string message) //Решение проблеммы
        {
            try
            {
                problem.MessageProblem = message;
                problem.DateProblemSolution = DateTime.Now;
                problem.StatusProblem = true;

                pc.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
