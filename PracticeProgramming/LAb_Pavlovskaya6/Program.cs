using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TwoSizedArrays
{
    struct Rows
    {
        public double character;
        public int position;
    }
    static int FindMaxInStruct(Rows[] row_struct)
    {
        double max = default(double);
        int pos_result = default(int);
        max = row_struct[0].character; ;
        int buf = 0;
        if(max==1)
        while (max == 1)
        {
                max = row_struct[buf].character;
                buf++;
        }
        for (int i = 0; i < row_struct.Length; i++)
        if (row_struct[i].character > max && row_struct[i].character != 1) max = row_struct[i].character;
        for (int i = 0; i < row_struct.Length; i++)
        {
            if (row_struct[i].character == max)
            {
                pos_result = row_struct[i].position;
                row_struct[i].character = 1;
                break;
            }
        }
        return pos_result;
    }
    static public double[,] SwapLinesMatrix(double[,] array)
    {
        Rows[] row_info = new Rows[array.GetLength(0)];
        int row_with_zero = -1;
        double buf_row_characters = 0;
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                if (array[i, j] == 0)
                {
                    row_with_zero = j;
                    break;
                }
        if (row_with_zero == -1) Console.WriteLine("В данном массиве нет ни одного нуля");
        else Console.WriteLine("В данном массиве первый столбец с нулевым элементом: {0}", row_with_zero);
        double[] characters_of_rows = new double[array.GetLength(0)];
        for (int i = 0; i < characters_of_rows.Length; i++) characters_of_rows[i] = -1; 
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i,j]<0 && array[i, j] % 2 == 0)
                    buf_row_characters += array[i, j];
            }
            characters_of_rows[i] = buf_row_characters;
            buf_row_characters = 0;
        }
        for (int i = 0; i < characters_of_rows.Length; i++)
        {
            row_info[i].character = characters_of_rows[i];
            row_info[i].position = i;
            Console.WriteLine("Характеристика {0}-ой строки:{1}",i, characters_of_rows[i]);
        }
        

        double[] buf_row_array = new double[array.GetLength(0)];
        double[,] result_array = new double[array.GetLength(0), array.GetLength(1)];

        for (int j = 0; j < result_array.GetLength(0); j++)
        {
            int current_row = FindMaxInStruct(row_info);
            for (int r = 0; r < result_array.GetLength(1); r++)
            {
                result_array[j, r] = array[current_row, r];
            }
        }
        return result_array;
    }
}
namespace LAb_Pavlovskaya6
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = true;
            while (exit)
            {
                Console.Clear();
                Random RandomObject = new Random();
                Console.WriteLine("Введите количество строк и столбцов массива:");
                double[,] array = new double[Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine())];
                Console.WriteLine("Исходный массив:");
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        array[i, j] = RandomObject.Next(-1000, 1000);
                        Console.Write("{0,4} ", array[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                array = TwoSizedArrays.SwapLinesMatrix(array);
                Console.WriteLine();
                Console.WriteLine("Результирующий массив:");
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        Console.Write("{0,4} ", array[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Ещё?");
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
