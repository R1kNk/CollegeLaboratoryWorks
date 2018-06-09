using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using MyDLL;

public struct FIOData : IComparable
{
    public string fio;
    public int startWorkingYear;
    public string workPosition;
    public double salary;
    public int experience;
    public FIOData(string FIO, string pos, int start, double sal)
    {
        fio = FIO;
        workPosition = pos;
        salary = sal;
        startWorkingYear = start;
        experience = DateTime.Now.Year - start;
    }
    public int CompareTo(object obj)
    {
        if (obj is FIOData)
        {
            FIOData buf = (FIOData)obj;
            if (experience > buf.experience) return 1;
            if (experience == buf.experience) return 0;
            else return -1;
        }
        return 0;


    }
    public override string ToString()
    {
        return String.Format("ФИО: {0}, Должность: {1}, Год начала работы: {2}, Зарплата: {3}, Стаж: {4} ",
            this.fio, this.workPosition, this.startWorkingYear, this.salary, this.experience);
    }
}
class WorkFIO
{
    public static void enterData(string file)
    {
        double maxSalary = default(double);
        Console.WriteLine("Введите максимальную величину зарплаты:");
        maxSalary = Convert.ToDouble(Console.ReadLine());
        StreamReader fs = new StreamReader(file);
        ArrayList dataArrList = new ArrayList();
        string[] separators = {" " };
        string buffStr = "0";
        while (buffStr != null)
        {
            buffStr = fs.ReadLine();
            if (buffStr != null)
            {
                string[] words = buffStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if(Convert.ToDouble(words[5])<maxSalary)
                    dataArrList.Add(new FIOData(words[0] +" "+ words[1] + " " + words[2], words[3], Convert.ToInt32(words[4]), Convert.ToDouble(words[5])));
               
            }
        }
        dataArrList.Sort();
        outInfo("outputPeople.txt", dataArrList, maxSalary);
    }
    static void outInfo(string outPath, ArrayList arrInfo, double salary)
    {
        using (StreamWriter sw = new StreamWriter(outPath, false, System.Text.Encoding.Default))
        {
            sw.WriteLine("Работники, имеющие зарплату меньше {0} отсортированнные по стажу:", salary);
            foreach (object obj in arrInfo)
            {
                FIOData buf = (FIOData)obj;
                sw.WriteLine(buf.ToString());
            }
        }
    }
}
class readWordsFromFile
    {
        public static void readWords(string file)
        {
        StreamReader fs = new StreamReader(file);
        Queue<string> littleLetterWords = new Queue<string>();
            Queue<string> bigLetterWords = new Queue<string>();
            string[] buffLineArray;
            string[] separators = { ",", ".", "!", "?", ";", ":", " " };
        string buffStr = "0";
        while (buffStr != null)
        {
            buffStr = fs.ReadLine();
            if (buffStr != null)
            {
                string[] words = buffStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (Char.IsLetter(word[0]))
                    {
                        if (Char.IsLower(word[0])) littleLetterWords.Enqueue(word);
                        else if (Char.IsUpper(word[0])) bigLetterWords.Enqueue(word);

                    }
                }
            }
        }
        
        showQueues(littleLetterWords, bigLetterWords);
           
        }
         static void showQueues(Queue<string> little, Queue<string> bigger)
        {
        Console.WriteLine(little.Count);
        Console.WriteLine("Слова начинающиеся с маленькой буквы:");
        while (little.Count != 0)
            Console.WriteLine(little.Dequeue());
        foreach(string str in little) Console.WriteLine(str);
        Console.WriteLine("Слова начинающиеся с большой буквы:");
        while (bigger.Count != 0)
            Console.WriteLine(bigger.Dequeue());
    }
     }

