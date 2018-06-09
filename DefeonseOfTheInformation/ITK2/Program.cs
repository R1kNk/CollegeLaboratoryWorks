using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

class Cezar_crypt
{
   private char ReturnShiftedChar(string alphabet, int currentKey, int Shift)
    {
        if (Shift < alphabet.Length)
        {
            if (currentKey + Shift > alphabet.Length) return alphabet[(currentKey + Shift) - alphabet.Length];
            else if (currentKey + Shift == alphabet.Length) return alphabet[0];
            else return alphabet[currentKey + Shift - 1];
        }
        else
        {
            int buf = (Shift - (alphabet.Length - currentKey));
            if (buf > alphabet.Length)
                while (true)
                {
                    buf = buf - alphabet.Length;
                    if (buf <= alphabet.Length)
                    {
                        return alphabet[buf - 1];
                    }
                }
            else return alphabet[buf - 1];
        }
    }
   static public bool isOk(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsNumber(str[i])) return false;
        }
        return true;
    }
    static public bool checkString(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsLetter(str[i])) return false;
        }
        return true;
    }
    Dictionary<char, char> alph_cezar = new Dictionary<char, char>();
    string rus_al = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    string eng_al = "abcdefghijklmnopqrstuvwxyz";
    public Cezar_crypt(int shiftNum)
    {
        for (int i = 0; i < rus_al.Length; i++)
        {
            if (i + shiftNum < rus_al.Length)
                alph_cezar.Add(rus_al[i], rus_al[i + shiftNum]);
            else
                alph_cezar.Add(rus_al[i], ReturnShiftedChar(rus_al, i, shiftNum));
        }
        for (int i = 0; i < eng_al.Length; i++)
        {
            if (i + shiftNum < eng_al.Length)
                alph_cezar.Add(eng_al[i], eng_al[i + shiftNum]);
            else
                alph_cezar.Add(eng_al[i], ReturnShiftedChar(eng_al, i, shiftNum));
        }
        Console.WriteLine("Алфавит:");
        foreach (KeyValuePair<char,char> kvp in alph_cezar)
        {
            Console.WriteLine("{0} == {1}", kvp.Key, kvp.Value);
        }
    }
    public string Crypt(string str)
    {
        if (!checkString(str)) Console.WriteLine("Строка содежит не только буквы алфавита. Побочные символы остануться прежними.");
        string result = default(string);
        for (int i = 0; i < str.Length; i++)
        {
            if (alph_cezar.ContainsKey(str[i]))
            {
                if(char.IsUpper(str[i]))
                result += char.ToUpper(alph_cezar[str[i]]);
                else result += alph_cezar[str[i]];

            }
            else result += str[i];
        }
        return result;
    }

}
class Trisemus_crypt
{
    static string rus_alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
    static public bool checkString(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsLetter(str[i])||!rus_alphabet.Contains(str[i])) return false;
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
    static public char FindCryptedSymbol(char[,] matrix, char symbol)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == symbol)
                {
                    if (i == matrix.GetLength(0) - 1)
                    {
                        return matrix[0, j];
                    }
                    else return matrix[i + 1, j];
                }
            }
        }
        return '0';
    }
    static public string Crypt(string keyWord, string word)
    {
        Console.WriteLine();
        if (checkString(word)&&checkString(keyWord))
        {
            char[,] crypt_matrix = new char[4, 8];
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
            //    static string rus_alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";

            for (int i = 0; i < crypt_matrix.GetLength(0); i++)
            {
                for (int j = 0; j < crypt_matrix.GetLength(1); j++)
                {
                    Console.Write("{0}  ", crypt_matrix[i, j]);
                }
                Console.WriteLine();
            }
            string result=default(string);
            for (int i = 0; i < word.Length; i++)
                result+=FindCryptedSymbol(crypt_matrix, word[i]);
            return result;
        }
        else Console.WriteLine("Ключевое слово или слово для шифрования содержит не только буквы русского алфавита");
        
        return "";
    }
}
namespace ITK2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите задание:\nШифр цезаря - 1\nШифр Трисемуса - 2\n3 - выход");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine("На сколько вы хотите сдвигать алфавит?");
                            string buf = Console.ReadLine();
                            bool ok = Cezar_crypt.isOk(buf);
                            while (!ok)
                            {
                                Console.WriteLine("Введие корректное число сдвига.");
                                buf = Console.ReadLine();
                                ok = Cezar_crypt.isOk(buf);
                            }
                            Cezar_crypt cezar_object = new Cezar_crypt(Convert.ToInt32(buf));
                            Console.WriteLine("Введите строку для шифровки");
                            Console.WriteLine("{0}",cezar_object.Crypt(Console.ReadLine()));
                            Console.ReadKey();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("Введите слово-ключ");
                            string key_word = Console.ReadLine();
                            Console.WriteLine("Слово, которое хотите зашифровать");
                            Console.WriteLine("Зашифрованное слово - {0}", Trisemus_crypt.Crypt(key_word, Console.ReadLine()));
                           
                            Console.ReadKey();
                            break;
                            
                        }
                    default:
                        break;
                }
            }
            Cezar_crypt ibj = new Cezar_crypt(54);
        }
    }
}
