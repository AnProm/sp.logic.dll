using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Класс, содержащий методы для парсинга синтаксических конструкций
    /// </summary>
    public static class SyntacticConstructions
    {
        //0-foreach
        //1-if
        /// <summary>
        /// Исполняет переданную синтаксическую конструкцию и возвращает результат, иначе возвращает текст с ошибкой
        /// </summary>
        /// <param name="inputString">Синтаксическая конструкция</param>
        /// <param name="mode">Выбор режима синтаксической кострукции</param>
        /// <returns></returns>
        public static string getResult(string inputString, byte mode)
        {
            string result = "Error!";
            string[] wordArray = inputString.Split(' ');
            if (wordArray.Length == 0)
            {
                return "The entered string is empty!";
            }

            int index = 0;
            string currentWord;
            switch (mode)
            {
                case 0:
                    if (wordArray[index][0] == '(')
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
                        else { result="Неподдерживаемый тип данных"; }
                    }
                    return result;
                case 1:
                    if (wordArray[index++]=="if" && wordArray[index][0] == '(')
                    {
                        currentWord = wordArray[index].Trim('(');
                        if (currentWord == "") { currentWord = wordArray[++index]; }
                        if (TryParseGeneric<bool>(currentWord)) { return Selector<bool>(wordArray, index); }
                        else if (TryParseGeneric<long>(currentWord)) { return Selector<long>(wordArray, index); }
                        else if (TryParseGeneric<double>(currentWord)) { return Selector<double>(wordArray, index); }
                        else if (TryParseGeneric<string>(currentWord)) { return Selector<string>(wordArray, index); }
                        else { result = "Неподдерживаемый тип данных"; }
                    }
                    return result;
            }
            return result;
            
        }
        /// <summary>
        /// Обрабатывает foreach конструкцию, возвращает количество итераций, либо ошибку
        /// </summary>
        /// <typeparam name="T"> Тип проверяемых в конструкции данных </typeparam>
        /// <param name="words"> Массив, содержащий в себе конструкцию </param>
        /// <param name="index"> Индекс, с которого начинается парсинг </param>
        /// <returns></returns>
        private static string Counter<T>(string[] words, int index)
        {
            string result = "Ошибка разбора конструкции FOREACH";
            if (words[++index] != "in") { return result; }
            if (words[++index] != "{") { return result; }

            int counter = 0;
            while (true || index<words.Length)
            {
                index++;
                if (TryParseGeneric<T>(words[index].Trim(',')))
                {
                    counter++;
                }
                else if (TryParseGeneric<T>(words[index].Trim('}'))) 
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
            if (words[++index] != ")" || words[++index] != "{" || words[words.Length - 1] != "}") { return result; }

            result = counter.ToString();
            return result;
        }
        /// <summary>
        /// Обрабатывает if/else конструкцию, возвращает номер выполненной ветви, либо ошибку
        /// </summary>
        /// <typeparam name="T"> Тип сравниваемых в конструкции данных</typeparam>
        /// <param name="words"> Массив, содержащий в себе конструкцию </param>
        /// <param name="index"> Индекс, с которого начинается парсинг</param>
        /// <returns></returns>
        public static string Selector<T>(string[] words, int index)
        {
            string result = "Ошибка разбора конструкции IF";
            try
            {
                T firstVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                T secondVariable;

                if (firstVariable is Boolean)
                {
                    if (words[index] == ")")
                    {
                        index++;
                        if ((bool)(object)firstVariable == true) { result = "1"; }
                        else { result = "2"; }
                    }
                    else if (words[index] == "==")
                    {
                        if (TryParseGeneric<bool>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((bool)(object)firstVariable == (bool)(object)secondVariable) { result = "1"; }
                            else { result = "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (TryParseGeneric<bool>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((bool)(object)firstVariable != (bool)(object)secondVariable) { result = "1"; }
                            else { result = "2"; }
                        }
                    }
                    else { result ="Неверный синтаксис"; }
                }

                else if (firstVariable is long)
                {
                    if(words[index] == "==")
                    {
                        if (TryParseGeneric<long>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((long)(object)firstVariable == (long)(object)secondVariable) { result = "1"; }
                            else { result = "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (TryParseGeneric<long>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((long)(object)firstVariable != (long)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else if (words[index] == ">")
                    {
                        if (TryParseGeneric<long>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((long)(object)firstVariable > (long)(object)secondVariable) { result = "1"; }
                            else { result = "2"; }
                        }
                    }
                    else if (words[index] == "<")
                    {
                        if (TryParseGeneric<long>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((long)(object)firstVariable < (long)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else if (words[index] == "<=")
                    {
                        if (TryParseGeneric<long>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((long)(object)firstVariable <= (long)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else if (words[index] == ">=")
                    {
                        if (TryParseGeneric<long>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((long)(object)firstVariable >= (long)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else { result = "Неверный синтаксис"; }
                }

                else if (firstVariable is double)
                {
                    if (words[index] == "==")
                    {
                        if (TryParseGeneric<double>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((double)(object)firstVariable == (double)(object)secondVariable) { result = "1"; }
                            else { result = "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (TryParseGeneric<double>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((double)(object)firstVariable != (double)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else if (words[index] == ">")
                    {
                        if (TryParseGeneric<double>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((double)(object)firstVariable > (double)(object)secondVariable) { result = "1"; }
                            else { result = "2"; }
                        }
                    }
                    else if (words[index] == "<")
                    {
                        if (TryParseGeneric<double>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((double)(object)firstVariable < (double)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else if (words[index] == "<=")
                    {
                        if (TryParseGeneric<double>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((double)(object)firstVariable <= (double)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else if (words[index] == ">=")
                    {
                        if (TryParseGeneric<double>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((double)(object)firstVariable >= (double)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else { result = "Неверный синтаксис"; }
                }

                else if (firstVariable is string)
                {
                    if (words[index] == "==")
                    {
                        if (TryParseGeneric<string>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            if ((string)(object)firstVariable == (string)(object)secondVariable) { result = "1"; }
                            else { result = "2"; }
                        }
                    }
                    else if (words[index] == "!=")
                    {
                        if (TryParseGeneric<string>(words[++index]))
                        {
                            secondVariable = (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(words[index++]);
                            result = ((string)(object)firstVariable != (string)(object)secondVariable) ? "1" : "2";
                        }
                    }
                    else { result = "Неверный синтаксис"; }
                }
            }
            catch
            {
                result = "Данные некорректны";
            }
            if (words[index++] != ")" || words[index++]!= "{") { result = "Неверный синтаксис"; };
            //ниже - обработка else 
            if (words.Contains("else"))
            {
                int elseIndex = Array.IndexOf(words, "else");
                if( elseIndex < index)
                {
                    result = "Неверный синтаксис";
                }
                else
                {
                    if(words[elseIndex-1]!="}" || words[elseIndex + 1] != "{" || words[words.Length - 1] != "}")
                    {
                        result = "Неверный синтаксис";
                    }
                }
            }
            else
            {
                if(words[words.Length - 1] != "}")
                {
                    result = "Неверный синтаксис";
                }
                else
                {
                    if (result == "2")
                    {
                        result = "Условие не выполнилось";
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Проверяет, возможно ли привести данные к указзном типу
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
