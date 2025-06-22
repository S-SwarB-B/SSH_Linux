using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class Storage
    {
        public static double StorageUsedParameter(string[] result, int countInResult)
        {
            string[] storageUsage = result[countInResult].Split($"{Constants.Tags.TagSTORAGE} ");
            return Convert.ToDouble(storageUsage[1]);
        }
    }
}

