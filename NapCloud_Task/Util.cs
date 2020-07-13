using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapCloud_Task
{
    class Util
    {
        public static string getCurrentTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMdd_HHmmssffff");
        }
    }
}
