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
    /// Логика взаимодействия для DeleteElement.xaml
    /// </summary>
    public partial class DeleteElement : Window
    {
        public DeleteElement()
        {
            InitializeComponent();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int yearNow = DateTimeOffset.Now.Year;
            int monthNow = DateTimeOffset.Now.Month;
            int dayNow = DateTimeOffset.Now.Day;
            if (BoxVariants.Text == null) MessageBox.Show("Выберите критерий для сортировки!", "Ошибка!");
            else
            {
                switch (BoxVariants.Text)
                {
                    case "По фамилии":
                        {
                            if (RealTask2.listZNAK.Count > 0)
                            {
                                string surname = SurnameTextBox.Text;
                                if (RealTask2.IsThereOnlyLettersInStroke(surname))
                                {
                                    int finded = 0;
                                    List<RealTask2.ZNAK> newList = new List<RealTask2.ZNAK>();
                                    for (int i = 0; i < RealTask2.listZNAK.Count; i++)
                                    {
                                        if (RealTask2.listZNAK[i].Surname != surname) { newList.Add(RealTask2.listZNAK[i]);  }
                                    }
                                    finded = RealTask2.listZNAK.Count - newList.Count;
                                    RealTask2.listZNAK = newList;
                                    if (finded > 0)
                                    {

                                        MessageBox.Show("Найдено и удалено" + finded + " соответствия.", "Успех");

                                    }
                                    else MessageBox.Show("Не найдено ни одного соответствия!", "Ошибка!");
                                }
                                else { MessageBox.Show("В фамилии должны быть только буквы. Попробуйте ещё раз."); SurnameTextBox.Text = ""; }
                            }
                            else MessageBox.Show("В листе нет элементов!", "Ошибка!");
                            break;
                        }
                    case "По имени":
                        {
                            if (RealTask2.listZNAK.Count > 0)
                            {
                                string name = NameTextBox.Text;
                                if (RealTask2.IsThereOnlyLettersInStroke(name))
                                {
                                    int finded = 0;
                                    List<RealTask2.ZNAK> newList = new List<RealTask2.ZNAK>();
                                    for (int i = 0; i < RealTask2.listZNAK.Count; i++)
                                    {
                                        if (RealTask2.listZNAK[i].Name != name) { newList.Add(RealTask2.listZNAK[i]); }
                                    }
                                    finded = RealTask2.listZNAK.Count - newList.Count;

                                    RealTask2.listZNAK = newList;
                                    if (finded > 0)
                                    {
                                        MessageBox.Show("Найдено и удалено " + finded + " соответствия.", "Успех");
                                    }
                                    else MessageBox.Show("Не найдено ни одного соответствия!", "Ошибка!");
                                }
                                else { MessageBox.Show("В имени должны быть только буквы. Попробуйте ещё раз."); SurnameTextBox.Text = ""; }
                            }
                            else MessageBox.Show("В листе нет элементов!", "Ошибка!");
                            break;
                        }
                    case "По дню рождения":
                        {
                            if (RealTask2.listZNAK.Count > 0)
                            {
                                string day = DayTextBox.Text;
                                //
                                if (day[0] != '0' && RealTask2.IsStringNumber(day))
                                {
                                    if (day.Length > 2 || Convert.ToInt32(day) > 31)
                                    { MessageBox.Show("День введен некорректно, или больше текущего. Поробуйте ещё раз", "Ошибка!"); DayTextBox.Text = ""; }
                                    else
                                    {
                                        int dayInt = Convert.ToInt32(day);
                                        int finded = 0;
                                        List<RealTask2.ZNAK> newList = new List<RealTask2.ZNAK>();
                                        for (int i = 0; i < RealTask2.listZNAK.Count; i++)
                                        {
                                            if (RealTask2.listZNAK[i].Date_Birth[0] != dayInt) { newList.Add(RealTask2.listZNAK[i]); }
                                        }
                                        finded = RealTask2.listZNAK.Count - newList.Count;

                                        RealTask2.listZNAK = newList;
                                        if (finded > 0)
                                        {
                                            
                                            MessageBox.Show("Найдено и удалено " + finded + " соответствия.", "Успех");

                                        }
                                        else MessageBox.Show("Не найдено ни одного соответствия!", "Ошибка!");
                                    }
                                }
                                else { MessageBox.Show("День введен некорректно. Попробуйте ещё раз.", "Ошибка!"); DayTextBox.Text = ""; }
                                //
                            }
                            else MessageBox.Show("В листе нет элементов!", "Ошибка!");
                            break;
                        }
                    case "По месяцу рождения":
                        {
                            if (RealTask2.listZNAK.Count > 0)
                            {
                                string month = MonthTextBox.Text;
                                //
                                if (month[0] != '0' && RealTask2.IsStringNumber(month))
                                {
                                    if (Convert.ToInt32(month) > 12 || month.Length > 2)
                                    { MessageBox.Show("Месяц введен некорректно. Поробуйте ещё раз", "Ошибка!"); MonthTextBox.Text = ""; }
                                    else
                                    {
                                        int monthInt = Convert.ToInt32(month);
                                        int finded = 0;
                                        List<RealTask2.ZNAK> newList = new List<RealTask2.ZNAK>();
                                        for (int i = 0; i < RealTask2.listZNAK.Count; i++)
                                        {
                                            if (RealTask2.listZNAK[i].Date_Birth[1] != monthInt) { newList.Add(RealTask2.listZNAK[i]); }
                                        }
                                        finded = RealTask2.listZNAK.Count - newList.Count;

                                        RealTask2.listZNAK = newList;
                                        
                                        if (finded > 0)
                                        {

                                            MessageBox.Show("Найдено и удалено " + finded + " соответствия.", "Успех");
                                        }
                                        else MessageBox.Show("Не найдено ни одного соответствия!", "Ошибка!");
                                    }
                                }
                                else { MessageBox.Show("Месяц введен некорректно. Попробуйте ещё раз.", "Ошибка!"); MonthTextBox.Text = ""; }
                                //
                            }
                            else MessageBox.Show("В листе нет элементов!", "Ошибка!");
                            break;
                        }
                    case "По году рождения":
                        {
                            if (RealTask2.listZNAK.Count > 0)
                            {
                                string year = YearTextBox.Text;
                                //
                                if (year[0] != '0' && RealTask2.IsStringNumber(year))
                                {
                                    if ((Convert.ToInt32(year) > yearNow))
                                    { MessageBox.Show("Год введен некорректно, или год больше текущего. Попробуйте ещё раз", "Ошибка"); YearTextBox.Text = ""; }
                                    else
                                    {
                                        int yearInt = Convert.ToInt32(year);
                                        int finded = 0;
                                        List<RealTask2.ZNAK> newList = new List<RealTask2.ZNAK>();
                                        for (int i = 0; i < RealTask2.listZNAK.Count; i++)
                                        {
                                            if (RealTask2.listZNAK[i].Date_Birth[2] != yearInt) { newList.Add(RealTask2.listZNAK[i]); }
                                        }
                                        finded = RealTask2.listZNAK.Count - newList.Count;

                                        RealTask2.listZNAK = newList;
                                        if (finded > 0)
                                        {

                                            MessageBox.Show("Найдено и удалено " + finded + " соответствия.", "Успех");
                                        }
                                        else MessageBox.Show("Не найдено ни одного соответствия!", "Ошибка!");
                                    }
                                }
                                else { MessageBox.Show("Год введен некорректно. Попробуйте ещё раз.", "Ошибка!"); MonthTextBox.Text = ""; }
                                //
                            }
                            else MessageBox.Show("В листе нет элементов!", "Ошибка!");
                            break;
                        }
                    case "По целой дате":
                        {
                            if (RealTask2.listZNAK.Count > 0)
                            {
                                bool[] check = new bool[3];
                                //day
                                string day = DayTextBox.Text;
                                //
                                if (day[0] != '0' && RealTask2.IsStringNumber(day))
                                {
                                    if (day.Length > 2 || Convert.ToInt32(day) > 31)
                                    { MessageBox.Show("День введен некорректно, или больше текущего. Поробуйте ещё раз", "Ошибка!"); DayTextBox.Text = ""; }
                                    else
                                    {
                                        check[0] = true;
                                    }
                                }
                                else { MessageBox.Show("День введен некорректно. Попробуйте ещё раз.", "Ошибка!"); DayTextBox.Text = ""; }
                                //
                                //month
                                string month = MonthTextBox.Text;
                                //
                                if (month[0] != '0' && RealTask2.IsStringNumber(month))
                                {
                                    if (Convert.ToInt32(month) > 12 || month.Length > 2)
                                    { MessageBox.Show("Месяц введен некорректно. Поробуйте ещё раз", "Ошибка!"); MonthTextBox.Text = ""; }
                                    else
                                    {
                                        check[1] = true;
                                    }
                                }
                                else { MessageBox.Show("Месяц введен некорректно. Попробуйте ещё раз.", "Ошибка!"); MonthTextBox.Text = ""; }
                                //
                                //year
                                string year = YearTextBox.Text;
                                //
                                if (year[0] != '0' && RealTask2.IsStringNumber(year))
                                {
                                    if ((Convert.ToInt32(year) > yearNow))
                                    { MessageBox.Show("Год введен некорректно, или год больше текущего. Попробуйте ещё раз", "Ошибка"); YearTextBox.Text = ""; }
                                    else
                                    {
                                        check[2] = true;
                                    }
                                }
                                else { MessageBox.Show("Год введен некорректно. Попробуйте ещё раз.", "Ошибка!"); MonthTextBox.Text = ""; }
                                //
                                if (check[0] == true && check[1] == true && check[2] == true)
                                {
                                    int dayInt = Convert.ToInt32(day);
                                    int monthInt = Convert.ToInt32(month);
                                    int yearInt = Convert.ToInt32(year);
                                    int finded = 0;
                                    List<RealTask2.ZNAK> newList = new List<RealTask2.ZNAK>();
                                    for (int i = 0; i < RealTask2.listZNAK.Count; i++)
                                    {
                                        if (RealTask2.listZNAK[i].Date_Birth[0] == dayInt && RealTask2.listZNAK[i].Date_Birth[1] == monthInt && RealTask2.listZNAK[i].Date_Birth[2] == yearInt) { }  else { newList.Add(RealTask2.listZNAK[i]); }
                                    }
                                    finded = RealTask2.listZNAK.Count - newList.Count;
                 
                                    RealTask2.listZNAK = newList;
                                    
                                    if (finded > 0)
                                    {
                                        MessageBox.Show("Найдено и удалено" + finded + " соответствия.", "Успех");
                                    }
                                    else MessageBox.Show("Не найдено ни одного соответствия!", "Ошибка!");
                                }
                            }
                            else MessageBox.Show("В листе нет элементов!", "Ошибка!");
                            break;
                        }

                }
            }
        }

        private void MouseMoveWindow(object sender, MouseEventArgs e)
        {
            if (BoxVariants.Text != null)
            {
                switch (BoxVariants.Text)
                {
                    case "По фамилии":
                        {
                            SurnameLabel.Visibility = Visibility.Visible;
                            SurnameTextBox.Visibility = Visibility.Visible;
                            //
                            NameLabel.Visibility = Visibility.Hidden;
                            NameTextBox.Visibility = Visibility.Hidden;
                            //
                            DayLabel.Visibility = Visibility.Hidden;
                            DayTextBox.Visibility = Visibility.Hidden;
                            //
                            MonthLabel.Visibility = Visibility.Hidden;
                            MonthTextBox.Visibility = Visibility.Hidden;
                            //
                            YearLabel.Visibility = Visibility.Hidden;
                            YearTextBox.Visibility = Visibility.Hidden;
                            //
                            break;
                        }
                    case "По имени":
                        {
                            SurnameLabel.Visibility = Visibility.Hidden;
                            SurnameTextBox.Visibility = Visibility.Hidden;
                            //
                            NameLabel.Visibility = Visibility.Visible;
                            NameTextBox.Visibility = Visibility.Visible;
                            //
                            DayLabel.Visibility = Visibility.Hidden;
                            DayTextBox.Visibility = Visibility.Hidden;
                            //
                            MonthLabel.Visibility = Visibility.Hidden;
                            MonthTextBox.Visibility = Visibility.Hidden;
                            //
                            YearLabel.Visibility = Visibility.Hidden;
                            YearTextBox.Visibility = Visibility.Hidden;
                            break;
                        }
                    case "По дню рождения":
                        {
                            SurnameLabel.Visibility = Visibility.Hidden;
                            SurnameTextBox.Visibility = Visibility.Hidden;
                            //
                            NameLabel.Visibility = Visibility.Hidden;
                            NameTextBox.Visibility = Visibility.Hidden;
                            //
                            DayLabel.Visibility = Visibility.Visible;
                            DayTextBox.Visibility = Visibility.Visible;
                            //
                            MonthLabel.Visibility = Visibility.Hidden;
                            MonthTextBox.Visibility = Visibility.Hidden;
                            //
                            YearLabel.Visibility = Visibility.Hidden;
                            YearTextBox.Visibility = Visibility.Hidden;
                            break;
                        }
                    case "По месяцу рождения":
                        {
                            SurnameLabel.Visibility = Visibility.Hidden;
                            SurnameTextBox.Visibility = Visibility.Hidden;
                            //
                            NameLabel.Visibility = Visibility.Hidden;
                            NameTextBox.Visibility = Visibility.Hidden;
                            //
                            DayLabel.Visibility = Visibility.Hidden;
                            DayTextBox.Visibility = Visibility.Hidden;
                            //
                            MonthLabel.Visibility = Visibility.Visible;
                            MonthTextBox.Visibility = Visibility.Visible;
                            //
                            YearLabel.Visibility = Visibility.Hidden;
                            YearTextBox.Visibility = Visibility.Hidden;
                            break;
                        }
                    case "По году рождения":
                        {
                            SurnameLabel.Visibility = Visibility.Hidden;
                            SurnameTextBox.Visibility = Visibility.Hidden;
                            //
                            NameLabel.Visibility = Visibility.Hidden;
                            NameTextBox.Visibility = Visibility.Hidden;
                            //
                            DayLabel.Visibility = Visibility.Hidden;
                            DayTextBox.Visibility = Visibility.Hidden;
                            //
                            MonthLabel.Visibility = Visibility.Hidden;
                            MonthTextBox.Visibility = Visibility.Hidden;
                            //
                            YearLabel.Visibility = Visibility.Visible;
                            YearTextBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case "По целой дате":
                        {
                            SurnameLabel.Visibility = Visibility.Hidden;
                            SurnameTextBox.Visibility = Visibility.Hidden;
                            //
                            NameLabel.Visibility = Visibility.Hidden;
                            NameTextBox.Visibility = Visibility.Hidden;
                            //
                            DayLabel.Visibility = Visibility.Visible;
                            DayTextBox.Visibility = Visibility.Visible;
                            //
                            MonthLabel.Visibility = Visibility.Visible;
                            MonthTextBox.Visibility = Visibility.Visible;
                            //
                            YearLabel.Visibility = Visibility.Visible;
                            YearTextBox.Visibility = Visibility.Visible;
                            break;
                        }

                }
            }
        }

       
        private void Delete_Closed(object sender, EventArgs e)
        {
            RealTask2.isDeleting = false;

        }

       
    }
}
