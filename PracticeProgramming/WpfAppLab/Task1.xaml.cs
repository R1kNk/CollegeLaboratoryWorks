using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppLab
{
    /// <summary>
    /// Логика взаимодействия для Task1.xaml
    /// </summary>
    public partial class Task1 : Window
    {
        static double number1;
        static double number2;
        static double number3;
        //
        static bool summ;
        static bool multiple;
        //
        Task1_input inputObject;
        public Task1()
        {
            InitializeComponent();
            number1 = 0;
            number2 = 0;
            number3 = 0;
            inputObject = new Task1_input();
        }
        
        static public void updateData(double numb1, double numb2, double numb3, bool sum, bool multipl)
        {
            number1 = numb1;
            number2 = numb2;
            number3 = numb3;
            summ = sum;
            multiple = multipl;
        }
        bool check3Numbers()
        {
            string buf = default(string);
            buf = Convert.ToString(number1);
            if(number1!=0)
           { 
            foreach(char c in buf)
            {
                if ((c != ',' && c != '.') && Char.IsDigit(c)) continue; else return false;
            }
           }
            else return false;
            //
            buf = Convert.ToString(number2);
            if (number2 != 0)
             { 
                foreach (char c in buf)
                {
                    if ((c != ',' && c != '.') && Char.IsDigit(c)) continue; else return false;
                }
             }
            else return false;
            //
            buf = Convert.ToString(number3);
            if (number3 != 0)
            {
                foreach (char c in buf)
                {
                    if ((c != ',' && c != '.') && Char.IsDigit(c)) continue; else return false;
                }
            }
            else return false;
            return true;
        }
        bool check2Numbers()
        {
            string buf = default(string);
            buf = Convert.ToString(number1);
            if (number1 != 0)
            {
                foreach (char c in buf)
                {
                    if ((c != ',' && c != '.') && Char.IsDigit(c)) continue; else return false;
                }
            }
            else return false;
            //
            buf = Convert.ToString(number2);
            if (number2 != 0)
            {
                foreach (char c in buf)
                {
                    if ((c != ',' && c != '.') && Char.IsDigit(c)) continue; else return false;
                }
            }
            else return false;
            //
            return true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            inputObject = new Task1_input();
                inputObject.Show();
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (summ == true && multiple == true)
            {
                if (check3Numbers())
                    for (int i = 0; i < (number1 * number2 + 1); i++)
                    {
                        if (i % number2 == 0 && i % number1 == 0)
                        {
                            int nok = i;
                            if (i != 0)
                            {
                                MessageBox.Show("Сумма чисел " + number1 + ", " + number2 + " и " + number3 + " = " + (number1 + number2 + number3) + " НОК " + number1 + " и " + number2 + " = " + i, "Результат!");
                                break;
                            }
                        }
                    }
                else MessageBox.Show("Числа введены некоррекно или не введены вовсе. Попробуйте ещё раз в меню Input", "Ошибка!");

            }
            else if (summ)
            {
                if (check3Numbers())
                    MessageBox.Show("Сумма чисел " + number1 + ", " + number2 + " и " + number3 + " = " + (number1 + number2 + number3), "Результат!");
                else MessageBox.Show("Числа введены некоррекно или не введены вовсе. Ппробуйте ещё раз в меню Input", "Ошибка!");
            }
            else if (multiple)
            {
                if(check2Numbers())
                for (int i = 0; i < (number1 * number2 + 1); i++)
                {
                    if (i % number2 == 0 && i % number1 == 0)
                    {
                        int nok = i;
                        if (i != 0)
                        {
                            MessageBox.Show("НОК "+ number1+" и "+number2+ " = " + i,"Результат!");
                            break;
                        }
                    }
                }
                else MessageBox.Show("Числа введены некоррекно или не введены вовсе. Ппробуйте ещё раз в меню Input", "Ошибка!");

            }
            else MessageBox.Show("Выберите действие в меню Input", "Ошибка!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Автор - Послед Никита Т-592");
        }
    }
}
