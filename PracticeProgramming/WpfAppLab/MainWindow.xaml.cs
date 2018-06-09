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
using System.Diagnostics;

namespace WpfAppLab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Task1 task1Object;
        Task2 task2Object;
        RealTask2 realTask2Object;
        public MainWindow()
        {
            InitializeComponent();
            

        }
        
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
                task1Object = new Task1();
                task1Object.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
                task2Object = new Task2();
                task2Object.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
                Process.GetCurrentProcess().Kill();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            realTask2Object = new RealTask2();
            realTask2Object.Show();

        }
    }
}
