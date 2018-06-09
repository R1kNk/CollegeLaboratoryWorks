using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Program
    {
        static void Main(string[] args)
        {
        int a, b;
        a = Convert.ToInt32(Console.ReadLine());
        b = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("До операции\t a = {0}, b = {1}", a, b);
        a = a ^ b;
        b = a ^ b;
        a = a ^ b;
        Console.WriteLine("После операции\t a = {0}, b = {1}", a, b);
    }
    }
