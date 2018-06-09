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
    /// Логика взаимодействия для IntoFile.xaml
    /// </summary>
    public partial class IntoFile : Window
    {
        public IntoFile()
        {
            InitializeComponent();
        }

        private void Into_Click(object sender, RoutedEventArgs e)
        {

            if (RealTask2.listZNAK.Count > 0)
            {
                if (TextBoxFile.Text.Contains(".txt") && TextBoxFile.Text.Length > 4)
                {
                    FileStream file = new FileStream(TextBoxFile.Text, FileMode.OpenOrCreate);
                    file.Close();
                    if (CheckBoxReWrite.IsChecked == false)
                    {
                        StreamWriter writer = new StreamWriter(TextBoxFile.Text, true);
                        foreach (RealTask2.ZNAK item in RealTask2.listZNAK)
                        {
                            writer.WriteLine("//");
                            writer.WriteLine("Фамилия: " + item.Surname);
                            writer.WriteLine("Имя: " + item.Name);
                            writer.WriteLine("Дата рождения: " + item.Date_Birth[0] + "." + item.Date_Birth[1] + "." + item.Date_Birth[2]);
                            writer.WriteLine("Знак Зодиака: " + item.Zodiak_Sign);

                        }
                        writer.Close();
                        RealTask2.isWriting = false;
                        Close();
                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter(TextBoxFile.Text, false);
                        foreach (RealTask2.ZNAK item in RealTask2.listZNAK)
                        {
                            writer.WriteLine("//");
                            writer.WriteLine("Фамилия: " + item.Surname);
                            writer.WriteLine("Имя: " + item.Name);
                            writer.WriteLine("Дата рождения: " + item.Date_Birth[0] + "." + item.Date_Birth[1] + "." + item.Date_Birth[2]);
                            writer.WriteLine("Знак Зодиака: " + item.Zodiak_Sign);
                        }
                        writer.Close();
                        RealTask2.isWriting = false;
                        Close();

                    }
                }
                else MessageBox.Show("Неправильно записано имя файла. Можно записывать только в .txt файл", "Ошибка!");
            } else
            {
                MessageBox.Show("В листе нет элементов", "Ошибка");

                this.Close();
            }
        }
    }
}
