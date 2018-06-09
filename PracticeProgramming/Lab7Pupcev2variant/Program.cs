using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7Pupcev2variant;


public class Task2Var
{
    static string rus_alphabet = "йцукенгшщзхъфывапролджэячсмитьбю";
    static public string ReWorkRusandEngLetters(char[] array)
    {
        char[] result = new char[array.Length];
        int position = 0;
        for (int i = 0; i < array.Length; i++)
        if (IsItEng(array[i]) != '0') { result[position] = array[i]; position++; }
        for (int i = 0; i < array.Length; i++)
        if (IsItRus(array[i]) != '1') { result[position] = array[i]; position++; }
        return new string(result);
    }
    static char IsItEng(char character)
    {
        if (char.IsLower(character))
        {
            string engAlph = "qwertyuiopasdfghjklzxcvbnm";
            for (int i = 0; i < engAlph.Length; i++)
                if (character == engAlph[i])
                {
                    Console.WriteLine(char.ToUpper(character));
                    return char.ToUpper(character);
                }
        }
        else if(char.IsUpper(character))
        {
            string upperEngAlph = "QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < upperEngAlph.Length; i++)
                if (character == upperEngAlph[i])
                {
                    return character;
                }
        }
        return '0';
    }
    static char IsItRus(char character)
    {
       
            string rusAlph = "йцукенгшщзхъфывапролджэячсмитьбю";
            for (int i = 0; i < rusAlph.Length; i++)
                if (character == rusAlph[i])
                {
                    return character;
                }
        return '1';
    }
    static public string AppendNe(string word)
    {
        string ne = "не";
        string result = "";
        result += ne += word;
        return result;
    }
    static public string RemoveSpaces(string word)
    {
        while(word.Contains("  "))
        {
            word = word.Replace("  ", " ");
        }
        if (word[0] == ' ')
        word=word.Remove(0, 1);
        if (word[word.Length - 1] == ' ') word = word.Remove(word.Length - 1, 1);
        return word;
    }
    static string FindSmallestWord(string words, out int number)
    {
        int start_index = default(int);
        int length = default(int);
        number = default(int);
        //
        int counter = 0;
        int min = 9999;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i] != '#')
            {
                counter++;
            }
            else if (counter != 0)
            {
                if (counter < min)
                {
                    min = counter;
                    start_index = i - counter;
                }

                counter = 0;
            }

        }
        length = min;
        char[] word = new char[length];
        words.CopyTo(start_index, word, 0, length);
        int index_of_hash = start_index;
        for (int i = 0; i < index_of_hash; i++)
            if (words[i] == '#') number++;
        number++;
        return new string(word);
    }
    static public void WorkWithHashSymbol(string words)
    {
        while (words.Contains("##"))
        {
            words = words.Replace("##", "#");
        }
        if (words[0] == '#')
            words = words.Remove(0, 1);
        if (words[words.Length - 1] == '#') words = words.Remove(words.Length - 1, 1);
        words += "#";
        int counter = default(int);
        for (int i = 0; i < words.Length; i++) if (words[i] == '#') counter++;
        counter++;
        string[] data_words = new string[counter];
        int count_a = 0;
        int buf = 0;
        char[] word;
        int counter_symbols = 0;
            for (int i = 0; i < words.Length;)
            {
            if (words[i] != '#')
            {
                counter_symbols++;
                i++;
                continue;
            }
            else if (words[i] == '#')
            {
                word = new char[counter_symbols];
                words.CopyTo(i - counter_symbols, word, 0, counter_symbols);
                i++;
                counter_symbols = 0;
                count_a = 0;
                for (int j = 0; j < word.Length; j++)
                {
                    if (word[j] == 'a' || word[j] == 'а') count_a++;
                }
                Console.Write("\nВ слове ");
                Console.Write(word);
                Console.Write(" {0} букв а", count_a);
                continue;
            }
        }       
        int kk;
        Console.WriteLine("\nСамое маленькое слово - {0}\nЕго позиция {1}",FindSmallestWord(words,out kk),kk);

    }
    static public int SumNumbers(Int64 number)
    {
        string str_number = number.ToString();
        int buf_number = default(int);
        int result_sum = default(int);
        Console.WriteLine(str_number);
        if (str_number[0] == '-') { str_number=str_number.Remove(0, 1); }
        for (int i = 0; i < str_number.Length; i++)
        {

            buf_number = Convert.ToInt32(str_number[i].ToString());
            result_sum += buf_number;
        }
        return result_sum;
    }
    static public string CryptAlphabet(string words)
    {
        Dictionary<char, char> Alphabet = new Dictionary<char, char>();
        Random rand_alphabet = new Random();
        char rand_symbol = default(char);
        int counter = default(int);
        int number = default(int);
        int buf = 1;
        
        while (counter != 32)
        {
            number = rand_alphabet.Next(1, 32);
            rand_symbol = IntToChar(number);
            if (Alphabet.ContainsValue(rand_symbol)==false)
            {
                Alphabet.Add(rus_alphabet[counter], rand_symbol);
                counter++;  
            }
            else
            {
                while(Alphabet.ContainsValue(rand_symbol) == false)
                {
                    rand_symbol = IntToChar(number + buf);
                    buf++;
                }
                Alphabet.Add(rus_alphabet[counter], rand_symbol);
                counter++;
                buf = 1;
            }

        }
        Console.WriteLine("Алфавит:");
        for (int i = 0; i < 32; i++)
        {
            Console.WriteLine("{0} - {1}", rus_alphabet[i], FindInDictionary(rus_alphabet[i], Alphabet));
        }
        string result_word = default(string);
        for (int i = 0; i < words.Length; i++)
        {
            if (Alphabet.ContainsKey(words[i]))
            {
                foreach (KeyValuePair<char, char> kvp in Alphabet)
                {
                    if (words[i] == kvp.Key) result_word += kvp.Value;
                }
            }
            else { result_word += words[i]; continue; }
        }
        return result_word;
    }
    static char FindInDictionary(char symbol, Dictionary<char,char> Dic)
    {
        foreach (KeyValuePair<char, char> kvp in Dic)
        {
            if (kvp.Key == symbol) return kvp.Value;
        }
        return '0';
    }
    static char IntToChar(int numb)
    {
        for (int i = 0; i < rus_alphabet.Length; i++)
            if (numb == i + 1) return rus_alphabet[i];
        return '0';
    }
}
namespace Lab7Pupcev2variant
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
                            Console.WriteLine("Введите ваш массив элементов");
                            string rr = Console.ReadLine();
                            bool ok = true;
                            for (int i = 0; i < rr.Length; i++)
                                if (!char.IsLetter(rr[i])) ok = false;
                            if (ok)
                            {
                                char[] arr = new char[rr.Length];
                                for (int i = 0; i < arr.Length; i++)
                                arr[i] = rr[i];
                                rr=Task2Var.ReWorkRusandEngLetters(arr);
                                Console.WriteLine("Результат:\n{0}", rr);
                            }
                            else Console.WriteLine("Ваш массив состоит не только из букв");
                            Console.ReadKey();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("Введите слово к которому хотите добавить не:");
                            string word = Console.ReadLine();
                            Console.WriteLine("Результат:\n{0}", Task2Var.AppendNe(word));
                            Console.ReadKey();
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine("Введите строку в которой хотите убрать лишние пробелы");
                            string word = Console.ReadLine();
                            Console.WriteLine("\n{0}", Task2Var.RemoveSpaces(word));
                            Console.ReadKey();
                            break;  
                        }
                    case '4':
                        {
                            Task2Var.WorkWithHashSymbol(Console.ReadLine());
                            Console.ReadLine();
                            break;
                        }
                    case '5':
                        {
                            Console.WriteLine("Введите число, сумму которого вы хтите посчитать");
                            Int64 num = Convert.ToInt64(Console.ReadLine());
                            Console.WriteLine("Сумма цифр данного числа: {0}",Task2Var.SumNumbers(num));
                            Console.ReadKey();
                            break;
                        }
                    case '6':
                        {
                            Console.WriteLine("Введите слово для шифровки");
                            Console.WriteLine("Зашифрованное слово - {0}",Task2Var.CryptAlphabet(Console.ReadLine()));

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
