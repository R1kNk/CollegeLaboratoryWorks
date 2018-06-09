using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct point
{
    public double x_coord;
    public double y_coord;
}
class TaskMethods
{
   static public void MaxPointsLenght(point[] point_arr)
    {
        Console.Clear();
        Console.WriteLine("Информация о точках:");
        for (int i = 0; i < point_arr.Length; i++)
        {
            Console.WriteLine("Точка {0}:\n x = {1} y = {2}", i + 1, point_arr[i].x_coord, point_arr[i].y_coord);
        }
        double max_length = -1;
        double point1 = default(int);
        double point2 = default(int);
        double current_length = default(int);
        for (int i = 0; i < point_arr.Length; i++)
            for (int j = 0; j < point_arr.Length; j++)
                if (i != j)
                {
                    current_length = Math.Abs(Math.Sqrt(Math.Pow(point_arr[j].x_coord - point_arr[i].x_coord, 2) + Math.Pow(point_arr[j].y_coord - point_arr[i].y_coord, 2)));
                    if (current_length > max_length)
                    {
                        point1 = i; point2 = j; max_length = current_length;
                    }
                }
        Console.WriteLine("Самое большое расстояние {0} между точками {1} и {2}", max_length, point1 + 1, point2 + 1);
    }
/// <summary>
/// Поиск
/// </summary>
/// <param name="words_array">орплорпорплорпло</param>
/// <param name="biggest_count"></param>
/// <returns></returns>
    static public string MaxVowelCount(string[] words_array, out int biggest_count)
    {
        Console.Clear();
        string rus_vowel="уеыаоэяиюУЕЫАОЭЯИЮ";
        int index_word = default(int);
        biggest_count = -1;
        int current_count = default(int);
        for (int i = 0; i < words_array.Length; i++)
            Console.WriteLine("Слово {0}: {1}", i + 1, words_array[i]);
        for (int i = 0; i < words_array.Length; i++)
        {
            current_count = default(int);
            for (int j = 0; j < words_array[i].Length; j++)
            {
                for (int k = 0; k < rus_vowel.Length; k++)
                {
                    if(words_array[i][j]==rus_vowel[k]) { current_count++; break; }
                }
            }
            if (biggest_count < current_count) { index_word = i; biggest_count = current_count; }
        }
        return words_array[index_word];
    } 
    static public double SolveFunction(int m, int n, double k, int a)
    {
        if (m < n)
        {
            Console.WriteLine("При m < n решить данную функцю невозможно. (факториал от отрицатеьного числа не находится)");
            return 0;
        }
        else
        return fact(m - n) / (k + fact(a));
    }
    static public int fact(int number)
    {
            if (number == 1 || number == 0) return 1;
            return number * fact(number - 1);

    }
    static bool IsTriangle(double a, double b, double c)
    {
        if (a < b + c && b < a + c && c < a + b)
            return true;
        else return false;
    }

    static public void TriangleSquare(double[] arr_length)
    {
        double square = default(double);
        if (IsTriangle(arr_length[0], arr_length[1], arr_length[2]))
        {
            double p_p = (arr_length[0] + arr_length[1] + arr_length[2]) / 2;
            square = Math.Sqrt(p_p * (p_p - arr_length[0]) * (p_p - arr_length[1]) * (p_p - arr_length[2]));
            Console.WriteLine("Треугольник, который образовали отрезки a b и c имеет площадь {0}", square);
            square = default(double);
        }
        else Console.WriteLine("Отрезки а b и c  не образуют треугольник");
        if (IsTriangle(arr_length[0], arr_length[1], arr_length[3]))
        {
            double p_p = (arr_length[0] + arr_length[1] + arr_length[3]) / 2;
            square = Math.Sqrt(p_p * (p_p - arr_length[0]) * (p_p - arr_length[1]) * (p_p - arr_length[3]));
            Console.WriteLine("Треугольник, который образовали отрезки a b и d имеет площадь {0}", square);
            square = default(double);
        }
        else Console.WriteLine("Отрезки а b и d  не образуют треугольник");
        if (IsTriangle(arr_length[1], arr_length[2], arr_length[3]))
        {
            double p_p = (arr_length[1] + arr_length[2] + arr_length[3]) / 2;
            square = Math.Sqrt(p_p * (p_p - arr_length[1]) * (p_p - arr_length[2]) * (p_p - arr_length[3]));
            Console.WriteLine("Треугольник, который образовали отрезки b c и d имеет площадь {0}", square);
            square = default(double);
        }
        else Console.WriteLine("Отрезки b c и d  не образуют треугольник");
        if (IsTriangle(arr_length[0], arr_length[2], arr_length[3]))
        {
            double p_p = (arr_length[0] + arr_length[2] + arr_length[3]) / 2;
            square = Math.Sqrt(p_p * (p_p - arr_length[0]) * (p_p - arr_length[2]) * (p_p - arr_length[3]));
            Console.WriteLine("Треугольник, который образовали отрезки a c и d имеет площадь {0}", square);
            square = default(double);
        }
        else Console.WriteLine("Отрезки а c и d  не образуют треугольник");
    }
        /*a b c
         a  b d
         b c d
         d a c*/

    }
