using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class CounterExpression {
static public double Expression(double m)
    {
        double result=0;
        int i;
        if (m <= 0)
        {
            result = 1;
            i = 4;
            while (i < 17)
            {
                result *= ((1 / Math.Sqrt(i)) + 1);
                i += 4;
            }


        }
        else
        {
            i = 2;
            while (i < 21)
            {
                result += (Math.Sqrt(i) + 1);
                i += 2;
            }
        }
        return result;
    }
}

    class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine("Введите m");
        double m = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Результат: {0}", CounterExpression.Expression(m));
        }
    }
