using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace WpfAppLab.RealTask
{
    /// <summary>
    /// Логика взаимодействия для OutFromFile.xaml
    /// </summary>
    public partial class OutFromFile : Window
    {
        bool ReWriteChecked;
        bool CleanFileChecked;
        public OutFromFile()
        {
            InitializeComponent();
        }

        private void OutFromFile_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxOut.Text.Contains(".txt") && TextBoxOut.Text.Length > 4)
            {
                try
                {
                    FileStream file = new FileStream(TextBoxOut.Text, FileMode.Open);
                    file.Close();
                } catch(IOException ) { MessageBox.Show("Такого файла не существует!","Ошибка!");return; }
                string surname = default(string);
                string name = default(string);
                int[] date = new int[3];
                if (ReWriteChecked == true) RealTask2.listZNAK = new List<RealTask2.ZNAK>();
                if (CleanFileChecked == true)
                {
                    StreamReader reader = new StreamReader(TextBoxOut.Text, true);
                    string buf = default(string);
                    while (!reader.EndOfStream)
                    {
                         surname = default(string);
                         name = default(string);
                         date = new int[3];
                        buf = reader.ReadLine();
                       
                            if (buf.Contains("Фамилия:") && buf != "//")
                            {
                                for (int i = 9; i < buf.Length; i++)
                                    surname += buf[i];
                                //
                                buf = reader.ReadLine();
                                if (buf.Contains("Имя:") && buf != "//")
                                {
                                    for (int i = 5; i < buf.Length; i++)
                                        name += buf[i];
                                }
                                //
                                buf = reader.ReadLine();
                                if (buf.Contains("Дата рождения:") && buf != "//")
                                {
                                    string buf_date = default(string);
                                    for (int i = 15; i < buf.Length; i++)
                                        buf_date += buf[i];
                                    string[] dates = buf_date.Split('.');
                                    if (dates.Length == 3)
                                    {
                                        date[0] =Convert.ToInt32(dates[0]);
                                        date[1] = Convert.ToInt32(dates[1]);
                                        date[2] = Convert.ToInt32(dates[2]);
                                    }
                                    RealTask2.ConstructObject(surname, name, date);

                                }

                            }


                        
                    }
                    RealTask2.isReading = false;
                    reader.Close();
                    File.Delete(TextBoxOut.Text);
                    File.Create(TextBoxOut.Text);
                    Close();
                }
                else
                {
                    StreamReader reader = new StreamReader(TextBoxOut.Text, false);
                    string buf = default(string);
                    while (!reader.EndOfStream)
                    {
                        surname = default(string);
                        name = default(string);
                        date = new int[3];
                        buf = reader.ReadLine();

                        if (buf.Contains("Фамилия:") && buf != "//")
                        {
                            for (int i = 9; i < buf.Length; i++)
                                surname += buf[i];
                            //
                            buf = reader.ReadLine();
                            if (buf.Contains("Имя:") && buf != "//")
                            {
                                for (int i = 5; i < buf.Length; i++)
                                    name += buf[i];
                            }
                            //
                            buf = reader.ReadLine();
                            if (buf.Contains("Дата рождения:") && buf != "//")
                            {
                                string buf_date = default(string);
                                for (int i = 15; i < buf.Length; i++)
                                    buf_date += buf[i];
                                string[] dates = buf_date.Split('.');
                                if (dates.Length == 3)
                                {
                                    date[0] = Convert.ToInt32(dates[0]);
                                    date[1] = Convert.ToInt32(dates[1]);
                                    date[2] = Convert.ToInt32(dates[2]);
                                }
                                RealTask2.ConstructObject(surname, name, date);

                            }

                        }
                    }
                    RealTask2.isReading = false;
                    reader.Close();
                    Close();

                }
            }
            else MessageBox.Show("Неправильно записано имя файла. Можно записывать только в .txt файл", "Ошибка!");
        }

        private void CleanFile_Checked(object sender, RoutedEventArgs e)
        {
            CleanFileChecked = true;
        }

        private void CleanFile_Unchecked(object sender, RoutedEventArgs e)
        {
            CleanFileChecked = false;
        }

        private void ReWrite_Checked(object sender, RoutedEventArgs e)
        {
            ReWriteChecked = true;
        }

        private void ReWrite_Unchecked(object sender, RoutedEventArgs e)
        {
            ReWriteChecked = false;
        }
    }
}
