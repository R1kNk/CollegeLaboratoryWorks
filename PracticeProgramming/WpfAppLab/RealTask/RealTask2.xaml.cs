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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace WpfAppLab
{
    /// <summary>
    /// Логика взаимодействия для RealTask2.xaml
    /// </summary>
    public partial class RealTask2 : Window
    {
        static public bool IsThereOnlyLettersInStroke(string str)
        { if (str == null) return false;
            for (int i = 0; i < str.Length; i++)
                if (!char.IsLetter(str[i])) return false;
            return true;
        }
        static public bool IsStringNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
                if (!char.IsNumber(str[i]))
                {
                    return false;
                }
            return true;
        }
        static bool isCreating;
        static bool randomDateCheckbox;
        static public List<ZNAK> listZNAK;
        public struct ZNAK
        {
            public string Name;
            public string Surname;
            public int[] Date_Birth;
            public string Zodiak_Sign;
            public override string ToString()
            {
                return "\nФамилия: "+Surname+"\nИмя: "+Name+"\nДата рождения: "+Date_Birth[0]+"."+Date_Birth[1]+"."+Date_Birth[2]+"\nЗнак Зодиака: "+Zodiak_Sign;
            }

        }
        static public bool isSorting;
        static public bool isWriting;
        static public bool isReading;
        static public bool isSearching;
        static public bool isDeleting;
        //
        void showAddingContent()
        {
            SurnameLabel.Visibility = Visibility.Visible;
            TexBoxSurname.Visibility = Visibility.Visible;
            //
            NameLabel.Visibility = Visibility.Visible;
            TextBoxName.Visibility = Visibility.Visible;
            //
            YearLabel.Visibility = Visibility.Visible;
            YearTextBox.Visibility = Visibility.Visible;
            //
            MonthLabel.Visibility = Visibility.Visible;
            MonthTextBox.Visibility = Visibility.Visible;
            //
            DayLabel.Visibility = Visibility.Visible;
            DayTextBox.Visibility = Visibility.Visible;
            //
            RandomDateCheckBox.Visibility = Visibility.Visible;
            //
            ContinueButton.Visibility = Visibility.Visible;
        }
        //
        void hideAddingContent()
        {
            SurnameLabel.Visibility = Visibility.Hidden;
            TexBoxSurname.Visibility = Visibility.Hidden;
            //
            NameLabel.Visibility = Visibility.Hidden;
            TextBoxName.Visibility = Visibility.Hidden;

            YearLabel.Visibility = Visibility.Hidden;
            YearTextBox.Visibility = Visibility.Hidden;
            //
            MonthLabel.Visibility = Visibility.Hidden;
            MonthTextBox.Visibility = Visibility.Hidden;
            //
            DayLabel.Visibility = Visibility.Hidden;
            DayTextBox.Visibility = Visibility.Hidden;
            //
            RandomDateCheckBox.Visibility = Visibility.Hidden;
            //
            ContinueButton.Visibility = Visibility.Hidden;
        }
        
        public static void ConstructObject(string surname, string name, int[] date)
        {
            ZNAK obj = new ZNAK();
            obj.Date_Birth = new int[3];
            string buf = default(string);
            int yearNow = DateTimeOffset.Now.Year;
            int monthNow = DateTimeOffset.Now.Month;
            int dayNow = DateTimeOffset.Now.Day;
            bool[] IsChecked = new bool[5];

            if (IsThereOnlyLettersInStroke(surname))
            {
                IsChecked[0] = true;
            }
            else { return; }


            if (IsThereOnlyLettersInStroke(name))
            {
                IsChecked[1] = true;
            }
            else { return; }



                buf = date[2].ToString();
                if (buf[0] != '0' && IsStringNumber(buf))
                {
                    if ((Convert.ToInt32(buf) > yearNow))
                    { return; }
                    else
                    {
                        IsChecked[2] = true;
                    }
                }
                else { return; }

                buf = date[1].ToString();

                if (buf[0] != '0' && IsStringNumber(buf))
                {
                    if (Convert.ToInt32(buf) > 12 || buf.Length > 2 || (yearNow == date[2] && Convert.ToInt32(buf) > monthNow))
                    { return; }
                    else
                    {
                        IsChecked[3] = true;
                    }
                }
                else { return; }

            buf = date[0].ToString();
                if (buf[0] != '0' && IsStringNumber(buf))
                {
                    if (buf.Length > 2 || Convert.ToInt32(buf) > 31 || (yearNow == date[2] && date[1] == monthNow && Convert.ToInt32(buf) > dayNow))
                    { return; }
                    else
                    {
                        IsChecked[4] = true;
                    }
                }
                else { return; }
            string zodiac = default(string);
            if ((date[1] == 3 && date[0] >= 21) || date[1] == 4 && date[0] <= 20) zodiac = "Овен";
            else if ((date[1] == 4 && date[0] >= 21) || date[1] == 5 && date[0] <= 21) zodiac = "Телец";
            else if ((date[1] == 5 && date[0] >= 22) || date[1] == 6 && date[0] <= 21) zodiac = "Близнецы";
            else if ((date[1] == 6 && date[0] >= 22) || date[1] == 7 && date[0] <= 23) zodiac = "Рак";
            else if ((date[1] == 7 && date[0] >= 24) || date[1] == 8 && date[0] <= 23) zodiac = "Лев";
            else if ((date[1] == 8 && date[0] >= 24) || date[1] == 9 && date[0] <= 23) zodiac = "Дева";
            else if ((date[1] == 9 && date[0] >= 24) || date[1] == 10 && date[0] <= 23) zodiac = "Весы";
            else if ((date[1] == 10 && date[0] >= 24) || date[1] == 11 && date[0] <= 22) zodiac = "Скорпион";
            else if ((date[1] == 11 && date[0] >= 23) || date[1] == 12 && date[0] <= 21) zodiac = "Стрелец";
            else if ((date[1] == 12 && date[0] >= 22) || date[1] == 1 && date[0] <= 20) zodiac = "Козерог";
            else if ((date[1] == 1 && date[0] >= 21) || date[1] == 2 && date[0] <= 19) zodiac = "Водолей";
            else if ((date[1] == 2 && date[0] >= 20) || date[1] == 3 && date[0] <= 20) zodiac = "Рыбы";
            else zodiac = "unknown";
            if (IsChecked[0] == true && IsChecked[1] == true && IsChecked[2] == true && IsChecked[3] == true && IsChecked[4] == true)
            {
                obj.Surname = surname;
                obj.Name = name;
                obj.Date_Birth[0] = date[0];
                obj.Date_Birth[1] = date[1];
                obj.Date_Birth[2] = date[2];
                obj.Zodiak_Sign = zodiac;
                listZNAK.Add(obj);
            }
        }
        public RealTask2()
        {
            InitializeComponent();
            listZNAK = new List<ZNAK>();
            isCreating = false;
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreating)
            {
                isCreating = true;
                TexBoxSurname.Text = "";
                TextBoxName.Text = "";
                //
                YearTextBox.Text = "";
                MonthTextBox.Text = "";
                DayTextBox.Text = "";
                //
                RandomDateCheckBox.IsChecked = false;
                showAddingContent();
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            //isContinueClicked = true;
            ZNAK obj = new ZNAK();
            obj.Date_Birth = new int[3];
            string buf = default(string);
            bool is_checked = false;
            showAddingContent();
            int yearNow = DateTimeOffset.Now.Year;
            int monthNow = DateTimeOffset.Now.Month;
            int dayNow = DateTimeOffset.Now.Day;
            Random rand_date = new Random();
            bool[] IsChecked = new bool[5];
            string surname = default(string);
            string name = default(string);
            int year = default(int);
            int month = default(int);
            int day = default(int);
            string zodiac = default(string);
            
                        buf = TexBoxSurname.Text;
                        if (IsThereOnlyLettersInStroke(buf))
                        {
                            surname = buf;
                            IsChecked[0] = true;
                        }
                        else { MessageBox.Show("В фамилии должны быть только буквы. Попробуйте ещё раз."); TexBoxSurname.Text = ""; }


                        buf = TextBoxName.Text;
                        if (IsThereOnlyLettersInStroke(buf))
                        {
                            name = buf;
                            IsChecked[1] = true;
                        }
                        else { MessageBox.Show("В имени должны быть только буквы. Попробуйте ещё раз."); TextBoxName.Text = ""; }

                        if (randomDateCheckbox)
                        {
                            year = rand_date.Next(1, yearNow);
                            if (year == yearNow)
                                month = rand_date.Next(1, monthNow);
                            else month = rand_date.Next(1, 12);
                            if (year == yearNow && month == monthNow)
                                day = rand_date.Next(1, dayNow);
                            else day = rand_date.Next(1, 31);
                            IsChecked[2] = true;
                            IsChecked[3] = true;
                            IsChecked[4] = true;
                        }
                        else
                        {

                            buf = YearTextBox.Text;
                            if (buf[0] != '0' && IsStringNumber(buf))
                            {
                                if ((Convert.ToInt32(buf) > yearNow))
                                { MessageBox.Show("Год введен некорректно, или год больше текущего. Поробуйте ещё раз", "Ошибка"); YearTextBox.Text = ""; }
                                else
                                {
                                    year = Convert.ToInt32(buf);
                                    IsChecked[2] = true;
                                }
                            }
                            else { MessageBox.Show("Год введен некорректно. Попробуйте ещё раз.", "Ошибка!"); YearTextBox.Text = ""; }

                            buf = MonthTextBox.Text;

                            if (buf[0] != '0' && IsStringNumber(buf))
                            {
                                if (Convert.ToInt32(buf) > 12 || buf.Length > 2 || (yearNow == year && Convert.ToInt32(buf) > monthNow))
                                { MessageBox.Show("Месяц введен некорректно, или больше текущего. Поробуйте ещё раз", "Ошибка!"); MonthTextBox.Text = ""; }
                                else
                                {
                                    month = Convert.ToInt32(buf);
                                    IsChecked[3] = true;
                                }
                            }
                            else { MessageBox.Show("Месяц введен некорректно. Попробуйте ещё раз.", "Ошибка!"); MonthTextBox.Text = ""; }

                            buf = DayTextBox.Text;
                            if (buf[0] != '0' && IsStringNumber(buf))
                            {
                                if (buf.Length > 2 || Convert.ToInt32(buf) > 31 || (yearNow == year && month == monthNow && Convert.ToInt32(buf) > dayNow))
                                { MessageBox.Show("День введен некорректно, или больше текущего. Поробуйте ещё раз", "Ошибка!"); DayTextBox.Text = ""; }
                                else
                                {
                                    day = Convert.ToInt32(buf);
                                    IsChecked[4] = true;
                                }
                            }
                            else { MessageBox.Show("День введен некорректно. Попробуйте ещё раз.", "Ошибка!"); DayTextBox.Text = ""; }
                        }
                        if ((month == 3 && day >= 21) || month == 4 && day <= 20) zodiac = "Овен";
                        else if ((month == 4 && day >= 21) || month == 5 && day <= 21) zodiac = "Телец";
                        else if ((month == 5 && day >= 22) || month == 6 && day <= 21) zodiac = "Близнецы";
                        else if ((month == 6 && day >= 22) || month == 7 && day <= 23) zodiac = "Рак";
                        else if ((month == 7 && day >= 24) || month == 8 && day <= 23) zodiac = "Лев";
                        else if ((month == 8 && day >= 24) || month == 9 && day <= 23) zodiac = "Дева";
                        else if ((month == 9 && day >= 24) || month == 10 && day <= 23) zodiac = "Весы";
                        else if ((month == 10 && day >= 24) || month == 11 && day <= 22) zodiac = "Скорпион";
                        else if ((month == 11 && day >= 23) || month == 12 && day <= 21) zodiac = "Стрелец";
                        else if ((month == 12 && day >= 22) || month == 1 && day <= 20) zodiac = "Козерог";
                        else if ((month == 1 && day >= 21) || month == 2 && day <= 19) zodiac = "Водолей";
                        else if ((month == 2 && day >= 20) || month == 3 && day <= 20) zodiac = "Рыбы";
                        else zodiac = "unknown";
                        if (IsChecked[0] == true && IsChecked[1] == true && IsChecked[2] == true && IsChecked[3] == true && IsChecked[4] == true )
            {
                obj.Surname = surname;
                obj.Name = name;
                obj.Date_Birth[0] = day;
                obj.Date_Birth[1] = month;
                obj.Date_Birth[2] = year;
                obj.Zodiak_Sign = zodiac;
                listZNAK.Add(obj);
                hideAddingContent();
                isCreating = false;
            }

        }

        private void RandomData_Checked(object sender, RoutedEventArgs e)
        {
            randomDateCheckbox = true;
            if (isCreating)
            {
                YearLabel.Visibility = Visibility.Hidden;
                YearTextBox.Visibility = Visibility.Hidden;
                //
                MonthLabel.Visibility = Visibility.Hidden;
                MonthTextBox.Visibility = Visibility.Hidden;
                //
                DayLabel.Visibility = Visibility.Hidden;
                DayTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void RandomDateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            randomDateCheckbox = false;
           
                YearLabel.Visibility = Visibility.Visible;
                YearTextBox.Visibility = Visibility.Visible;
                //
                MonthLabel.Visibility = Visibility.Visible;
                MonthTextBox.Visibility = Visibility.Visible;
                //
                DayLabel.Visibility = Visibility.Visible;
                DayTextBox.Visibility = Visibility.Visible;
            
        }

        private void SortBy_Click(object sender, RoutedEventArgs e)
        {
            if (!isSorting)
            {
                isSorting = true;
                RealTask.SortBy srt = new RealTask.SortBy();
                srt.Show();
            }
        }

        private void IntoFile_Click(object sender, RoutedEventArgs e)
        {
            if (!isWriting)
            {
                RealTask.IntoFile into = new RealTask.IntoFile();
                into.Show();
            }
        }
        

        private void OutFile_Click(object sender, RoutedEventArgs e)
        {
            if (!isReading)
            {
                RealTask.OutFromFile outo = new RealTask.OutFromFile();
                outo.Show();
            }
        }

        private void SearchElement_Click(object sender, RoutedEventArgs e)
        {
            if (!isSearching)
            {
                RealTask.SearchElement search = new RealTask.SearchElement();
                search.Show();
            }
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            if (!isDeleting)
            {
                RealTask.DeleteElement del = new RealTask.DeleteElement();
                del.Show();
            }
        }
    }
}
