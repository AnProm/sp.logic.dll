using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogManager
    {
        public enum type
        {
            INFO,
            WARN, 
            ERROR
        }

        private String logInfo;

        public LogManager() => this.logInfo = "";
        

        public void log(type type, string eventInfo) 
        {
            logInfo += DateTime.Now.ToString() + "  [" + type.ToString() + "]  " + eventInfo + "\n";
        }
        public String getLog()
        {
            return logInfo;
        }
    }
}
