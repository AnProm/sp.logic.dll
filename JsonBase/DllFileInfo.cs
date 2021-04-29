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

        public override string ToString()
        {
            return "Класс DllFileInfo " +  FileName + " " + FileVersion + " "  + DateOfLastEdit.ToString();
        }
        public DllFileInfo() { }

        public DllFileInfo(string FileName, string FileVersion, DateTime DateOfLastEdit)
        {
            this.SystemId = Guid.NewGuid();  this.FileName = FileName; this.FileVersion = FileVersion; this.DateOfLastEdit = DateOfLastEdit;
        }

        /// <summary>
        /// Конструктор для объекта информации по записи о файле. 
        /// </summary>
        /// <param name="guid">Уникальный для БД id</param>
        /// <param name="FileName">Имя файла</param>
        /// <param name="FileVersion">Версия файла</param>
        /// <param name="DateOfLastEdit">Дата последнего редактирования</param>
        public DllFileInfo(Guid guid, string FileName, string FileVersion, DateTime DateOfLastEdit)
        {
            this.SystemId = guid; this.FileName = FileName; this.FileVersion = FileVersion; this.DateOfLastEdit = DateOfLastEdit;
        }
    }
}

