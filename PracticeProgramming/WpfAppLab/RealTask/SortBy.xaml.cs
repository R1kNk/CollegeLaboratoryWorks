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

namespace WpfAppLab.RealTask
{
    /// <summary>
    /// Логика взаимодействия для SortBy.xaml
    /// </summary>
    public partial class SortBy : Window
    {
        public SortBy()
        {
            InitializeComponent();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            if (RealTask2.listZNAK.Count > 0)
            {
                switch (SwitchComboBox.Text)
                {
                    case "Surname":
                        {
                            var worker_sort = RealTask2.listZNAK.OrderBy(n => n.Surname).ToList();
                            RealTask2.listZNAK = worker_sort;
                            break;
                        }
                    //
                    case "Name":
                        {
                            var worker_sort = RealTask2.listZNAK.OrderBy(n => n.Name).ToList();
                            RealTask2.listZNAK = worker_sort;
                            break;
                        }
                    //
                    case "Date":
                        {
                            var worker_sort = RealTask2.listZNAK.OrderBy(n => n.Date_Birth[2]).ThenBy(n => n.Date_Birth[1]).ThenBy(n => n.Date_Birth[0]).ToList();
                            RealTask2.listZNAK = worker_sort;
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Вы не выбрали тип сортировки!", "Ошибка!");
                            break;
                        }
                }
            } else
            {
                MessageBox.Show("Лист не содержит элемнтов!", "Ошибка!");
                this.Close();
            }
            
        }

        private void ClosedSort(object sender, EventArgs e)
        {
            RealTask2.isSorting = false;
        }


    }
}
