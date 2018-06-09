using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class SolutionOfExpressions
{
    static public double SolveAXB(double a, double b)
    {
        if (b == 0||a==0)
        return 0;
        else
        return b / a;

    }

}
    class Program
    {
        static void Main()
        {
        Console.WriteLine("Дадзены сапраўдныя лiкi a, b. Знайсці рашэнне няроўнасцi ax<=b.\n");
        Console.WriteLine("Введите переменную a");
        double a = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите переменную b");
        double b = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Выражение является верным при x <= {0}", SolutionOfExpressions.SolveAXB(a, b));

    }
    }
