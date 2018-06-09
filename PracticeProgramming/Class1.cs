using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
namespace ITK17
{
    public struct FIOData : IComparable
    {
        public string fio;
        public int startWorkingYear;
        public string workPosition;
        public double salary;
        public int experience;
        public FIOData(string FIO, string pos, int start, double sal)
        {
            // System.Collections.Generic.
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
    public class WorkFIO
    {
        public static void enterData(string file)
        {
            double maxSalary = default(double);
            Console.WriteLine("Введите максимальную величину зарплаты:");
            maxSalary = Convert.ToDouble(Console.ReadLine());
            StreamReader fs = new StreamReader(file);
            ArrayList dataArrList = new ArrayList();
            string[] separators = { " " };
            string buffStr = "0";
            while (buffStr != null)
            {
                buffStr = fs.ReadLine();
                if (buffStr != null)
                {
                    string[] words = buffStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (Convert.ToDouble(words[5]) < maxSalary)
                        dataArrList.Add(new FIOData(words[0] + " " + words[1] + " " + words[2], words[3], Convert.ToInt32(words[4]), Convert.ToDouble(words[5])));

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
    public class readWordsFromFile
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
            foreach (string str in little) Console.WriteLine(str);
            Console.WriteLine("Слова начинающиеся с большой буквы:");
            while (bigger.Count != 0)
                Console.WriteLine(bigger.Dequeue());
        }
    }
    public static class HashMusic
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
                static public string[] separator = { "/" };
                string diskName;
                Hashtable diskInfo;
                public Disk(string disk)
                {
                    diskName = disk;
                    diskInfo = new Hashtable();
                }
                public void addSong(string authorName, string songName)
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
                            Console.Write("{0}: ", author);
                            string songs = (string)diskInfo[author];
                            string[] words = songs.Split(Disk.separator, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var song in words)
                                Console.Write("{0}// ", song);
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
}