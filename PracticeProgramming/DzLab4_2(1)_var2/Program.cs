using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class SolvingExample
{
static public double Solve(double a)
    {
        if (a < 0 || a == 0) return 1;
        else if ((0 < a) && (a < 1 || a == 1)) return Math.Pow(a, 2);
        else return Math.Pow(a, 4);
    }

}
    class Program
    {
        static void Main()
        {
        Console.WriteLine("Введите число a:");
        double a = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Функция f(x) приняла значение {0}", SolvingExample.Solve(a));
        }
    }
