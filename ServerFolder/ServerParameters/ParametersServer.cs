using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SSHProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class ParametersServer
    {
        public static string ParametersForServer(ServerMonitoringContext sc, Server server) //Получение уникальных параметров для этого сервера
        {
            List<string> parameters = sc.ServerParameters //Получение списка этих параметров
                .Where(x => x.ServerId == server.Id)
                .Include(x => x.Parameter)
                .Select(x => x.Parameter.Name)
                .ToList() ?? new List<string>();

            if (parameters.Count > 0) //Если есть уникальные параметры
            {
                string parametersForServer = "";
                foreach (string parameter in parameters) 
                {
                    parametersForServer += " " + parameter; //Формирование строки, которая будет отправлять запросы к серверу
                }
                return parametersForServer; //Возврат строки
            }
            else //Если нет
            {
                return "";
            }
        }
    }
}
