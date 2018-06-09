using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
class ITK
{
    private static string kek = @"\\d";
   static public string InsertString(string s1, string s2, char InsertSymbol, bool InsertBeforeSymbol)
    {
        
        StringBuilder s1_st = new StringBuilder(s1);
        int counter = default(int);
        for (int i = 0; i < s1_st.Length; i++)
            if (s1_st[i] == InsertSymbol) counter++;
        string[] symbols_n_string = new string[counter];
        counter = 0;
        for (int i = 0; i < s1_st.Length; i++)
            if (s1_st[i] == InsertSymbol)
            {
                symbols_n_string[counter] = Convert.ToString(i);
                counter++;

            }
        if (counter > 0)
        {
            if (InsertBeforeSymbol)
            {
                for (int i = 0; i < symbols_n_string.Length; i++)
                {
                    s1_st.Insert(Convert.ToInt32(symbols_n_string[i]), s2);
                    for (int j = i + 1; j < symbols_n_string.Length; j++)
                        symbols_n_string[j] = Convert.ToString(Convert.ToInt32(symbols_n_string[j]) + s2.Length);
                }
            }
            else
            {
                for (int i = 0; i < symbols_n_string.Length; i++)
                {
                    s1_st.Insert(Convert.ToInt32(symbols_n_string[i]) + 1, s2);
                    for (int j = i + 1; j < symbols_n_string.Length; j++)
                        symbols_n_string[j] = Convert.ToString(Convert.ToInt32(symbols_n_string[j]) + s2.Length);
                }
            }
            return s1_st.ToString();
        }
        else return "В этой строке нет этого символа";
    }
    static public char InsertSymbol()
    {
        try
        {
            Console.WriteLine("Введите ключевой символ");
            char symbol = Convert.ToChar(Console.ReadLine());
            return symbol;
        }
        catch (System.FormatException)
        {
            Console.WriteLine("Символ введен некорректно, попробуйте ещё раз");
            InsertSymbol();
        }
        return '0';
    }
    static bool IsStringNumber(string str)
    {
        for (int i = 0; i < str.Length; i++)
            if (!char.IsNumber(str[i]))
            {
                return false;   
            }
        return true;
    }
    static bool IsThereOnlyLettersInStroke(string str)
    {
        for (int i = 0; i < str.Length; i++)
            if (!char.IsLetter(str[i])) return false;
        return true;
    }
    public static void Out_SortedInfo(ZNAK[] arr)
    {
        int i = 1;
        var sorted_arr = from n in arr
                         orderby n.Date_Birth[2], n.Date_Birth[1], n.Date_Birth[0] ascending
                         select n;
        foreach (var buf in sorted_arr) {
            Console.WriteLine("N{6}:\nИмя: {0}\nФамилия: {1}\nДата рождения: {2}.{3}.{4}\nЗнак зодиака: {5}\n", buf.Name, buf.Surname, buf.Date_Birth[0], buf.Date_Birth[1], buf.Date_Birth[2], buf.Zodiak_Sign,i);
            i++;
        };
    }
    public static void Out_Info(ZNAK[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        Console.WriteLine("N{6}:\nИмя: {0}\nФамилия: {1}\nДата рождения: {2}.{3}.{4}\nЗнак зодиака: {5}\n", arr[i].Name, arr[i].Surname, arr[i].Date_Birth[0], arr[i].Date_Birth[1], arr[i].Date_Birth[2], arr[i].Zodiak_Sign, i+1);

    }
    public static void SearchInfo (ZNAK[] arr, string sur)
    {
        int matches = default(int);
        for (int i = 0; i < arr.Length; i++)
            if (arr[i].Surname == sur) matches++;
        if (matches != 0)
        {
            
            Console.WriteLine("Найдено {0} совпадений(е):", matches);
            int current_match = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Surname == sur)
                {
                    Console.WriteLine("Совпадение {0}:\nИмя: {1}\nФамилия: {2}\nДата рождения: {3}.{4}.{5}\nЗнак зодиака: {6}\n", current_match, arr[i].Name, arr[i].Surname, arr[i].Date_Birth[0], arr[i].Date_Birth[1], arr[i].Date_Birth[2], arr[i].Zodiak_Sign);
                    current_match++;
                }
            }
        }
        else Console.WriteLine("Совпадений не найдено");
    }
    public struct ZNAK
    {
       public string Name;
        public string Surname;
        public int[] Date_Birth;
        public string Zodiak_Sign;
        public void Construct_Object()
        {
            Date_Birth = new int[3];
            string buf = default(string);
            bool is_checked = false;
            Console.WriteLine("Введите имя");
            while (!is_checked)
            {
                buf = Console.ReadLine();
                if (IsThereOnlyLettersInStroke(buf))
                {
                    Name = buf;
                    is_checked = true;
                }
                else Console.WriteLine("В имени должны быть только буквы. Попробуйте ещё раз.");
            }
            is_checked = false;
            Console.WriteLine("Введите фамилию");
            while (!is_checked)
            {
                buf = Console.ReadLine();
                if (IsThereOnlyLettersInStroke(buf))
                {
                    Surname = buf;
                    is_checked = true;
                }
                else Console.WriteLine("В фамилии должны быть только буквы. Попробуйте ещё раз.");
            }
            is_checked = false;

            int yearNow = DateTimeOffset.Now.Year;
            int monthNow = DateTimeOffset.Now.Month;
            int dayNow = DateTimeOffset.Now.Day;
            Random rand_date = new Random();
            Console.WriteLine("Зарандомить дату?");
            string answer = Console.ReadLine();
            if (answer == "y" || answer == "true")
            {
                Date_Birth[2] = rand_date.Next(1, yearNow);
                if (Date_Birth[2] == yearNow)
                    Date_Birth[1] = rand_date.Next(1, monthNow);
                else Date_Birth[1] = rand_date.Next(1, 12);
                if (Date_Birth[2] == yearNow && Date_Birth[1] == monthNow)
                    Date_Birth[0] = rand_date.Next(1, dayNow);
                else Date_Birth[0] = rand_date.Next(1, 31);
                
            }
            else
            {
                Console.WriteLine("Введите год рождения");
                while (!is_checked)
                {
                    buf = Console.ReadLine();
                    if (buf[0] != '0' && IsStringNumber(buf))
                    {
                        if ((Convert.ToInt32(buf) > yearNow))
                            Console.WriteLine("Год введен некорректно, или год больше текущего. Поробуйте ещё раз");
                        else
                        {
                            is_checked = true;
                            Date_Birth[2] = Convert.ToInt32(buf);
                        }
                    }
                    else Console.WriteLine("Год введен некорректно. Попробуйте ещё раз.");
                }
                is_checked = false;
                Console.WriteLine("Введите месяц рождения");
                while (!is_checked)
                {
                    buf = Console.ReadLine();
                    if (buf[0] != '0' && IsStringNumber(buf))
                    {
                        if (Convert.ToInt32(buf) > 12 || buf.Length > 2 || (yearNow == Date_Birth[2] && Convert.ToInt32(buf) > monthNow))
                            Console.WriteLine("Месяц введен некорректно, или больше текущего. Поробуйте ещё раз");
                        else
                        {
                            is_checked = true;
                            Date_Birth[1] = Convert.ToInt32(buf);

                        }
                    }
                    else Console.WriteLine("Месяц введен некорректно. Попробуйте ещё раз.");
                }
                is_checked = false;
                Console.WriteLine("Введите день рождения");
                while (!is_checked)
                {
                    buf = Console.ReadLine();
                    if (buf[0] != '0' && IsStringNumber(buf))
                    {
                        if (buf.Length > 2 || Convert.ToInt32(buf) > 31 || (yearNow == Date_Birth[2] && Date_Birth[1] == monthNow && Convert.ToInt32(buf) > dayNow))
                            Console.WriteLine("День введен некорректно, или больше текущего. Поробуйте ещё раз");
                        else
                        {
                            is_checked = true;
                            Date_Birth[0] = Convert.ToInt32(buf);
                        }
                    }
                    else Console.WriteLine("День введен некорректно. Попробуйте ещё раз.");

                }
                is_checked = false;
               
            }
            if ((Date_Birth[1] == 3 && Date_Birth[0] >= 21) || Date_Birth[1] == 4 && Date_Birth[0] <= 20) Zodiak_Sign = "Овен";
            else if ((Date_Birth[1] == 4 && Date_Birth[0] >= 21) || Date_Birth[1] == 5 && Date_Birth[0] <= 21) Zodiak_Sign = "Телец";
            else if ((Date_Birth[1] == 5 && Date_Birth[0] >= 22) || Date_Birth[1] == 6 && Date_Birth[0] <= 21) Zodiak_Sign = "Близнецы";
            else if ((Date_Birth[1] == 6 && Date_Birth[0] >= 22) || Date_Birth[1] == 7 && Date_Birth[0] <= 23) Zodiak_Sign = "Рак";
            else if ((Date_Birth[1] == 7 && Date_Birth[0] >= 24) || Date_Birth[1] == 8 && Date_Birth[0] <= 23) Zodiak_Sign = "Лев";
            else if ((Date_Birth[1] == 8 && Date_Birth[0] >= 24) || Date_Birth[1] == 9 && Date_Birth[0] <= 23) Zodiak_Sign = "Дева";
            else if ((Date_Birth[1] == 9 && Date_Birth[0] >= 24) || Date_Birth[1] == 10 && Date_Birth[0] <= 23) Zodiak_Sign = "Весы";
            else if ((Date_Birth[1] == 10 && Date_Birth[0] >= 24) || Date_Birth[1] == 11 && Date_Birth[0] <= 22) Zodiak_Sign = "Скорпион";
            else if ((Date_Birth[1] == 11 && Date_Birth[0] >= 23) || Date_Birth[1] == 12 && Date_Birth[0] <= 21) Zodiak_Sign = "Стрелец";
            else if ((Date_Birth[1] == 12 && Date_Birth[0] >= 22) || Date_Birth[1] == 1 && Date_Birth[0] <= 20) Zodiak_Sign = "Козерог";
            else if ((Date_Birth[1] == 1 && Date_Birth[0] >= 21) || Date_Birth[1] == 2 && Date_Birth[0] <= 19) Zodiak_Sign = "Водолей";
            else if ((Date_Birth[1] == 2 && Date_Birth[0] >= 20) || Date_Birth[1] == 3 && Date_Birth[0] <= 20) Zodiak_Sign = "Рыбы";
            else Zodiak_Sign = "unknown";
        }


    }

    }
