using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Класс логирования базы данных
    /// </summary>
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
        
        /// <summary>
        /// Логировать какое-либо событие
        /// </summary>
        /// <param name="type">Тип сообщения</param>
        /// <param name="eventInfo">Информация о событии</param>
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
