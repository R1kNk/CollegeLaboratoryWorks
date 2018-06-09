using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class SolveShedule
{
    static public double Solve(double x)
    {
        double y = 0;
        if (x < -1 || x == -1) y = 0;
        else if (x > 1 || x == 1) y = 0;
        else if (x > -1 && x < 1)
        {
           y = Math.Sqrt(4 - Math.Pow(x, 2));
            Console.WriteLine("test");
        }
        return y;
    }
}
    class Program
    {
        static void Main()
        {
        Console.WriteLine("Введите значение x");
        double k = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Результат  {0}", SolveShedule.Solve(k));
        }
    }
