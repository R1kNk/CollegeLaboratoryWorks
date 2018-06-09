using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class SolvingExample
{
static public double Solve(double f, double g, double k)
    {
        return (Math.Min((Math.Min(f, g)), k)+8)/(Math.Max(g,k));
    }
}
    class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine("Введите f, g, k");
        double f = Convert.ToDouble(Console.ReadLine());
        double g = Convert.ToDouble(Console.ReadLine());
        double k = Convert.ToDouble(Console.ReadLine());
        double result = SolvingExample.Solve(f, g, k);
        Console.WriteLine("Результат: {0} ", result);
    }
    }

