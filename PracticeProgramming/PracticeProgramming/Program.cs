using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PracticeProgramming
{
   static class Solution_lab1 {
        static public double SolvingOf1(ref double a) {
            double space1, space2;
            space1 = Math.Sin(4 * a) / (1 + Math.Cos(4 * a));
            space2 = Math.Cos(2 * a) / (1 + Math.Cos(2 * a));
            a = space1 * space2;
            return a;
        }
        static public double SolvingOf2(ref double a) {
            a = Math.Cos((((3*Math.PI) / 2)) - a) / Math.Sin((((3 * Math.PI) / 2)) - a);
            return a;
        }
    }
    class Lesson1_main
    {
        static void Main()
        {
            double a, b;
            Console.WriteLine("Введите число a");
            a = b = Convert.ToDouble(Console.ReadLine());
            Solution_lab1.SolvingOf1(ref a);
            Solution_lab1.SolvingOf2(ref b);
            Console.WriteLine("a1 = {0}\ta2 = {1}\t Difference = {2}", Math.Round(a,5), Math.Round(b,5), Math.Round(a, 5)-Math.Round(b, 5));
        }
    }
}
