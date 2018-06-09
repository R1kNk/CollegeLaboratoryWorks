using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class FileReal
{
    FileStream OurFileStream;
    string fileName;
    public bool StreamIsOpen;
    int switchSize = default(int);
    public FileReal()
    {
    }
    void OpenStream()
    {
        OurFileStream = new FileStream(FileName, FileMode.OpenOrCreate);
        StreamIsOpen = true;
    }
    public string FileName
    {
        get
        {

            if (fileName != null) return fileName;
            else
            {
                Console.WriteLine("Имя файла ещё не задано!");
                return " ";
            }
        }
        set
        {
            try
            {
                if (value[value.Length - 1] == 't' && value[value.Length - 2] == 'x' && value[value.Length - 3] == 't' && value[value.Length - 4] == '.')
                {
                    fileName = value;
                    OpenStream();

                }
                else Console.WriteLine("Это не txt файл.");
            }
            catch (System.UnauthorizedAccessException)
            {
                FileInfo obj = new FileInfo(value);
                obj.IsReadOnly = false;
                if (value[value.Length - 1] == 't' && value[value.Length - 2] == 'x' && value[value.Length - 3] == 't' && value[value.Length - 4] == '.')
                {
                    fileName = value;
                    OpenStream();


                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("Не введено имя файла");
            }

        }

    }
    public void AddInfoToTheEndOfFIle()
    {
        if (StreamIsOpen && isRead == false)
        {
            OurFileStream.Close();
            StreamWriter writer = new StreamWriter(FileName, true);
            Console.WriteLine("Введите текст, котоый хотите добавить в конец файла:");
            writer.Write(Console.ReadLine());
            writer.Close();
            OpenStream();
        }
        else Console.WriteLine("Файл не создан");
    }
    public void AddInfoToTheClearedFile()
    {
        OurFileStream.Close();
        StreamWriter writer = new StreamWriter(FileName, false);
        Console.WriteLine("Введите текст, котоый хотите добавить в конец файла:");
        writer.Write(Console.ReadLine());
        writer.Close();
        OpenStream();
    }
    public string ShowFileInString()
    {
        OurFileStream.Close();
        StreamReader reader = new StreamReader(FileName,true);
        string res = reader.ReadToEnd();
        reader.Close();
        OpenStream();
        return res;
    }
    public List<string> ShowMatchesOfDates(string file)
    {
        Console.WriteLine("ent");
        List<string> matches = new List<string>();
        string pattern = @"(0[1-9]|[1-9]|[10-31])\.(0[1-9]|[10-12])\.(\d|\d\d|\d\d\d|\d)";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(file);
        string res;
        if (match.Success)
        {
            Console.WriteLine("к");
            while (match.Success)
            {
                res = match.ToString();
                for (int i = 0; i < res.Length; i++)
                    Console.Write(res[i]);
                Console.WriteLine();
                matches.Add(res);
                match = match.NextMatch();
            }
        }
        return matches;
    }
    public bool isRead
    {

        get
        {
            if (StreamIsOpen)
            {
                FileInfo obj = new FileInfo(FileName);
                return obj.IsReadOnly;
            }
            else { Console.WriteLine("Файл не создан!"); return false; }
        }
        set
        {
            if (StreamIsOpen)
            {
                FileInfo obj = new FileInfo(FileName);
                obj.IsReadOnly = value;
            }
            else { Console.WriteLine("Файл не создан!"); }
        }
    }

}
namespace RegExpText
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReal obj = new FileReal();
            bool exit = false;
            ConsoleKeyInfo key;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:\n1.Открыть или создать файл\n2.Добавить текст в конец файла\n3.Выход");
                key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            try
                            {
                                string filename = Console.ReadLine();
                                obj.FileName = filename;
                            }
                            catch (System.IO.IOException objectExc)
                            {
                                Console.WriteLine(objectExc.Message);
                            }
                            Console.ReadKey();
                            break;
                        }

                    case '2':
                        {
                            obj.AddInfoToTheEndOfFIle();
                            Console.ReadKey();
                            break;
                        }
                    case '3':
                        {
                            obj.AddInfoToTheClearedFile();
                            Console.ReadKey();
                            break;
                        }
                    case '4':
                        {
                            string file = obj.ShowFileInString();
                            Console.WriteLine(file);
                            obj.ShowMatchesOfDates(file);
                            Console.ReadKey();
                            break;
                        }
                    case '5':
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Такого пункта нет!");
                            break;
                        }
                }
            }

        }
    }
}
