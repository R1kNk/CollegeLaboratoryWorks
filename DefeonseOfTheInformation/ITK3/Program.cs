using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Pleifera_crypt
{
    static string rus_alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
    static public bool checkString(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsLetter(str[i]) || !rus_alphabet.Contains(str[i])) return false;
        }
        return true;
    }
    static public bool IsThereSymbol(char[,] matrix, char symbol)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == symbol) return true;
            }
        return false;
    }
    static public int[] FindCryptedSymbol(char[,] matrix, char symbol)
    {
        int[] res = new int[2];

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == symbol)
                {
                    res[0] = i;
                    res[1] = j;
                    return res;
                }
            }
        }
        return res;
    }
    static public string Crypt(string keyWord, string word)
    {
        if (word != String.Empty||keyWord!=String.Empty)
        {
            List<int> UnHonestSymbols = new List<int>();

            Console.WriteLine();
            if (checkString(word) && checkString(keyWord))
            {
                char[,] crypt_matrix = new char[8, 4];
                for (int i = 0; i < crypt_matrix.GetLength(0); i++)
                    for (int j = 0; j < crypt_matrix.GetLength(1); j++)
                        crypt_matrix[i, j] = '0';
                int counter = default(int);
                int counter_rows = default(int);
                int counter_columns = default(int);
                for (int i = 0; i < crypt_matrix.GetLength(0); i++)
                {
                    //балалайка
                    //012345678  9
                    for (int j = 0; j < crypt_matrix.GetLength(1); j++)
                    {
                        if (!IsThereSymbol(crypt_matrix, keyWord[counter]))
                        {
                            crypt_matrix[i, j] = keyWord[counter];
                            counter++;
                            if (counter == keyWord.Length) { counter_columns = j; counter_rows = i; break; }
                        }
                        else
                        {

                            while (IsThereSymbol(crypt_matrix, keyWord[counter]))
                            {
                                counter++;
                                if (counter == keyWord.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                            if (counter == keyWord.Length) { counter_columns = j; counter_rows = i; break; }
                            crypt_matrix[i, j] = keyWord[counter];
                            counter++;
                            if (counter == keyWord.Length) { counter_columns = j; counter_rows = i; break; }
                        }
                    }
                    if (counter == keyWord.Length) break;
                }
                counter = 0;

                for (int i = 0; i < crypt_matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < crypt_matrix.GetLength(1); j++)
                    {
                        if (crypt_matrix[i, j] == '0')
                        {
                            while (IsThereSymbol(crypt_matrix, rus_alphabet[counter]))
                            {
                                counter++;
                            }
                            crypt_matrix[i, j] = rus_alphabet[counter];
                            counter = 0;
                        }

                    }
                }
                for (int i = 0; i < crypt_matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < crypt_matrix.GetLength(1); j++)
                    {
                        Console.Write(" {0}", crypt_matrix[i, j]);
                    }
                    Console.WriteLine();
                }
                string result = word;
                for (int i = 1; i < result.Length; i += 2)
                    if (result[i] == result[i - 1]) { result = result.Insert(i, "ъ"); UnHonestSymbols.Add(i - 1); }
                if (result.Length % 2 != 0) { result += "ъ"; UnHonestSymbols.Add(result.Length - 1); }
                string newRes = default(string);
                Console.WriteLine(result);
                for (int i = 1; i < result.Length; i += 2)
                {
                    if (i == 0) continue;
                    int[] symb2 = FindCryptedSymbol(crypt_matrix, result[i]);
                    int[] symb1 = FindCryptedSymbol(crypt_matrix, result[i - 1]);
                    if (symb1[0] == symb2[0] && symb1[1] != symb2[1])
                    {

                        if (result[i - 1] != 'ъ')
                        {
                            if (symb1[1] == crypt_matrix.GetLength(1) - 1) newRes += crypt_matrix[symb1[0], 0];
                            else newRes += crypt_matrix[symb1[0], symb1[1] + 1];
                        }

                        if (result[i] != 'ъ')
                        {
                            if (symb2[1] == crypt_matrix.GetLength(1) - 1) newRes += crypt_matrix[symb2[0], 0];
                            else newRes += crypt_matrix[symb2[0], symb2[1] + 1];
                        }
                    }
                    else if (symb1[0] != symb2[0] && symb1[1] == symb2[1])
                    {
                        if (result[i - 1] != 'ъ')
                        {
                            if (symb1[0] == crypt_matrix.GetLength(0) - 1) newRes += crypt_matrix[0, symb1[1]];
                            else newRes += crypt_matrix[symb1[0] + 1, symb1[1]];
                        }

                        if (result[i] != 'ъ')
                        {
                            if (symb2[0] == crypt_matrix.GetLength(0) - 1) newRes += crypt_matrix[0, symb2[1]];
                            else newRes += crypt_matrix[symb2[0] + 1, symb2[1]];
                        }
                    }
                    else if (symb1[0] != symb2[0] && symb1[1] != symb2[1])
                    {


                        if (result[i - 1] != 'ъ')
                            newRes += crypt_matrix[symb1[0], symb2[1]];
                        if (result[i] != 'ъ')
                            newRes += crypt_matrix[symb2[0], symb1[1]];

                    }
                }
                int[] arr_symb = UnHonestSymbols.ToArray();
                for (int i = 0; i < arr_symb.Length; i++)
                {
                    newRes.Remove(i, 1);
                    for (int j = arr_symb[i] + 1; j < arr_symb.Length; j++)
                        arr_symb[j] -= 1;
                }
                return newRes;
            }
            else Console.WriteLine("Ключевое слово или слово для шифрования содержит не только буквы русского алфавита");
            return "";
        }
        else return "Слово или ключ введены некорректно";
    }
}
namespace ITK3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите задание:\nШифр Плейфера - 1\n2 - выход");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine("\nВведите слово-ключ");
                            string key_word = Console.ReadLine();   
                            Console.WriteLine("Слово, которое хотите зашифровать");
                            Console.WriteLine("Зашифрованное слово - {0}", Pleifera_crypt.Crypt(key_word, Console.ReadLine()));

                            Console.ReadKey();
                            break;

                        }
                    default:
                        { exit = true; break;}
                }
            }
        }
    }
}
