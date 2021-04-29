using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Logic.Model
{
   public class AsmModel: IAsmModel
    {

        [DllImport("AsmFunc.dll")]
        public static extern int multiply(int a, int b);
        [DllImport("AsmFunc.dll")]
        public static extern double divide(double a, double b);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">Число а для операции</param>
        /// <param name="b">Число b для операции</param>
        /// <param name="mode">True = multiply, False = divide</param>
        /// <returns>[0] - результат вычислений, [1] - переполнение</returns>
        public string[] DoOperation(string a, string b, bool mode)
        {
            double aNum = 0;
            double bNum = 0;
            string[] result = new string[2];
            result[1] = null;
            if (double.TryParse(a, out aNum) && double.TryParse(b, out bNum))
            {
                if (mode)
                {
                    int coef = 1;
                    if ((aNum > 0 && bNum < 0) || (aNum < 0 && bNum > 0))
                        coef = -1;
                    aNum = Math.Abs(aNum);
                    bNum = Math.Abs(bNum);
                    result[0] = (coef * multiply((int)aNum, (int)bNum)).ToString();
                    if (aNum != 0 && bNum != 0 && result[0] == "0")
                        result[1] = "Ошибка переполнения!";
                }
                else
                    result[0] = divide(aNum, bNum).ToString();
            }
            else
                result[1] = "Неверный формат введенных данных";
            return result;
        }
    }
}
