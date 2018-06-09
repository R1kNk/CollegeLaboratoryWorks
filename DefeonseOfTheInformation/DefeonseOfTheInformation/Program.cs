using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class CryptReshuffleWithKey
{
    static int FindMinPosInArray(ref int[] arr)
    {
        int min = 9999;
        int pos = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < min && arr[i] != -1) { min = arr[i]; pos = i; }
        }
        if(min!=9999)
        arr[pos] = -1;
       
        return pos;
    }
    static public StringBuilder ReshuffleWithKeyNumber(string crypt, int[] key, int rows, int columns)
    {
        char[,] crypt_array = new char[rows, columns];
        int counter_for_string = 0;
        for (int i = 0; i < crypt_array.GetLength(0); i++)
            for (int j = 0; j < crypt_array.GetLength(1); j++)
            {
                crypt_array[i, j] = crypt[counter_for_string];
                counter_for_string++;
            }
        int current_key_pos = 0;
        int current_pos = 0;
        char[,] result_crypt_array = new char[rows, columns];
        for(int i=0; i < crypt_array.GetLength(1);i++)
        {
            current_key_pos = FindMinPosInArray(ref key);
            if(current_key_pos!=-1)
            {
                for (int j = 0; j < crypt_array.GetLength(0); j++)
                {
                    result_crypt_array[j, current_pos] = crypt_array[j, current_key_pos]; 
                }
                current_pos++;
            }
        }
        Console.WriteLine("Матрица до преобразований:");
        for (int i = 0; i < crypt_array.GetLength(0); i++)
        {
            for (int j = 0; j < crypt_array.GetLength(1); j++)
            {
                Console.Write("{0,2}  ", crypt_array[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine("Матрица после преобразований:");
        for (int i = 0; i < result_crypt_array.GetLength(0); i++)
        {
            for (int j = 0; j < result_crypt_array.GetLength(1); j++)
            {
                Console.Write("{0,2}  ", result_crypt_array[i,j]);
            }
            Console.WriteLine();
        }
        StringBuilder result= new StringBuilder();
        
        for (int i = 0; i < result_crypt_array.GetLength(1); ++i)
        for (int j = 0; j < result_crypt_array.GetLength(0); ++j)
        result.Append(result_crypt_array[j, i]);
        return result;
    }
}
class CryptMagicSquare
{

     struct KeyAndLetter
    {
        public int key;
        public char symbol;
        public bool AlreadyUsed;
    }
    static char FindMinInStruct(ref KeyAndLetter[,] struct_arr)
    {
        int min = 9999;
        char result = '0';
        for (int i = 0; i < struct_arr.GetLength(0); i++)
            for (int j = 0; j < struct_arr.GetLength(1); j++)
            {
                if (struct_arr[i, j].key < min && struct_arr[i, j].AlreadyUsed == false) min = struct_arr[i, j].key;
            }
        if (min != 9999)
        {
            for (int i = 0; i < struct_arr.GetLength(0); i++)
                for (int j = 0; j < struct_arr.GetLength(1); j++)
                {
                    if (struct_arr[i, j].key == min && struct_arr[i, j].AlreadyUsed == false)
                    {
                        result = struct_arr[i, j].symbol;
                        struct_arr[i, j].AlreadyUsed = true;
                        break;
                    }
                }
        }
        return result;
    }
    static bool check_key_arr(int[,] key)
    {
        int buf_sum = default(int);
        double const_var = ((key.GetLength(0) * ((Math.Pow(key.GetLength(0), 2) + 1))) / 2);
        for (int i = 0; i < key.GetLength(0); i++)
        {
            for (int j = 0; j < key.GetLength(1); j++)
            {
                buf_sum += key[i, j];
            }
            if (buf_sum != (int)const_var) return false;
            buf_sum = 0;
        }
        return true;
    }
    static public StringBuilder DeCryptMagicSquare(string crypted, int[,] key)
    {
        KeyAndLetter[,] MagicSquare = new KeyAndLetter[key.GetLength(0), key.GetLength(1)];
        if (check_key_arr(key))
        {
            int counter_symbols = 0;
            char buf_char = '0';
            for (int i = 0; i < MagicSquare.GetLength(0); i++)
                for (int j = 0; j < MagicSquare.GetLength(1); j++)
                {
                    MagicSquare[i, j].key = key[i, j];
                    MagicSquare[i, j].symbol = crypted[counter_symbols]; counter_symbols++;
                }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < MagicSquare.GetLength(0) * MagicSquare.GetLength(1); i++)
            {
                buf_char = FindMinInStruct(ref MagicSquare);
                result.Append(buf_char);
            }
            return result;
        }
        StringBuilder msg = new StringBuilder("Неверный магический квадрат. Сообщение не расшифровано");
        return msg;

    } 
}
namespace DefeonseOfTheInformation
{
    
    class Program
    {
       static public bool ChooseVariant()
        {
            bool choose = false;
            try
            {
                choose = Convert.ToBoolean(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("false");
            }
            return choose;
        }
        static void Main(string[] args)
        {
            bool exit = true;
            ConsoleKeyInfo Key;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("1.Зашифровка сообщения одиночной перестановкой с ключом.\n2.Расшифровка сообщения методом магического квадрата");
                Key = Console.ReadKey();
                Console.WriteLine();
                switch (Key.KeyChar)
                {
                    case '1':
                        {
                            string crypt = "НИКТО_НИЧЕГО_НЕ_МОЖЕТ_СКАЗАТЬ_ПРО ВАС._ЧТО_БЫ_ЛЮДИ_НЕ_ГОВОРИЛИ,_ОНИ_ГОВОРЯТ_ПРО_САМИХ_СЕБЯ";
                            Console.WriteLine("Хотите зашифровать свою фразу?");
                            if (ChooseVariant())
                            {
                                Console.WriteLine("Введите строку для зашифровки");
                                crypt = Console.ReadLine();
                                Console.WriteLine("Введите размеры таблицы для зашифровки");
                                int rows = Convert.ToInt32(Console.ReadLine());
                                int columns = Convert.ToInt32(Console.ReadLine());
                                if (rows * columns == crypt.Length)
                                {
                                    int[] key = new int[columns];
                                    Console.WriteLine("Ваш массив ключей будет равен {0}", columns);
                                    for (int i = 0; i < key.Length; i++)
                                    {
                                        Console.WriteLine("Введите элемент массива ключей {0}", i);
                                        key[i] = Convert.ToInt32(Console.ReadLine());
                                    }
                                    Console.WriteLine("Исходное сообщение: {0}\nЗашифрованное сообщение:\n{1}", crypt, CryptReshuffleWithKey.ReshuffleWithKeyNumber(crypt, key, rows, columns));

                                }
                                else Console.WriteLine("Неверный размер таблицы");
                            }
                            else
                            {
                                int[] key = { 7, 1, 3, 2, 5, 4, 9, 8, 6 };
                                Console.WriteLine("Исходное сообщение: {0}\nЗашифрованное сообщение:\n{1}", crypt, CryptReshuffleWithKey.ReshuffleWithKeyNumber(crypt, key, 10,9));
                            }
                            break;
                        }
                    case '2':
                        {
                            string Crypted_square;
                            Console.WriteLine("Хотите расшифоваь фразу по умолчанию?");
                            if (ChooseVariant())
                            {


                                Crypted_square = "АЕЧЖД_ССИЬ_ОВВМ_";
                                int[,] key_square = { { 13, 8, 12, 1 }, { 2, 11, 7, 14 }, { 3, 10, 6, 15 }, { 16, 5, 9, 4 } };
                                Console.WriteLine("Строка для дешифровки: {0}\nРезультат: {1}", Crypted_square, CryptMagicSquare.DeCryptMagicSquare(Crypted_square, key_square));
                            }
                            else
                            {
                                Console.WriteLine("Введите строку для дешифровки");
                                Crypted_square = Console.ReadLine();
                                double size = Math.Sqrt(Crypted_square.Length);
                                if ((int)size * (int)size == Crypted_square.Length)
                                {
                                    int[,] key = new int[(int)size, (int)size];
                                    for(int i=0; i< key.GetLength(0); i++)
                                        for (int j = 0; j < key.GetLength(1); j++)
                                        {
                                            Console.WriteLine("Введите элемент {0} {1}", i, j);
                                            key[i, j] = Convert.ToInt32(Console.ReadLine());
                                        }
                                    Console.WriteLine("Строка для дешифровки: {0}\nРезультат: {1}", Crypted_square, CryptMagicSquare.DeCryptMagicSquare(Crypted_square, key));

                                }
                                else Console.WriteLine("Это сообщение не может быть расшифровано данным способом");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Такого пункта нет!");
                        }
                        break;
                }

                Console.WriteLine("Ещё?");
                try
                { exit = Convert.ToBoolean(Console.ReadLine()); }
                catch(System.FormatException)
                {
                    exit = false;
                }
            }
        }
    }
}
 