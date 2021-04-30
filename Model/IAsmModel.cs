using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    /// <summary>
    /// Интерфейс модели для ассемблерных вставок
    /// </summary>
    public interface IAsmModel
    {
        /// <summary>
        /// Возвращает результат выполнения вставок или признак переполнения
        /// </summary>
        /// <param name="a">Число а</param>
        /// <param name="b">Число b</param>
        /// <param name="mode">Режим работы</param>
        /// <returns></returns>
        public string[] DoOperation(string a, string b, bool mode);
    }
}
