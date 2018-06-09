using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITK4
{
    class Vijener_System
    {
        static string alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
       private static string TransformKey(string key, string word)
        {
            string buf = key;
            int count = 0;
            key = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] != ' ')
                {
                    key += buf[count];
                    count++;
                    if (count == buf.Length)
                    {
                        count = 0;
                    }
                }
                else
                    key += ' ';
            }
            return key;
        }
      public  static string Crypt(string input, string keyword)
        {
            keyword = TransformKey(keyword, input);
            Console.WriteLine("Ключ -  {0}", keyword);
            string result = default(string);
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != ' ')
                {
                    int keyind = default(int);
                    int inputind = default(int);
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (keyword[i] == alphabet[j])
                            keyind = j;
                        if (input[i] == alphabet[j])
                            inputind = j;
                    }
                    int s = keyind + inputind + 1;
                    while (s > 32)
                        s -= 32;
                    result += alphabet[s];
                }
                else
                 result += " ";
            }
            return result;
        }
        public static string DeCrypt(string input, string keyword)
        {
            string result = default(string);
            keyword = TransformKey(keyword, input);
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != ' ')
                {
                    int keyind = 0;
                    int inputind = 0;
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (keyword[i] == alphabet[j])
                        {
                            keyind = j;
                            break;
                        }
                    }
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (input[i] == alphabet[j])
                        {
                            inputind = j - keyind - 1;
                            break;
                        }
                    }
                    while (inputind < 0)
                        inputind += 32;
                    result += alphabet[inputind];
                }
                else
                {
                    result += " ";
                }
            }
            return result;  
        }
    }
    class DoubleSquare_crypt
    {
        static string rus_alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        static public bool checkString(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]) || !rus_alphabet.Contains(Char.ToLower(str[i]))) return false;
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
        static public string Crypt(string keyWord1, string keyWord2, string word)
        {
            if (word != String.Empty || keyWord1 != String.Empty|| keyWord2 != String.Empty)
            {
                List<int> UnHonestSymbols = new List<int>();
                for (int i = 1; i < word.Length; i += 2)
                    if (word[i] == word[i - 1]) { word = word.Insert(i, "ъ"); UnHonestSymbols.Add(i - 1); }
                if (word.Length % 2 != 0) { word += "ъ"; UnHonestSymbols.Add(word.Length - 1); }
                Console.WriteLine();
                if (checkString(word) && checkString(keyWord1) && checkString(keyWord2))
                {
                    char[,] crypt_matrix1 = new char[8, 4];
                    char[,] crypt_matrix2 = new char[8, 4];

                    for (int i = 0; i < crypt_matrix1.GetLength(0); i++)
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                            crypt_matrix1[i, j] = '0';
                    ///
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                            crypt_matrix2[i, j] = '0';
                    int counter = default(int);
                    int counter_rows = default(int);
                    int counter_columns = default(int);
                    for (int i = 0; i < crypt_matrix1.GetLength(0); i++)
                    {
                       
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                        {
                            if (!IsThereSymbol(crypt_matrix1, keyWord1[counter]))
                            {
                                crypt_matrix1[i, j] = keyWord1[counter];
                                counter++;
                                if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                            else
                            {

                                while (IsThereSymbol(crypt_matrix1, keyWord1[counter]))
                                {
                                    counter++;
                                    if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                                }
                                if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                                crypt_matrix1[i, j] = keyWord1[counter];
                                counter++;
                                if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                        }
                        if (counter == keyWord1.Length) break;
                    }
                    counter = 0;
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        //балалайка
                        //012345678  9
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                        {
                            if (!IsThereSymbol(crypt_matrix2, keyWord2[counter]))
                            {
                                crypt_matrix2[i, j] = keyWord2[counter];
                                counter++;
                                if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                            else
                            {

                                while (IsThereSymbol(crypt_matrix2, keyWord2[counter]))
                                {
                                    counter++;
                                    if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                                }
                                if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                                crypt_matrix2[i, j] = keyWord2[counter];
                                counter++;
                                if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                        }
                        if (counter == keyWord2.Length) break;
                    }

                    ///////
                    counter = 0;
                
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                        {
                            if (crypt_matrix1[i, j] == '0')
                            {
                                while (IsThereSymbol(crypt_matrix1, rus_alphabet[counter]))
                                {
                                    counter++;
                                }
                                crypt_matrix1[i, j] = rus_alphabet[counter];
                                counter = 0;
                            }

                        }
                    }
                    counter = 0;
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                        {
                            if (crypt_matrix2[i, j] == '0')
                            {
                                while (IsThereSymbol(crypt_matrix2, rus_alphabet[counter]))
                                {
                                    counter++;
                                }
                                crypt_matrix2[i, j] = rus_alphabet[counter];
                                counter = 0;
                            }

                        }
                    }
                    //////////////////////////
                    counter = 0;
                    Console.WriteLine("Первая таблица:\n");
                    for (int i = 0; i < crypt_matrix1.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                        {
                            Console.Write(" {0}", crypt_matrix1[i, j]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Вторая таблица:\n");
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                        {
                            Console.Write(" {0}", crypt_matrix2[i, j]);
                        }
                        Console.WriteLine();
                    }
                    //
                    string result = word;
                 
                    string newRes = default(string);
                    Console.WriteLine(result);
                    for (int i = 1; i < result.Length; i += 2)
                    {
                        if (i == 0) continue;
                        int[] symb2 = FindCryptedSymbol(crypt_matrix2, result[i]);
                        int[] symb1 = FindCryptedSymbol(crypt_matrix1, result[i - 1]);
                        
                        if (symb1[0] == symb2[0] && symb1[1] != symb2[1])
                        {
                            
                                if (symb1[1] == crypt_matrix1.GetLength(1) - 1) newRes += crypt_matrix1[symb1[0], 0];
                                else newRes += crypt_matrix1[symb1[0], symb1[1] + 1];

                                if (symb2[1] == crypt_matrix2.GetLength(1) - 1) newRes += crypt_matrix2[symb2[0], 0];
                                else newRes += crypt_matrix2[symb2[0], symb2[1] + 1];


                        }
                        else if (symb1[0] != symb2[0] && symb1[1] == symb2[1])
                        {
                          
                                if (symb1[0] == crypt_matrix1.GetLength(0) - 1) newRes += crypt_matrix1[0, symb1[1]];
                                else newRes += crypt_matrix1[symb1[0] + 1, symb1[1]];

                            
                                if (symb2[0] == crypt_matrix2.GetLength(0) - 1) newRes += crypt_matrix2[0, symb2[1]];
                                else newRes += crypt_matrix2[symb2[0] + 1, symb2[1]];
                        }
                        else if (symb1[0] != symb2[0] && symb1[1] != symb2[1])
                        {


                                newRes += crypt_matrix1[symb1[0], symb2[1]];
                                newRes += crypt_matrix2[symb2[0], symb1[1]];

                        }
                        else
                        {
                            newRes += result[i-1];
                            newRes += result[i];
                        }
                    }
                   
                    return newRes;
                }
                else Console.WriteLine("Ключевое слово или слово для шифрования содержит не только буквы русского алфавита");
                return "";
            }
            else return "Слово или ключ введены некорректно";
        }
        static public string DeCrypt(string keyWord1, string keyWord2, string word)
        {
            if (word != String.Empty || keyWord1 != String.Empty || keyWord2 != String.Empty)
            {
                List<int> UnHonestSymbols = new List<int>();

                Console.WriteLine();
                if (checkString(word) && checkString(keyWord1) && checkString(keyWord2))
                {
                    char[,] crypt_matrix1 = new char[8, 4];
                    char[,] crypt_matrix2 = new char[8, 4];

                    for (int i = 0; i < crypt_matrix1.GetLength(0); i++)
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                            crypt_matrix1[i, j] = '0';
                    ///
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                            crypt_matrix2[i, j] = '0';
                    int counter = default(int);
                    int counter_rows = default(int);
                    int counter_columns = default(int);
                    for (int i = 0; i < crypt_matrix1.GetLength(0); i++)
                    {
                        //балалайка
                        //012345678  9
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                        {
                            if (!IsThereSymbol(crypt_matrix1, keyWord1[counter]))
                            {
                                crypt_matrix1[i, j] = keyWord1[counter];
                                counter++;
                                if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                            else
                            {

                                while (IsThereSymbol(crypt_matrix1, keyWord1[counter]))
                                {
                                    counter++;
                                    if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                                }
                                if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                                crypt_matrix1[i, j] = keyWord1[counter];
                                counter++;
                                if (counter == keyWord1.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                        }
                        if (counter == keyWord1.Length) break;
                    }
                    counter = 0;
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        //балалайка
                        //012345678  9
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                        {
                            if (!IsThereSymbol(crypt_matrix2, keyWord2[counter]))
                            {
                                crypt_matrix2[i, j] = keyWord2[counter];
                                counter++;
                                if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                            else
                            {

                                while (IsThereSymbol(crypt_matrix2, keyWord2[counter]))
                                {
                                    counter++;
                                    if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                                }
                                if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                                crypt_matrix2[i, j] = keyWord2[counter];
                                counter++;
                                if (counter == keyWord2.Length) { counter_columns = j; counter_rows = i; break; }
                            }
                        }
                        if (counter == keyWord2.Length) break;
                    }

                    ///////
                    counter = 0;

                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                        {
                            if (crypt_matrix1[i, j] == '0')
                            {
                                while (IsThereSymbol(crypt_matrix1, rus_alphabet[counter]))
                                {
                                    counter++;
                                }
                                crypt_matrix1[i, j] = rus_alphabet[counter];
                                counter = 0;
                            }

                        }
                    }
                    counter = 0;
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                        {
                            if (crypt_matrix2[i, j] == '0')
                            {
                                while (IsThereSymbol(crypt_matrix2, rus_alphabet[counter]))
                                {
                                    counter++;
                                }
                                crypt_matrix2[i, j] = rus_alphabet[counter];
                                counter = 0;
                            }

                        }
                    }
                    //////////////////////////
                    counter = 0;
                    Console.WriteLine("Первая таблица:\n");
                    for (int i = 0; i < crypt_matrix1.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix1.GetLength(1); j++)
                        {
                            Console.Write(" {0}", crypt_matrix1[i, j]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Вторая таблица:\n");
                    for (int i = 0; i < crypt_matrix2.GetLength(0); i++)
                    {
                        for (int j = 0; j < crypt_matrix2.GetLength(1); j++)
                        {
                            Console.Write(" {0}", crypt_matrix2[i, j]);
                        }
                        Console.WriteLine();
                    }
                    //
                    string result = word;
                    string newRes = default(string);
                    Console.WriteLine(result);
                    for (int i = 1; i < result.Length; i += 2)
                    {
                        if (i == 0) continue;
                        int[] symb2 = FindCryptedSymbol(crypt_matrix2, result[i]);
                        int[] symb1 = FindCryptedSymbol(crypt_matrix1, result[i - 1]);
                        if (symb1[0] == symb2[0] && symb1[1] != symb2[1])
                        {

                            if (symb1[1] == 0) newRes += crypt_matrix1[symb1[0], crypt_matrix1.GetLength(1) - 1];
                            else newRes += crypt_matrix1[symb1[0], symb1[1] - 1];

                            if (symb2[1] == 0) newRes += crypt_matrix2[symb2[0], crypt_matrix2.GetLength(1) - 1];
                            else newRes += crypt_matrix2[symb2[0], symb2[1] - 1];

                        }
                        else if (symb1[0] != symb2[0] && symb1[1] == symb2[1])
                        {

                            if (symb1[0] == 0) newRes += crypt_matrix1[crypt_matrix1.GetLength(1) - 1, symb1[1]];
                            else newRes += crypt_matrix1[symb1[0] - 1, symb1[1]];


                            if (symb2[0] == 0) newRes += crypt_matrix2[crypt_matrix2.GetLength(1) - 1, symb2[1]];
                            else newRes += crypt_matrix2[symb2[0] - 1, symb2[1]];
                        }
                        else if (symb1[0] != symb2[0] && symb1[1] != symb2[1])
                        {


                            newRes += crypt_matrix1[symb1[0], symb2[1]];
                            newRes += crypt_matrix2[symb2[0], symb1[1]];

                        }
                        else
                        {
                            newRes += result[i - 1];
                            newRes += result[i];
                        }
                    }
                    
                   for (int i = 0; i < newRes.Length; i++)
                    {
                        if(newRes[i]=='ъ')
                        newRes = newRes.Remove(i, 1);
                    }
                    return newRes;
                }
                else Console.WriteLine("Ключевое слово или слово для шифрования содержит не только буквы русского алфавита");
                return "";
            }
            else return "Слово или ключ введены некорректно";
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите задание:\n1 - Шифр Виженера\n2 - Двойной квадрат Уитсона\n3 - Выход");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            bool exit_c = false;
                            while (!exit_c)
                            {
                                Console.WriteLine("\n1 - Зашифровать\n2 - Расшифровать\n3 - Выход");
                                 key = Console.ReadKey();
                                string key_word = default(string);
                                string word = default(string);
                                switch (key.KeyChar)
                                {
                                    case '1':
                                        {
                                            Console.WriteLine("\nВведите слово-ключ");
                                            key_word = Console.ReadLine();
                                            Console.WriteLine("Слово, которое хотите зашифровать");
                                            word = Console.ReadLine();
                                            Console.WriteLine("Зашифрованное слово - {0}", Vijener_System.Crypt(word, key_word));
                                            Console.ReadKey();

                                            break;
                                        }
                                    case '2':
                                        {
                                            Console.WriteLine("\nВведите слово-ключ");
                                            key_word = Console.ReadLine();
                                            Console.WriteLine("Введите слово для расшифровки:");
                                            word = Console.ReadLine();
                                            Console.WriteLine("Расшифрованное слово - {0}", Vijener_System.DeCrypt(word, key_word));
                                            Console.ReadKey();
                                            break;
                                        }
                                    case '3':
                                        {
                                            exit_c = true;
                                            break;
                                        }
                                }
                            }
                                        

                            
                           
                            Console.ReadKey();
                            break;

                        }
                    case '2':
                        {
                            Console.WriteLine("\nВведите слово-ключ для первой матрицы:");
                            string key_word1 = Console.ReadLine();
                            Console.WriteLine("\nВведите слово-ключ для второй матрицы:");
                            string key_word2 = Console.ReadLine();
                            Console.WriteLine("Слово, которое хотите зашифровать");
                            string word = Console.ReadLine();

                            Console.WriteLine("Зашифрованное слово - {0}", DoubleSquare_crypt.Crypt(key_word1, key_word2, word));
                            Console.WriteLine("Введите слово для рашифровки с тем же ключом:");
                            word = Console.ReadLine();
                            Console.WriteLine("Расшифрованное слово - {0}", DoubleSquare_crypt.DeCrypt(key_word1,key_word2,word));


                            Console.ReadKey();
                            break;
                            
                        }
                    default:
                        { exit = true; break; }
                }
            }
        }
    }
}
