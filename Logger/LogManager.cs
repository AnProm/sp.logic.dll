using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class LogManager
    {
        public enum type
        {
            INFO,
            WARN
        }

        private String logInfo;

        LogManager()
        {
            logInfo = "";
        }

        public void log(type type, string eventInfo) 
        {
            logInfo = logInfo + DateTime.Now.ToString() + "  [" + type.ToString() + "]  " + eventInfo + "\n";
        }
        public String getLog()
        {
            return logInfo;
        }
    }
}
