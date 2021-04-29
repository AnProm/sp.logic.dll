using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Базовый класс базы данных
    /// </summary>
    public class DataBaseObject 
    {
        public virtual Guid SystemId { get; set; }

    }
}
