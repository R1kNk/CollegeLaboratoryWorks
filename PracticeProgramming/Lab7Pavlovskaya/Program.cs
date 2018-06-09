using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
class FileWork
{
    struct HelpString
    {
        public int start_index;
        public int lenght;
    }
    static public string FindBiggestWordAndItCount(FileStream file, out int count_in_file)
    {
        StreamReader file_out;
        file_out = new StreamReader(file, Encoding.GetEncoding(1251));
       string TextInString=default(string);
        while (!file_out.EndOfStream)
        TextInString += file_out.ReadLine();
        Console.WriteLine(TextInString);
        //
        HelpString detectBiggest = new HelpString();
        int counter = 0;
        int max = -9999;
        for (int i = 0; i < TextInString.Length; i++)
        {
            if (TextInString[i] != ' ' && TextInString[i] != ',' && TextInString[i] != '.' && TextInString[i] != '!' && TextInString[i] != ',' && TextInString[i] != '-')
                counter++;
            else if (counter != 0)
            {
                if (counter > max)
                {
                    max = counter;
                    detectBiggest.start_index = i - counter;
                }
                counter = 0;
            }
            if (i == TextInString.Length - 1)
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
        char[] BiggestWord=new char[detectBiggest.lenght];
        TextInString.CopyTo(detectBiggest.start_index, BiggestWord,0,detectBiggest.lenght);
        //
        count_in_file = 0;
        CountOfTheWordInString(TextInString, new string(BiggestWord), ref count_in_file);
        return new string(BiggestWord);
    }
    static int CountOfTheWordInString(string str, string word, ref int counter)
    {
        int buf_counter_for_string = 0;
        counter = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != ' ' && str[i] != ',' && str[i] != '.' && str[i] != '!' && str[i] != ',' && str[i] != '-')
            {
                if (str[i] == word[buf_counter_for_string]) buf_counter_for_string++;
                else if (buf_counter_for_string > 0) buf_counter_for_string = 0;
                if (buf_counter_for_string == word.Length)
                {
                    counter++;
                    buf_counter_for_string = 0;
                }
            }

        }
        return counter;
    }
    
}
namespace Lab7Pavlovskaya
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fout = new FileStream("test.txt", FileMode.OpenOrCreate);
            StreamReader file_out;
            int Count_in_file;
            Console.WriteLine("Самое большое слово в файле: {0}\n Оно встреччайлось в файле {1} раз(а)",FileWork.FindBiggestWordAndItCount(fout, out Count_in_file),Count_in_file);
        }
    }
}