static class HashMusic
{
    public class Catalog
    {
        public class Disk
        {
            static string toSongBuf(string song)
            {
                string ret = "/";
                return ret += song;
            }
            static public string[] separator = {"/"};
             string diskName;
            Hashtable diskInfo;
            public Disk(string disk)
            {
                diskName = disk;
                diskInfo = new Hashtable();
            }
            public void addSong(string authorName,string songName)
            {
                string songNameBuf = toSongBuf(songName);
                if (diskInfo.ContainsKey(authorName))
                {
                    string songsBuf = (string)diskInfo[authorName];
                    if (songsBuf.Contains(songName)) Console.WriteLine("Данный диск уже содержит песню {0} этого автора", songName);
                    else
                    {
                        diskInfo[authorName] += songNameBuf;
                    }
                }
                else diskInfo.Add(authorName, songNameBuf);
            }
            public void deleteSong(string authorName, string songName)
            {
                if (diskInfo.ContainsKey(authorName))
                {
                    string songsBuf = (string)diskInfo[authorName];
                    string[] words = songsBuf.Split(Disk.separator, StringSplitOptions.RemoveEmptyEntries);

                    if (words.Contains<string>(songName))
                    {
                        diskInfo.Remove((string)authorName);
                        if (words[0] != songName)
                        {
                            diskInfo.Add(authorName, toSongBuf(songName));
                            if (words.Length > 1)
                                for (int i = 1; i < words.Length; i++)
                                    if (words[i] != songName) diskInfo[authorName] += toSongBuf(words[i]);
                        }
                        else
                        {
                            if (words.Length > 1)
                            {
                                diskInfo.Add(authorName, toSongBuf(words[1]));
                                if (words.Length > 2)
                                    for (int i = 2; i < words.Length; i++)
                                        diskInfo[authorName] += toSongBuf(words[i]);
                            }
                        }
                    }
                    else Console.WriteLine("На этом диске нет этой песни данного автора");

                }
                else Console.WriteLine("Данный диск не содержит песен автора {0}", authorName);
            }
            public void viewDiskInfo()
            {
                ICollection keys = diskInfo.Keys;
                if (keys.Count != 0)
                {
                    Console.WriteLine("На данном диске содержаться следующие композиции авторов:");
                    foreach (string author in keys)
                    {
                        Console.Write("{0}: ",author);
                        string songs = (string)diskInfo[author];
                        string[] words = songs.Split(Disk.separator, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var song in words)
                            Console.Write("{0}// ",song);
                        Console.Write("\n");

                    }
                }
                else Console.WriteLine("Данный диск пуст!");
            }
            public void searchAuthorsSongs(string author)
            {
                if (diskInfo.ContainsKey(author))
                {
                    Console.WriteLine("На диске {0} есть данные песни(я) автора:", diskName);
                    string songs = (string)diskInfo[author];
                    string[] words = songs.Split(Disk.separator, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var song in words)
                        Console.Write("{0}  ", song);
                    Console.Write("\n");
                }
            }
        }
        public Hashtable hashtableCatalogInfo;
        public Catalog()
        {
            hashtableCatalogInfo = new Hashtable();
        }
        public void addDisk(string diskName)
        {
            if (hashtableCatalogInfo.ContainsKey(diskName)) Console.WriteLine("Данный диск уже есть в каталоге!");
            else hashtableCatalogInfo.Add(diskName, new Disk(diskName));
        }
        public void deleteDisk(string diskName)
        {
            if (!hashtableCatalogInfo.ContainsKey(diskName)) Console.WriteLine("Этого диска нет в каталоге!");
            else hashtableCatalogInfo.Remove(diskName);
        }
        public Disk returnDisk(string diskname)
        {
            if (hashtableCatalogInfo.ContainsKey(diskname))
            {
                Disk buf = (Disk)hashtableCatalogInfo[diskname];
                return buf;
            }
            return new Disk("INVALID");
        }
        public void viewCatalogInfo()
        {
            if (hashtableCatalogInfo.Count > 0)
            {
                Console.WriteLine("В данном каталоге находятся следующие диски:");
                ICollection disks = hashtableCatalogInfo.Keys;
                foreach (var disk in disks)
                {
                    Console.WriteLine("Диск {0}:", disk);
                    Disk disk_buf = (Disk)hashtableCatalogInfo[disk];
                    disk_buf.viewDiskInfo();
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("В данном каталоге ещё нет ни одного диска");

        }
        public void searchAuthor(string author)
        {
           
            if (hashtableCatalogInfo.Count > 0)
            {
                Console.WriteLine("Найденные соответствия:");
                ICollection disks = hashtableCatalogInfo.Keys;
                foreach (var disk in disks)
                {
                    
                    Disk disk_buf = (Disk)hashtableCatalogInfo[disk];
                    disk_buf.searchAuthorsSongs(author);
                }

            }
            else Console.WriteLine("В данном каталоге ещё нет ни одного диска");
        }
        
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(HelloDLL.HelloToString());
        Console.ReadKey();
       // readWordsFromFile.readWords("kek.txt");
       WorkFIO.enterData( "people.txt");
        HashMusic.Catalog ctl = new HashMusic.Catalog();
        ctl.addDisk("Country Music");
        HashMusic.Catalog.Disk buf = ctl.returnDisk("Country Music");
        ctl.returnDisk("Country Music").addSong("Kay Adams", "Wheels and Tears");
        ctl.returnDisk("Country Music").addSong("Kay Adams", "Make Mine Country");
        ctl.returnDisk("Country Music").addSong("Kay Adams", "Alcohol and Tears");
        ctl.returnDisk("Country Music").addSong("Ernest Ashworth", "You Can't Pick a Rose in December");
        ctl.returnDisk("Country Music").addSong("Ernest Ashworth", "Forever Gone");
        ctl.returnDisk("Country Music").addSong("Ernest Ashworth", "I Take the Chance");

        ctl.addDisk("Jazz Music");
        ctl.returnDisk("Jazz Music").addSong("John D.Boswell", "Ode to the Brain");
        ctl.returnDisk("Jazz Music").addSong("John D.Boswell", "The Quantum World");
        ctl.returnDisk("Jazz Music").addSong("Mark Isham", "Everybody Wins");
        bool exit = false;
        ConsoleKeyInfo key;
        while (!exit)
        {
            Console.Clear();
            ctl.viewCatalogInfo();
            Console.WriteLine("Выберите действие:\n1.Создать новый диск\n2.Удалить диск\n3.Найти все песни автора в катлоге\n4.Информация о каталоге\n5.Выбрать диск для дальнейших действий");
            key = Console.ReadKey();
            Console.WriteLine();
            switch (key.KeyChar)
            {
                case '1':
                    {
                        Console.WriteLine("Введите имя диска, который вы хотите добавить");
                        ctl.addDisk(Console.ReadLine());
                        Console.ReadKey();
                        break;
                    }
                case '2':
                    {
                        Console.WriteLine("Введите имя диска, который вы хотите удалить");
                        ctl.deleteDisk(Console.ReadLine());
                        Console.ReadKey();
                        break;
                    }
                case '3':
                    {
                        Console.WriteLine("Введите имя автора, песни которого вы хотите найти");
                        ctl.searchAuthor(Console.ReadLine());
                        Console.ReadKey();
                        break;
                    }

                case '4':
                    {
                        ctl.viewCatalogInfo();
                        Console.ReadKey();
                        break;
                    }
                case '5':
                    {
                        ICollection disks = ctl.hashtableCatalogInfo.Keys;
                        if (disks.Count > 0)
                        {
                            Console.WriteLine("Выберите диск:");
                            int dskBuf = 1;
                            foreach (var disk in disks)
                            {
                                Console.WriteLine("{0}.{1}", dskBuf, disk);
                                dskBuf++;
                            }
                            dskBuf = 1;
                            int choose = Convert.ToInt32(Console.ReadLine());
                            foreach (var disk in disks)
                            {
                                if (dskBuf != choose)
                                    dskBuf++;
                                else
                                {
                                    ConsoleKeyInfo key_choise;
                                    bool ex_disk = false;
                                    while (!ex_disk)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Диск {0}:", disk);
                                        ctl.returnDisk((string)disk).viewDiskInfo();
                                        Console.WriteLine("Выберите действие с диском:\n0-Выход\n1-Добавить песню\n2-Удалить песню\n3-Информация о диске\n4-Найти песни автора на диске");
                                        key_choise = Console.ReadKey();
                                        Console.WriteLine();
                                        switch (key_choise.KeyChar)
                                        {
                                            case '0': { ex_disk = true; break; }
                                            case '1': {
                                                    Console.WriteLine("Введите автора песни:");
                                                    string authorBuf = Console.ReadLine();
                                                    Console.WriteLine("Введите название песни:");
                                                    string songBuf = Console.ReadLine();
                                                    ctl.returnDisk((string)disk).addSong(authorBuf, songBuf);
                                                    Console.ReadKey();
                                                    break;
                                                }
                                            case '2':
                                                {
                                                    Console.WriteLine("Введите автора песни:");
                                                    string authorBuf = Console.ReadLine();
                                                    Console.WriteLine("Введите название песни:");
                                                    string songBuf = Console.ReadLine();
                                                    ctl.returnDisk((string)disk).deleteSong(authorBuf, songBuf);
                                                    Console.ReadKey();
                                                    break;
                                                }
                                            case '3':
                                                {

                                                    ctl.returnDisk((string)disk).viewDiskInfo();
                                                    Console.ReadKey();
                                                    break;
                                                }
                                            case '4':
                                                {
                                                    Console.WriteLine("Введите автора:");
                                                    string authorBuf = Console.ReadLine();
                                                    ctl.returnDisk((string)disk).searchAuthorsSongs(authorBuf);
                                                    Console.ReadKey();
                                                    break;
                                                }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        else Console.WriteLine("В каталоге ещё нет дисков");
                        Console.ReadKey();
                        break;
                    }
            }
        }
    }
}



