using Microsoft.EntityFrameworkCore;
using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class SearchProblemFile
    {
        public static bool SearchInProblemFile(ServerMonitoringContext sc, Server server) //Поиск ошибок отсутсвия файла
        {
            return sc.Errors 
                    .Where(x => x.ServerId == server.Id)
                    .Any(x => x.State == false && x.Message == Constants.ActiveProblem.MessageFailedFile); 
        }
        public static Error SearchCertainProblemFile(ServerMonitoringContext sc, Server server) //Поиск конкретной ошибки отсутсвия файла
        {
            return sc.Errors.First(x => x.ServerId == server.Id
                   && x.State == false && x.Message == Constants.ActiveProblem.MessageFailedFile);
        }
    }
}
