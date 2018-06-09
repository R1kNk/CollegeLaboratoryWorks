using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITK5
{
    class VernamCrypt
    {
        Dictionary<char, int> alph = new Dictionary<char, int>();
        Dictionary<int, char> alph_r = new Dictionary<int, char>();

        public VernamCrypt(IEnumerable<char> Alphabet)
        {
            int i = 0;
            foreach (char c in Alphabet)
            {
                alph.Add(c, i);
                alph_r.Add(i++, c);
            }
        }




        public string Crypt(string Text, string Key)
        {
            char[] key = Key.ToCharArray();
            char[] text = Text.ToCharArray();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                int ind;
                if (alph.TryGetValue(text[i], out ind))
                {
                    sb.Append(alph_r[(ind ^ alph[key[i % key.Length]]) % alph.Count]);
                }
            }

            return sb.ToString();
        }
        public static void StartCrypt()
        {
            string alph = "абвгдежзийклмнопрстуфхцчшщыьэюя"; // нет букв ё ъ щ
            bool check = false;
            string text = default(string);
            string key = default(string);
            while (!check)
            {
                Console.WriteLine("Слово, которое хотите зашифровать");
                text = Console.ReadLine();
                Console.WriteLine("\nВведите слово-ключ");
                key = Console.ReadLine();
                if (text.Length == key.Length) check = true;
                else Console.WriteLine("Длина слова должна совпадать с длиной ключа");
            }
            var pad = new VernamCrypt(alph);


            string encrypt = pad.Crypt(text, key);

            Console.WriteLine("Зашифрованный (или расшифрованный текст)\n" + encrypt);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                VernamCrypt.StartCrypt();
                Console.ReadKey();
            }
        }


        
    }

}

