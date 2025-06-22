using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class ParametersServer
    {
        public static string ParametersForServer(SSHContext sc, Server server)
        {
            List<string> parameters = sc.AdditionalParametersservers
                .Where(x => x.IdServer == server.IdServer)
                .Include(x => x.IdAdditionalParameterNavigation)
                .Select(x => x.IdAdditionalParameterNavigation.Name).ToList();

            if (parameters.Count > 0)
            {
                string parametersForServer = " ";
                foreach (string parameter in parameters) 
                {
                    parametersForServer += " " + parameter;
                }
                return parametersForServer;
            }
            else
            {
                return "";
            }
        }
    }
}
