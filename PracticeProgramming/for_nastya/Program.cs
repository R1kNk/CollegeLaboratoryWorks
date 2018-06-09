using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pavlovski_5
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0, min = 0, j, k = 0;
            Console.WriteLine("Введите размер массива: ");
            int[] mas = new int[Convert.ToInt32(Console.ReadLine())];

            for (i = 0; i < mas.Length; i++)
            {
                Console.WriteLine("Введите элемент массива {0}", i);
                mas[i] = Convert.ToInt32(Console.ReadLine());
                if (mas[i] < 0) k++;
            }

            for (i = 0; i < mas.Length; i++)
                Console.Write("{0}  ",mas[i]);
            Console.WriteLine();


            // Минимальный по модулю элемент 
            for (i = 0; i < mas.Length; i++)
                if (Math.Abs(mas[i]) < Math.Abs(mas[min])) min = i;
            Console.WriteLine("Минимальное число по модулю: {0}", mas[min]);
            
            // конец первого задания 

            // Сумма модулей после первого нуля 
            int abssum = 0;
            for (i = Array.IndexOf(mas, 0); i < mas.Length && i >= 0; ++i)
                if (mas[i] != 0)
                    abssum += Math.Abs(mas[i]);

            Console.WriteLine("Сумма модулей элемента массива " + abssum);
            int[] array=default(int[]);
            bool check=false;
            while (!check)
            {
                try
                {
                    Console.WriteLine("Введите размер нового массива:");
                    array = new int[Convert.ToInt32(Console.ReadLine())];
                    check = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введен некорректный размер массива");
                }
            }
            Random randInt32Numbers = new Random();
            for (i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Нажмите любую клавишу чтобы зарандомить элемент {0}", i);
                Console.ReadKey();
                array[i] = randInt32Numbers.Next(-999999, 999999);
            }
            Console.WriteLine("Исходный массив до сортировки по позициям:");
            for (i = 0; i < array.Length; i++)
                Console.Write("{0}  ", array[i]);
            int counterSortedArray = default(int);
            int[] sorted_arr = new int[array.Length];
            for (i = 0; i < array.Length; i++)
                if(i % 2 == 0) { sorted_arr[counterSortedArray] = array[i]; counterSortedArray++; }
            for (i = 0; i < array.Length; i++)
                if (i % 2 == 1) { sorted_arr[counterSortedArray] = array[i]; counterSortedArray++; }
            Console.WriteLine("\nОтсортированный массив: ");
            for (i = 0; i < array.Length; i++)
            Console.Write("{0}  ", sorted_arr[i]);
            Console.ReadKey();
        }
    }
}



