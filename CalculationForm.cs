using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP2
{
    public partial class CalculationForm : Form
    {
        public CalculationForm()
        {
            InitializeComponent();
        }

        private bool CheckKey(char number)  //Возможен ввод 1 или 0
        {
            if ((number <= 47 || number >= 50) && number != 8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Increase() //Заполнение до 8 разрядов 
        {
            while (RgABox.Text.Length < 16)
            {
                RgABox.Text = '0' + RgABox.Text;
            }
            while (RgBBox.Text.Length < 16)
            {
                RgBBox.Text = '0' + RgBBox.Text;
            }
        }

        private void RgABox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            e.Handled = CheckKey(number);
        }

        private void RgBBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            e.Handled = CheckKey(number);
        }

        private void OperationPlus_Click(object sender, EventArgs e)
        {
            Increase();
            InfoBox.Clear();
            InfoBox.Text = "RgA: ";
            RgCBox.Clear();
            for (int i = 0; i < RgABox.Text.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + RgABox.Text[i];
                if (i == 0)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n";
            InfoBox.Text = InfoBox.Text + "RgB: ";
            for (int i = 0; i < RgBBox.Text.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + RgBBox.Text[i];
                if (i == 0)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n";
            InfoBox.Text = InfoBox.Text + "Operation +" + "\r\n" + "\r\n";
            Calculations.Copy(RgABox.Text);
            Calculations.Copy(RgBBox.Text);
            //Для модифицированного кода
            if (Calculations.rg_a[1] == 1)
            {
                Calculations.rg_a[0] = 1;
            }
            else
            {
                Calculations.rg_a[0] = 0;
            }
            if (Calculations.rg_b[1] == 1)
            {
                Calculations.rg_b[0] = 1;
            }
            else
            {
                Calculations.rg_b[0] = 0;
            }
            InfoBox.Text = InfoBox.Text + "Обратный модифицированный код:" + "\r\n";
            for (int i = 0; i < Calculations.rg_a.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + Convert.ToString(Calculations.rg_a[i]);
                if (i == 1)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n";
            for (int i = 0; i < Calculations.rg_b.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + Convert.ToString(Calculations.rg_b[i]);
                if (i == 1)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n" + "\r\n";
            //Концовка модифицированного кода
            int[] check = new int[17] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if ((Calculations.rg_a[0] == 1)&&(!Equals(Calculations.rg_a, check)))
            {
                Calculations.Inverse(Calculations.rg_a);
            }
            if ((Calculations.rg_b[0] == 1)&&(!Equals(Calculations.rg_b, check)))
            {
                Calculations.Inverse(Calculations.rg_b);
            }
            Plus();
            if (!Calculations.not_right)
            {
                for (int i = 1; i < 17; i++)
                {
                    RgCBox.Text = RgCBox.Text + Convert.ToString(Calculations.rg_c[i]);
                    if (i == 1)
                    {
                        RgCBox.Text = RgCBox.Text + ',';
                    }
                }
            }
            else
            {
                InfoBox.Text = InfoBox.Text + "Возникло переполнение!";
            }
        }

        public void Plus()
        {
            Calculations.rg_c = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Calculations.not_right = false;
            int ct = 16;
            Calculations.tg_p = 0;
            Calculations.step = 1;
            do
            {
                int summ = 3;
                if (ct != 0)
                {
                    InfoBox.Text = InfoBox.Text + "Вычисление от " + ct + " до " + (ct - 3) + "\r\n";
                }
                do
                {
                    InfoBox.Text = InfoBox.Text + "Step: " + Calculations.step + "\r\n";
                    InfoBox.Text = InfoBox.Text + "TgP: " + Calculations.tg_p + "\r\n";
                    InfoBox.Text = InfoBox.Text + "RgC: ";
                    for (int i = 0; i < 17; i++)
                    {
                        InfoBox.Text = InfoBox.Text + Calculations.rg_c[i];
                        if (i == 1)
                        {
                            InfoBox.Text = InfoBox.Text + ',';
                        }
                    }
                    InfoBox.Text = InfoBox.Text + "\r\n" + "\r\n";
                    Calculations.rg_c[ct] = Calculations.rg_a[ct] + Calculations.rg_b[ct] + Calculations.tg_p;
                    if (Calculations.rg_c[ct] > 1)
                    {
                        Calculations.tg_p = 1;
                        Calculations.rg_c[ct] = Calculations.rg_c[ct] - 2;
                    }
                    else
                    {
                        Calculations.tg_p = 0;
                    }
                    summ--;
                    ct--;
                    Calculations.step++;
                } while ((summ >= 0) && (ct >= 0));
            } while (ct >= 0);
            InfoBox.Text = InfoBox.Text + "Step: " + Calculations.step + "\r\n";
            InfoBox.Text = InfoBox.Text + "TgP: " + Calculations.tg_p + "\r\n";
            InfoBox.Text = InfoBox.Text + "RgC: ";
            for (int i = 0; i < 17; i++)
            {
                InfoBox.Text = InfoBox.Text + Calculations.rg_c[i];
                if (i == 1)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            if (Calculations.rg_c[0] != Calculations.rg_c[1])
            {
                Calculations.not_right = true;
            }
            else
            {
                if (Calculations.tg_p == 1)
                {
                    int col = 16;
                    bool increase = true;
                    while (increase)
                    {
                        Calculations.rg_c[col] = Calculations.rg_c[col] + Calculations.tg_p;
                        if (Calculations.rg_c[col] > 1)
                        {
                            Calculations.rg_c[col] = Calculations.rg_c[col] - 2;
                            col--;
                        }
                        else
                        {
                            increase = false;
                        }
                    }
                }
                if ((Calculations.rg_c[0] == 1) && (Calculations.rg_c[1] == 1))
                {
                    Calculations.Inverse(Calculations.rg_c);
                }
            }
        }


        private void OperatonMinus_Click(object sender, EventArgs e)
        {
            Increase();
            InfoBox.Clear();
            InfoBox.Text = "RgA: ";
            RgCBox.Clear();
            for (int i = 0; i < RgABox.Text.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + RgABox.Text[i]; 
                if (i == 0)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n";
            InfoBox.Text = InfoBox.Text + "RgB: ";
            for (int i = 0; i < RgBBox.Text.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + RgBBox.Text[i];
                if (i == 0)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n";
            InfoBox.Text = InfoBox.Text + "Operation -" + "\r\n" + "\r\n";
            Calculations.Copy(RgABox.Text);
            Calculations.Copy(RgBBox.Text);
            //Для модифицированного кода
            if (Calculations.rg_a[1] == 1)
            {
                Calculations.rg_a[0] = 1;
            }
            else
            {
                Calculations.rg_a[0] = 0;
            }
            if (Calculations.rg_b[1] == 1)
            {
                Calculations.rg_b[0] = 1;
            }
            else
            {
                Calculations.rg_b[0] = 0;
            }
            InfoBox.Text = InfoBox.Text + "Обратный модифицированный код:" + "\r\n";
            for (int i = 0; i < Calculations.rg_a.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + Convert.ToString(Calculations.rg_a[i]);
                if (i == 1)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n";
            for (int i = 0; i < Calculations.rg_b.Length; i++)
            {
                InfoBox.Text = InfoBox.Text + Convert.ToString(Calculations.rg_b[i]);
                if (i == 1)
                {
                    InfoBox.Text = InfoBox.Text + ',';
                }
            }
            InfoBox.Text = InfoBox.Text + "\r\n" + "\r\n";
            //Концовка модифицированного кода
            int[] check = new int[17] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if ((Calculations.rg_a[0] == 1) && (!Equals(Calculations.rg_a, check)))
            {
                Calculations.Inverse(Calculations.rg_a);
            }
            if (Calculations.rg_b[0] == 0)
            {
                Calculations.rg_b[0] = 1;
                Calculations.rg_b[1] = 1;
                if (!Equals(Calculations.rg_b, check))
                {
                    Calculations.Inverse(Calculations.rg_b);
                }
            }
            else
            {
                Calculations.rg_b[0] = 0;
                Calculations.rg_b[1] = 0;
            }
            Plus();
            if (!Calculations.not_right)
            {
                for (int i = 1; i < 17; i++)
                {
                    RgCBox.Text = RgCBox.Text + Convert.ToString(Calculations.rg_c[i]);
                    if (i == 1)
                    {
                        RgCBox.Text = RgCBox.Text + ',';
                    }
                }
            }
            else
            {
                InfoBox.Text = InfoBox.Text + "\r\n" + "Возникло переполнение!";
            }
        }
    }
}
