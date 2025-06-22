using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHProject
{
    internal class SysTime
    {
        public static int SysTimeParameter(string[] result, int countInResult)
        {
            string[] SysTimeInfo = result[countInResult].Split($"{Constants.Tags.TagSYSTIME} ");
            return Convert.ToInt32(SysTimeInfo[1]);
        }
    }
}
