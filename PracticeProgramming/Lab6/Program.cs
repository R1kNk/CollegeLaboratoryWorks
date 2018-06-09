using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class StringWork
{
    struct HelpString
    {
        public int start_index;
        public int lenght;
    }
    static public void OutSmallestWord(string str)
    {
        HelpString detectSmallest = new HelpString();
        int counter = 0;
        int min = 9999;
        for (int i = 0; i < str.Length; i++)
        {
            if ((str[i] != ' ' && str[i] != ',' && str[i] != '.' && str[i] != '!' && str[i] != ',' && str[i] != '-') && str[i] != '#')
            {
                counter++;
            }
            else if (counter != 0)
            {
                if (counter < min)
                {
                    min = counter;
                    detectSmallest.start_index = i - counter;
                }

                counter = 0;
            }

        }
        detectSmallest.lenght = min;
        for (int i = detectSmallest.start_index; i < detectSmallest.lenght; i++) Console.WriteLine(str[i]);
        char[] result = new char[min];
        Console.WriteLine();
        for (int i = detectSmallest.start_index; i < detectSmallest.lenght + detectSmallest.start_index; i++) Console.Write(str[i]);
        Console.WriteLine();
    }
        static public void OutSmallestWord(StringBuilder str)
        {
            HelpString detectSmallest = new HelpString();
            int counter = 0;
            int min = 9999;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' && str[i] != ',' && str[i] != '.' && str[i] != '!' && str[i] != ',' && str[i] != '-')
                {
                    counter++;
                }
                else if (counter != 0)
                {
                    if (counter < min)
                    {
                        min = counter;
                        detectSmallest.start_index = i - counter;
                    }

                    counter = 0;
                }

            }
            detectSmallest.lenght = min;
            Console.WriteLine();
            for (int i = detectSmallest.start_index; i < detectSmallest.lenght + detectSmallest.start_index; i++) Console.Write(str[i]);
            Console.WriteLine();

        }
        static public void OutSmallestWord(char[] str)
        {
            HelpString detectSmallest = new HelpString();
            int counter = 0;
            int min = 9999;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' && str[i] != ',' && str[i] != '.' && str[i] != '!' && str[i] != ',' && str[i] != '-')
                {
                    counter++;
                }
                else if (counter != 0)
                {
                    if (counter < min)
                    {
                        min = counter;
                        detectSmallest.start_index = i - counter;
                    }

                    counter = 0;
                }

            }
            detectSmallest.lenght = min;
            Console.WriteLine();
            for (int i = detectSmallest.start_index; i < detectSmallest.lenght + detectSmallest.start_index; i++) Console.Write(str[i]);
            Console.WriteLine();

        }
        static public void OutBiggestWord(string str)
        {
            HelpString detectBiggest = new HelpString();
            int counter = 0;
            int max = -9999;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' && str[i] != ',' && str[i] != '.' && str[i] != '!' && str[i] != ',' && str[i] != '-')
                {
                    counter++;
                }
                else if (counter != 0)
                {
                    if (counter > max)
                    {
                        max = counter;
                        detectBiggest.start_index = i - counter;
                    }

                    counter = 0;
                }
                if (i == str.Length - 1)
                {
                    if (counter > max)
                    {
                        max = counter;
                        detectBiggest.start_index = i - counter + 1;
                    }

                    counter = 0;
                }

            }
            detectBiggest.lenght = max;
            Console.WriteLine();
            for (int i = detectBiggest.start_index; i < detectBiggest.lenght + detectBiggest.start_index; i++) Console.Write(str[i]);
            Console.WriteLine();
        }
        static public void OutBiggestWord(StringBuilder str)
        {
            HelpString detectBiggest = new HelpString();
            int counter = 0;
            int max = -9999;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' && str[i] != ',' && str[i] != '.' && str[i] != '!' && str[i] != ',' && str[i] != '-')
                {
                    counter++;
                }
                else if (counter != 0)
                {
                    if (counter > max)
                    {
                        max = counter;
                        detectBiggest.start_index = i - counter;
                    }

                    counter = 0;
                }
                if (i == str.Length - 1)
                {
                    if (counter > max)
                    {
                        max = counter;
                        detectBiggest.start_index = i - counter + 1;
                    }

                    counter = 0;
                }

            }
            detectBiggest.lenght = max;
            Console.WriteLine();
            for (int i = detectBiggest.start_index; i < detectBiggest.lenght + detectBiggest.start_index; i++) Console.Write(str[i]);
            Console.WriteLine();

        }
        static public void OutBiggestWord(char[] str)
        {
            HelpString detectBiggest = new HelpString();
            int counter = 0;
            int max = -9999;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' && str[i] != ',' && str[i] != '.' && str[i] != '!' && str[i] != ',' && str[i] != '-' && str[i] != ')' && str[i] != '(' && str[i] != '=')
                {
                    counter++;
                }
                else if (counter != 0)
                {
                    if (counter > max)
                    {
                        max = counter;
                        detectBiggest.start_index = i - counter;
                    }

                    counter = 0;
                }
                if (i == str.Length - 1)
                {
                    if (counter > max)
                    {
                        max = counter;
                        detectBiggest.start_index = i - counter + 1;
                    }

                    counter = 0;
                }

            }
            Console.WriteLine();
            detectBiggest.lenght = max;
            for (int i = detectBiggest.start_index; i < detectBiggest.lenght + detectBiggest.start_index; i++) Console.Write(str[i]);
            Console.WriteLine();
        }
    }

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = true;
            Console.WriteLine("Введите строку:");
            string buf_str = Console.ReadLine();
            ConsoleKeyInfo Key;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Ваша строка:\n{0}", buf_str);
                Console.WriteLine("Выберите действие:\n1.Найти самое маленькое слово\n2.Найти самое большое слово");
                Key = Console.ReadKey();
                switch (Key.KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine("\nЧто использовать?\n1.string\n2.StringBuilder\n3.char[]");
                            Key = Console.ReadKey();
                            switch (Key.KeyChar)
                            {
                                case '1':
                                    {
                                        StringWork.OutSmallestWord(buf_str);
                                        break;
                                    }
                                case '2':
                                    {
                                        StringBuilder string_builder = new StringBuilder();
                                        string_builder.Append(buf_str);
                                        StringWork.OutSmallestWord(string_builder);
                                        break;
                                    }
                                case '3':
                                    {
                                        char[] char_string = new char[buf_str.Length];
                                        buf_str.CopyTo(0, char_string, 0, buf_str.Length);
                                        StringWork.OutSmallestWord(char_string);
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("\nТакого пункта нет!");
                                        break;
                                    }
                            }
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("\nЧто использовать?\n1.string\n2.StringBuilder\n3.char[]");
                            Key = Console.ReadKey();
                            switch (Key.KeyChar)
                            {
                                case '1':
                                    {
                                        StringWork.OutBiggestWord(buf_str);
                                        break;
                                    }
                                case '2':
                                    {
                                        StringBuilder string_builder = new StringBuilder(buf_str);
                                        StringWork.OutBiggestWord(string_builder);
                                        break;
                                    }
                                case '3':
                                    {
                                        char[] char_string = new char[buf_str.Length];
                                        buf_str.CopyTo(0, char_string, 0, buf_str.Length);
                                        StringWork.OutBiggestWord(char_string);
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("\nТакого пункта нет!");
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nТакого пункта нет!");
                            break;
                        }
                }
                Console.WriteLine("Ещё?");
                exit = Convert.ToBoolean(Console.ReadLine());
            }

        }
    }
}
