using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SolveLab4
{
    static public void Graphic(double Xn, double Xk)
    {
        double h = (Xk - Xn) / 10;
        Console.WriteLine("1-for\n2- while\n3-do while");
        int choise = Convert.ToInt32(Console.ReadLine());
        switch (choise)
        {
            case 1:
                {
                    for (double i = Xn; i < Xk; i += h)
                    {
                        Console.WriteLine("При x = {0}  y = {1}", i, i * Math.Atan(i) - Math.Log10(Math.Sqrt(1 + Math.Pow(i, 2))));
                    }
                    break;
                }
            case 2:
                {
                    double i = Xn;
                    while (i < Xk)
                    {
                        Console.WriteLine("При x = {0}  y = {1}", i, i * Math.Atan(i) - Math.Log10(Math.Sqrt(1 + Math.Pow(i, 2))));
                         i += h;
                    }
                    break;
                }
            case 3:
                {
                    double i = Xn;
                    do
                    {
                        Console.WriteLine("При x = {0}  y = {1}", i, i * Math.Atan(i) - Math.Log10(Math.Sqrt(1 + Math.Pow(i, 2))));
                        i += h;
                    }
                    while (i < Xk);
                    break;
                }
        }
       
        
    }
    static public void SolveSecond(int n)
    {
        string buf_n = Convert.ToString(n);
        StringBuilder strReverse = new StringBuilder();
        strReverse.Append(n);
        for (int i = 0, j = buf_n.Length - 1; i < buf_n.Length/2; i++, j--)
        {
            char buffer=default(char);
            buffer = strReverse[i];
            strReverse[i]=strReverse[j];
            strReverse[j] = buffer;
        }
        if (strReverse[0] == '0') strReverse.Remove(0, 1);
        Console.WriteLine(strReverse);
    }

}
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = true;
            while (exit)
            {
            try
            {
                Console.Clear();
                Console.WriteLine("Выберите задание: 1 или 2");
                int choise = Convert.ToInt32(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        {
                            SolveLab4.Graphic(0.1, 0.8);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Введите число n:");
                            int n = Convert.ToInt32(Console.ReadLine());
                            SolveLab4.SolveSecond(n);
                            break;
                        }
                    default: Console.WriteLine("Нет такого графика"); break;

                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("ещё?");
                exit = Convert.ToBoolean(Console.ReadLine());
            }
            }
        }
    }
