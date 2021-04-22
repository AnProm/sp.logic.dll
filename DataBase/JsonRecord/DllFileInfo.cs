using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Класс для базы данных содержащих информацию по записи о dll файле. Имя файла, версию и дату последнего редактирования.  
    /// </summary>
    public class DllFileInfo: DataBaseObject
    {
        public virtual string FileName { get; set; }
        public virtual string FileVersion { get; set; }
        public virtual DateTime DateOfLastEdit { get; set; }
    }
}

