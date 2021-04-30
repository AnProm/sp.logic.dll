using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    /// <summary>
    /// Модель анализатора
    /// </summary>
    public interface IAnalyseModel
    {
        /// <summary>
        /// Метод возвращающий строку как результат анализа
        /// </summary>
        /// <param name="inpString">Строка для анализа</param>
        /// <param name="mode">True = foreach, False = if/else</param>
        /// <returns></returns>
        public string getResult(string inpString, bool mode);
    }
}