namespace Lab8Pupcev2variant
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = true;
            ConsoleKeyInfo key;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите задание");
                key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine("Введите нужное количество точек:");
                            point[] point_arr = new point[Convert.ToInt32(Console.ReadLine())];
                            for (int i = 0; i < point_arr.Length; i++)
                            {
                                Console.WriteLine("Точка {0}", i + 1);
                                Console.Write("x="); point_arr[i].x_coord = Convert.ToDouble(Console.ReadLine());
                                Console.Write("y="); point_arr[i].y_coord = Convert.ToDouble(Console.ReadLine());
                            }
                            TaskMethods.MaxPointsLenght(point_arr);
                            Console.ReadKey();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("Сколько слов вы хотите ввести?");
                            string[] arr_words = new string[Convert.ToInt32(Console.ReadLine())];
                            for (int i = 0; i < arr_words.Length; i++)
                            {
                                Console.Write("Введите слово {0}:",i+1);
                                arr_words[i] = Console.ReadLine();
                            }
                            int Count;
                            Console.WriteLine("Слово с наибольшим количеством гласных {0}, их количество {1}", TaskMethods.MaxVowelCount(arr_words, out Count), Count);
                            Console.ReadKey();
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine("Cколько чисел вы хотите ввести?");
                            Int64[] arr_numbers = new Int64[Convert.ToInt32(Console.ReadLine())];
                            for (int i = 0; i < arr_numbers.Length; i++)
                            {
                                Console.Write("Введите число {0}:", i + 1);
                                arr_numbers[i] = Convert.ToInt64(Console.ReadLine());
                            }
                            for (int i = 0; i < arr_numbers.Length; i++)
                            {
                                Console.WriteLine("Число {0} на позиции {1} имеет сумму чисел {2}", arr_numbers[i], i + 1, Task2Var.SumNumbers(arr_numbers[i]));
                            }
                            Console.ReadKey();
                            break;
                        }
                    case '4':
                        {
                            Console.WriteLine("Введите m, n, k, a");
                            Console.WriteLine("Результат функции  {0}", TaskMethods.SolveFunction(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()), Convert.ToInt32(Console.ReadLine())));
                            Console.ReadKey();
                            break;
                        }
                    case '5':
                        {
                            double[] points = new double[4];
                            string letters = "abcd";
                            for (int i = 0; i < points.Length; i++)
                            {
                                Console.WriteLine("Введите длину отрезка {0}", letters[i]);
                                points[i] = Convert.ToDouble(Console.ReadLine());
                            }
                            TaskMethods.TriangleSquare(points);
                            Console.ReadKey();
                            break;
                        }
                    case '6':
                        {
                            Console.WriteLine("Введите число, факториал которого вы хотите найти:");
                            Console.WriteLine("Результат - {0}", TaskMethods.fact(Convert.ToInt32(Console.ReadLine())));
                            Console.ReadKey();
                            break;
                        }
                    default:

                        break;
                }
            }
        }
    }

}