namespace ITK_6_8_
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
                    case '9':
                        {
                            Console.WriteLine("Введите число людей:");
                            ITK.ZNAK[] arr_znak = new ITK.ZNAK[Convert.ToInt32(Console.ReadLine())];
                            for (int i = 0; i < arr_znak.Length; i++)
                            {
                                arr_znak[i].Construct_Object();
                            }
                            bool exit_under = false;
                            ConsoleKeyInfo key_two;
                            while (!exit_under)
                            {
                                Console.Clear();
                                Console.WriteLine("Выберите задание:\n1.Вывести информацию\n2.Вывести отсортированную информацию\n3.Поиск по фамилии\n4.Выйти из задания");
                                key_two = Console.ReadKey();
                                Console.WriteLine();
                                switch (key_two.KeyChar)
                                {
                                    case '1':
                                        {
                                            ITK.Out_Info(arr_znak);
                                            Console.ReadKey();
                                            break;
                                        }
                                    case '2':
                                        {
                                            ITK.Out_SortedInfo(arr_znak);
                                            Console.ReadKey();
                                            break;
                                        }
                                    case '3':
                                        {
                                            Console.WriteLine("Введите фамилию для поиска");
                                            ITK.SearchInfo(arr_znak, Console.ReadLine());
                                            Console.ReadKey();
                                            break;
                                        }
                                    case '4':
                                        {
                                            exit_under = true;
                                            break;
                                        }
                                }
                            }
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
   
