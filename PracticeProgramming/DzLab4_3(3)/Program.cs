using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FactorialExpression
{
    static public double CountExp(int n, double x)
    {
        double result = 1;
        int counter=1;
        double fact=1;
        while (counter < n+1)
        {
            result += (Math.Pow(x, counter) / (fact = fact * counter));
            counter++;
        }
        return result;
    }
}
  class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine("Введите число n, до которого идет счет:");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите число x");
        double x = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Результат:\t {0}", FactorialExpression.CountExp(n, x));


    }
    }
