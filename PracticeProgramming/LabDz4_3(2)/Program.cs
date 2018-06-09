using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class CountWithWhile
{
    static public void Count(int a, int b, int h)
    {
        while (a <= b)
        {
            Console.WriteLine("При x = {0}\t y = {1}", a, (Math.Log10(a) - (1 / (a + 5))));
            a += h;
        }
    }
}
    class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine("Введите промежуток:");
        int a, b, h;
        a = Convert.ToInt32(Console.ReadLine());
        b = Convert.ToInt32(Console.ReadLine());
        h = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Результат:\n ");
        CountWithWhile.Count(a, b, h);
    }
    }
