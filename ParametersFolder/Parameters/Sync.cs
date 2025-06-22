using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject.ParametersFolder.Parameters
{
    internal class Sync
    {
        public static int SyncParameter(string[] result, int countInResult) //SYNC
        {
            string[] syncInfo = result[countInResult].Split($"{Constants.Tags.TagSYNCTIME} "); //Обработка результата
            return Convert.ToInt32(syncInfo[1]);                                               //
        }
    }
}
