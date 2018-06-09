using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Program
    {
        static void Main(string[] args)
        {
        double[] array = new double[10] { 1, 8, -10, 5, 9, -11, 2, 5, 10, -2 };
        foreach (var i in array) Console.Write("{0} ", i);
        Console.WriteLine();
        Console.WriteLine("Введите C");
        double c = Convert.ToDouble(Console.ReadLine());
        
        int count = (from i in array where i > c select i).Count();
        Console.WriteLine("Чисел больше C: {0}", count);
        double min = array.Min();
        double max = array.Max();
        double maxModule = default(double);
        if (min < 0)
        {
            if (min * -1 > max) maxModule = min;
            else maxModule = max;
        }
        int maxIndex = Array.IndexOf(array, maxModule);
        Console.WriteLine("Максимальный по модулю элемент: {0}, его индекс {1}", maxModule, maxIndex);

        double multiply = array.Skip(maxIndex + 1).Aggregate((x, y) => x * y);
        Console.WriteLine("Произведение элементов после {0} = {1}", maxModule, multiply);
        var sortedArr = from i in array orderby i select i;
        foreach (var i in sortedArr) Console.Write("{0} ", i);
        }
    }
