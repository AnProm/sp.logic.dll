using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    /// <summary>
    /// Класс, содержащий методы для парсинга синтаксических конструкций
    /// </summary>
    public class AnalyseModel : IAnalyseModel
    {
        /// <summary>
        /// Исполняет переданную синтаксическую конструкцию и возвращает результат, иначе возвращает текст с ошибкой
        /// </summary>
        /// <param name="inputString">Синтаксическая конструкция</param>
        /// <param name="mode">Выбор режима синтаксической кострукции 1-foreach 0-if</param>
        /// <returns></returns>
        public string getResult(string inputString, bool mode)
        {
            string result = "Ошибка! Неподдерживаемая конструкция!";
            string[] wordArray = inputString.Split(' ');
            if (wordArray.Length == 0)
            {
                return "Ошибка! Введенная строка пуста.";
            }

            int index = 0;
            string currentWord;
            if (!mode)
            {
                if (wordArray[index++] == "foreach" && wordArray[index][0] == '(')
                {
                    currentWord = wordArray[index].Trim('(');
                    if (currentWord == "") { currentWord = wordArray[++index]; }
                    if (String.Compare(currentWord, "byte") == 0) { return Counter<byte>(wordArray, index); }
                    else if (currentWord == "sbyte" || currentWord == "SByte") { return Counter<sbyte>(wordArray, index); }
                    else if (currentWord == "short" || currentWord == "Int16") { return Counter<short>(wordArray, index); }
                    else if (currentWord == "ushort" || currentWord == "UInt16") { return Counter<ushort>(wordArray, index); }
                    else if (currentWord == "int" || currentWord == "Int32") { return Counter<int>(wordArray, index); }
                    else if (currentWord == "uint" || currentWord == "UInt32") { return Counter<uint>(wordArray, index); }
                    else if (currentWord == "long" || currentWord == "Int64") { return Counter<long>(wordArray, index); }
                    else if (currentWord == "ulong" || currentWord == "UInt64") { return Counter<ulong>(wordArray, index); }
                    else if (currentWord == "float" || currentWord == "Single") { return Counter<float>(wordArray, index); }
                    else if (currentWord == "double" || currentWord == "Double") { return Counter<double>(wordArray, index); }
                    else if (currentWord == "decimal" || currentWord == "Decimal") { return Counter<decimal>(wordArray, index); }
                    else if (currentWord == "char" || currentWord == "Char") { return Counter<char>(wordArray, index); }
                    else if (currentWord == "string" || currentWord == "String") { return Counter<string>(wordArray, index); }
                    else { result = "Ошибка! Неподдерживаемый тип данных"; }
                }
                return result;
            }
            else
            {
                if (wordArray[index++] == "if" && wordArray[index][0] == '(')
                {
                    currentWord = wordArray[index].Trim('(');
                    if (currentWord == "") { currentWord = wordArray[++index]; }
                    if (currentWord.TryParseGeneric<bool>()) { return Selector<bool>(wordArray, index); }
                    else if (currentWord.TryParseGeneric<long>()) { return Selector<long>(wordArray, index); }
                    else if (currentWord.TryParseGeneric<double>()) { return Selector<double>(wordArray, index); }
                    else if (currentWord.TryParseGeneric<string>()) { return Selector<string>(wordArray, index); }
                    else { result = "Ошбика! Неподдерживаемый тип данных"; }
                }
                return result;
            }

        }
        /// <summary>
        /// Обрабатывает foreach конструкцию, возвращает количество итераций, либо ошибку
        /// </summary>
        /// <typeparam name="T"> Тип проверяемых в конструкции данных </typeparam>
        /// <param name="words"> Массив, содержащий в себе конструкцию </param>
        /// <param name="index"> Индекс, с которого начинается парсинг </param>
        /// <returns></returns>
        private string Counter<T>(string[] words, int index)
        {
            string result = "Ошибка! разбора конструкции FOREACH";
            try
            {
                index++;//here is place for name of variable
                if (words[++index] != "in") { return result; }
                if (words[++index] != "{") { return result; }

                int counter = 0;
                while (true || index < words.Length)
                {
                    index++;
                    if (Pars.TryParseGeneric<T>(words[index].Trim(',')))
                    {
                        counter++;
                    }
                    else if (Pars.TryParseGeneric<T>(words[index].Trim('}')))
                    {
                        counter++;
                        break;
                    }
                    else if (words[index] == "}")
                    {
                        break;
                    }
                    else { return result; }
                }
                if (words[++index] != ")" || words[++index] != "{" || words[++index] != "}") { return result; }

                result = "Отработало: " + counter.ToString() + " раз";
            }
            catch
            {
                result = "Ошибка! Не вышло разборать конструкцию FOREACH";
            }
            return result;
        }
        /// <summary>
        /// Обрабатывает if/else конструкцию, возвращает номер выполненной ветви, либо ошибку
        /// </summary>
        /// <typeparam name="T"> Тип сравниваемых в конструкции данных</typeparam>
        /// <param name="words"> Массив, содержащий в себе конструкцию </param>
        /// <param name="index"> Индекс, с которого начинается парсинг</param>
        /// <returns></returns>
        public string Selector<T>(string[] words, int index)
        {
            string result = "Ошибка! Не вышло разборать конструкцию IF";
            try
            {
                T firstVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                T secondVariable;

                if (firstVariable is Boolean)
                {
                    if (words[index] == ")")
                    {
                        index++;
                        if ((bool)(object)firstVariable == true) { result = "Вошло в ветку №" + "1"; }
                        else { result = "Вошло в ветку №" + "2"; }
                    }
                    else if (words[index] == "==")
                    {
                        if (words[++index].TryParseGeneric<bool>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((bool)(object)firstVariable == (bool)(object)secondVariable) { result = "Вошло в ветку №" + "1"; }
                            else { result = "Вошло в ветку №" + "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (words[++index].TryParseGeneric<bool>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((bool)(object)firstVariable != (bool)(object)secondVariable) { result = "Вошло в ветку №" + "1"; }
                            else { result = "Вошло в ветку №" + "2"; }
                        }
                    }
                    else { result = "Оишбка! Неверный синтаксис"; }
                }

                else if (firstVariable is long)
                {
                    if (words[index] == "==")
                    {
                        if (words[++index].TryParseGeneric<long>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((long)(object)firstVariable == (long)(object)secondVariable) { result = "Вошло в ветку №" + "1"; }
                            else { result = "Вошло в ветку №" + "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (words[++index].TryParseGeneric<long>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (long)(object)firstVariable != (long)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else if (words[index] == ">")
                    {
                        if (words[++index].TryParseGeneric<long>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((long)(object)firstVariable > (long)(object)secondVariable) { result = "Вошло в ветку №" + "1"; }
                            else { result = "Вошло в ветку №" + "2"; }
                        }
                    }
                    else if (words[index] == "<")
                    {
                        if (words[++index].TryParseGeneric<long>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (long)(object)firstVariable < (long)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else if (words[index] == "<=")
                    {
                        if (words[++index].TryParseGeneric<long>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (long)(object)firstVariable <= (long)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else if (words[index] == ">=")
                    {
                        if (words[++index].TryParseGeneric<long>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (long)(object)firstVariable >= (long)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else { result = "Ошбика! Неверный синтаксис"; }
                }

                else if (firstVariable is double)
                {
                    if (words[index] == "==")
                    {
                        if (words[++index].TryParseGeneric<double>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((double)(object)firstVariable == (double)(object)secondVariable) { result = "Вошло в ветку №" + "1"; }
                            else { result = "Вошло в ветку №" + "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (words[++index].TryParseGeneric<double>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (double)(object)firstVariable != (double)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else if (words[index] == ">")
                    {
                        if (words[++index].TryParseGeneric<double>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((double)(object)firstVariable > (double)(object)secondVariable) { result = "Вошло в ветку №" + "1"; }
                            else { result = "Вошло в ветку №" + "2"; }
                        }
                    }
                    else if (words[index] == "<")
                    {
                        if (words[++index].TryParseGeneric<double>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (double)(object)firstVariable < (double)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else if (words[index] == "<=")
                    {
                        if (words[++index].TryParseGeneric<double>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (double)(object)firstVariable <= (double)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else if (words[index] == ">=")
                    {
                        if (words[++index].TryParseGeneric<double>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (double)(object)firstVariable >= (double)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else { result = "Неверный синтаксис"; }
                }

                else if (firstVariable is string)
                {
                    if (words[index] == "==")
                    {
                        if (words[++index].TryParseGeneric<string>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((string)(object)firstVariable == (string)(object)secondVariable) { result = "Вошло в ветку №" + "1"; }
                            else { result = "Вошло в ветку №" + "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (words[++index].TryParseGeneric<string>())
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = (string)(object)firstVariable != (string)(object)secondVariable ? "Вошло в ветку №" + "1" : "Вошло в ветку №" + "2";
                        }
                    }
                    else { result = "Ошбика! Неверный синтаксис"; }
                }
                if (words[index++] != ")" || words[index++] != "{") { result = "Ошбика! Неверный синтаксис"; };
                //ниже - обработка else meow
                if (words.Contains("else"))
                {
                    int elseIndex = Array.IndexOf(words, "else");
                    if (elseIndex < index)
                    {
                        result = "Ошбика! Неверный синтаксис";
                    }
                    else
                    {
                        if (words[elseIndex - 1] != "}" || words[elseIndex + 1] != "{" || words[words.Length - 1] != "}")
                        {
                            result = "Ошбика! Неверный синтаксис";
                        }
                    }
                }
                else
                {
                    if (words[words.Length - 1] != "}")
                    {
                        result = "Ошибка! Неверный синтаксис";
                    }
                    else
                    {
                        ////if (result == "Вошло в ветку №" + "2")
                        ////{
                        ////    result = "Условие не выполнилось";
                        ////}
                    }
                }
            }
            catch
            {
                result = "Ошбика! Неверный синтаксис";
            }
            return result;
        }
    }
    static class Pars
    {
        /// <summary>
        /// Проверяет, возможно ли привести данные к указанному типу
        /// </summary>
        /// <typeparam name="T"> Тип, к которому необходимо привести </typeparam>
        /// <param name="input"> Входная строка </param>
        /// <returns></returns>
        public static bool TryParseGeneric<T>(this string input)
        {
            try
            {
                System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    } 
}
