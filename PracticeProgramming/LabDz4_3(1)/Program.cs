using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class SumNumbers
{
    static public int CountNumbers()
    {
        int numbers = 10;
        int counter = 0;
        do
        {
            if (numbers % 3 == 0) counter += numbers;
            numbers++;
        } while (numbers < 100);
        return counter;
    }
}
    class Program
    {
        static void Main()
        {
        Console.WriteLine("Сумма натуральных двузначных чисел которые делятся на 3 равна " + SumNumbers.CountNumbers());
        }
    }
