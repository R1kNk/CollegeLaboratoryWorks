using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class WorkingWithMassive
{
    static public double FindSum(double[] array)
    {
        double min = 999999;
        int buf_index = 0;
        foreach(double numbr in array)
        {
            if (numbr < min) min = numbr;
        }
        for (int i = 0; i < array.Length; i++) if (array[i] == min) buf_index = i;
        Console.WriteLine("Минимальное число {0}, его позиция в массиве {1}", min, buf_index);

        double result = 0;
        for (int i = 0; i < buf_index; i++) result += array[i];
        return result;
    } 
}
namespace Lab5__2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            double[] array = new double[size];
            for (int i=0; i<array.Length; i++)
            {
                Console.WriteLine("Введите элемент маасива № {0}", i);
                array[i] = Convert.ToDouble(Console.ReadLine());
            }
            Console.WriteLine("{0}",WorkingWithMassive.FindSum(array));

        }
    }
}
