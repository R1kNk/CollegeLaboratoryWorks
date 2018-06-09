using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

delegate int InfoSize();
class FileReal
{
    FileStream OurFileStream;
    string fileName;
    string date_creating;
    long filesize;
   public bool StreamIsOpen;
    int switchSize=default(int);
    public FileReal()
    {
        date_creating = "";
        filesize = 0;
    }
    void OpenStream()
    {
        OurFileStream = new FileStream(this.FileName, FileMode.OpenOrCreate);
        StreamIsOpen = true;
    }
    public void OutFileSize()
    {
        double size = FileSize;
        if (size > 0)
        {
            switch (switchSize)
            {
                case 0:
                    {
                        Console.WriteLine("Размер файла: {0} байт", size);
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Размер файла: {0:f5} кб", size);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Размер файла: {0:f5} мб", size);
                        break;
                    }
            }


        }
    }
    double FileSize
    {
        get
        {
            if (StreamIsOpen)
            {
                OurFileStream.Seek(0, SeekOrigin.End);
                filesize = OurFileStream.Position;
                double btSize = filesize;
                double size_kb;
                double size_mb;
                if (filesize > 1024)
                {
                    size_kb = btSize / 1024;
                    if (size_kb > 1024)
                    {
                        size_mb = size_kb / 1024;
                        switchSize = 2;
                        return size_mb;
                    }
                    else
                    {
                        switchSize = 1;
                        return size_kb;
                    }
                }
                else { switchSize = 0; OurFileStream.Seek(0, SeekOrigin.Begin); return filesize;}
            }
            else
            {
                Console.WriteLine("Файл не создан!");
                return 0;
            }
        }
    }
    public string Date_Creating
    {
        get
        {
            if (StreamIsOpen)
            {
                DateTime Data = new DateTime();
                if (FileName == null)
                {
                    string error = "Файл не задан или не создан";
                    return error;
                }
                else
                {
                    Data = File.GetCreationTime(FileName);
                    date_creating += Data.ToLongDateString();
                    date_creating += " " + Data.ToLongTimeString();
                    return date_creating;
                }
            }
            else
            {
                Console.WriteLine("Файл не создан!");
                return "";
            }
            
        }
    }
    public string FileName {
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
        if (StreamIsOpen&&isRead==false)
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
    public void ShowDirectory()
    {
        if (StreamIsOpen)
        {
            FileInfo obj = new FileInfo(fileName);
            Console.WriteLine("Файл {0} находится по пути:", FileName);
            Console.WriteLine(obj.FullName);
            
            
        }
        else Console.WriteLine("Файл не создан!");
        
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
            else { Console.WriteLine("Файл не создан!"); return false;}
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
namespace Lab4Pavlovskaya
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
                Console.WriteLine("Выберите действие:\n1.Открыть или создать файл\n2.Вывести имя файла\n3.Вывести размер файла\n4.Вывести дату создания файла\n5.Показать полный путь до файла\n6.Добавить текст в конец файла\n7.Открыт ли файл только для чтения?\n8.Изменить параметр (только для чтения)\n9.Выход");
                key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            string filename = Console.ReadLine();
                            obj.FileName = filename;
                            Console.ReadKey();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine(obj.FileName);
                            Console.ReadKey();

                            break;
                        }
                    case '3':
                        {
                            obj.OutFileSize();
                            Console.ReadKey();
                            break;
                        }
                    case '4':
                        {
                            Console.WriteLine(obj.Date_Creating);
                            Console.ReadKey();
                            break;
                        }
                    case '5':
                        {
                            obj.ShowDirectory();
                            Console.ReadKey();
                            break;
                        }
                    case '6':
                        {
                            obj.AddInfoToTheEndOfFIle();
                            Console.ReadKey();
                            break;
                        }
                    case '7':
                        {
                            if (obj.StreamIsOpen)
                            {
                                if (obj.isRead) Console.WriteLine("Файл открыт только для чтения");
                                else Console.WriteLine("Файл открыт для чтения и записи");
                            }
                            else Console.WriteLine("Файл не создан!");
                            Console.ReadKey();
                            break;
                        }
                    case '8':
                        {
                            bool isreadonly=false;
                            if (obj.StreamIsOpen)
                            {
                                try
                                {
                                    isreadonly = Convert.ToBoolean(Console.ReadLine());
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Введено неверное значение. Параметр будет равен false");
                                }
                                obj.isRead = isreadonly;
                            }
                            else Console.WriteLine("Файл не создан!");
                            Console.ReadKey();
                            break;
                        }
                    case '9':
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
