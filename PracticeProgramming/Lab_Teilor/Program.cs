using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TailorRow
{
    static double fact(double N)
    {
        double result = 1;
        for (int i = 1; i <= N; i++) result *= i;
        return result;
    }
    static public double SolveTeilor(double x, double e, out double step)
    {
        double arg_now = 1;
        step = 0;
        double result_func = 1;
        while (Math.Abs(arg_now) > e)
        {
            step++;
            arg_now = (Math.Pow(-1, step) * Math.Pow(x, 2 * step)) / TailorRow.fact((2*step)+1);
            result_func += arg_now;
        }
        return result_func;
    }
}
namespace Lab_Teilor
{
    class Program
    {
        static void Main()
        {
            
                double step, counter_steps, e, i, result, Xlow, Xhigh = default(double);
                Console.WriteLine("Введите точность e ");
                e = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите значение Xlow");
                Xlow = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите значение Xhigh");
                Xhigh = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите значение шага");
                step = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("\n\n{0,6}\t{1,25}\t{2,25}\t{3,3}\n", "x", "Изначальная функция (sin(x)/x)", "Результат","За кол-во шагов");
                for (i = Xlow; i < Xhigh; i += step)
                {
                   result = TailorRow.SolveTeilor(i, e, out counter_steps);
                    Console.WriteLine("{0,6:f2}\t{1,25}\t{2,25}\t{3,3}\n", i, (Math.Sin(i)/i), result,counter_steps);
                }
        }
    }
}
