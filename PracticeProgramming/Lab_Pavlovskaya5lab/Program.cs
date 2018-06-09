using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TaskMassive
{
        static public float[] ReWorkMassive(float[] array, float C)
    {
        int counter = 0;
        float BiggestByAbs = 0;
        int Pos = 0;
        float MultipliuedRes = 1;
        BiggestByAbs = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (C<array[i]) counter++;
            if (Math.Abs(array[i]) > Math.Abs(BiggestByAbs))
            {
                BiggestByAbs = array[i];
                Pos = i;
            }

        }
        if(Pos!=array.Length-1) for (int i = Pos + 1; i < array.Length; i++) { MultipliuedRes *= array[i]; }
        float[] result_array = new float[array.Length];
        int counter_buf = 0;
        for (int i=0; i<result_array.Length; i++)
        {
            if (array[i] < 0) { result_array[counter_buf] = array[i]; counter_buf++; }
        }
        for (int i = 0; i < result_array.Length; i++)
        {
            if (array[i] >= 0) { result_array[counter_buf] = array[i]; counter_buf++; }
        }
        Console.WriteLine("Элементов в массиве, больших С: {0}\n Максимальный элемент по модулю:{1} на позиции {2}\nПроизведение элементов массива, расположенных после максимального по модулю элемента: {3}", counter, BiggestByAbs, Pos+1, MultipliuedRes);
        return result_array;
    } 

}
namespace Lab_Pavlovskaya5lab
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = true;
            Random RandomObject = new Random();
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Укажите размер массива:");
                float[] array = new float[Convert.ToInt32(Console.ReadLine())];
                Console.WriteLine("Исходный массив");
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = RandomObject.Next(-999, 999) + (float)RandomObject.NextDouble();
                    Console.Write("{0,10}  ", array[i]);

                }
                Console.WriteLine("\nВведите число C");
                array = TaskMassive.ReWorkMassive(array, float.Parse(Console.ReadLine()));
                Console.WriteLine();
                Console.WriteLine("Массив после преобразования:");
                for (int i = 0; i < array.Length; i++) Console.Write("{0,10}  ", array[i]);
                Console.WriteLine("\nЕщё?");
                try
                {
                    exit = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    exit = false;
                }
            }

        }
    }
}
