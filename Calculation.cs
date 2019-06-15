using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP2
{
    static class Calculation
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculationForm());
        }
    }
    
    public class Calculations
    {
        public static int[] rg_a = new int[17];
        public static int[] rg_b = new int[17];
        public static bool count = false;
        public static int[] rg_c = new int[17];
        public static bool not_right = false;
        public static int step;
        public static int tg_p;

        public static void Inverse(int[] rg) //Перевод числа в обратный код
        {
            for (int i = 2; i < rg.Length; i++)
            {
                rg[i] = (rg[i] + 1) % 2;
            }
        }

        public static void Copy(string copy_str) //Запись в переменные
        {
            if (!count)
            {
                for (int i = 0; i < copy_str.Length; i++)
                {
                    rg_a[i+1] = Convert.ToInt16(copy_str[i]) - 48;
                }
                count = true;
            }
            else
            {
                for (int i = 0; i < copy_str.Length; i++)
                {
                    rg_b[i+1] = Convert.ToInt16(copy_str[i]) - 48;
                }
                count = false;
            }
        }
    }
}
