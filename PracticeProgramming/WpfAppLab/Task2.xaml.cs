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

namespace WpfAppLab
{
    /// <summary>
    /// Логика взаимодействия для Task2.xaml
    /// </summary>
    public partial class Task2 : Window
    {
       static double width;
       static double height;
       static  string colorStatement;
        public Button pnt;
        public delegate void MethodContainer();
        static public void InputData(double wdth, double hgt, string clr)
        {
            width = wdth;
            height = hgt;
            colorStatement = clr;
            
        }
         public void setUsablePaintButton()
        {
            this.paint.IsEnabled = true;
        }
        public Task2()
        {
            InitializeComponent();
            colorStatement = "null";
            pnt = this.paint;
            
        }

        private void Paint_Click(object sender, RoutedEventArgs e)
        {
            if (width > 600 || height > 600)
            {
                MessageBox.Show("Размеры прямоугольника превышают размер окна!", "Ошибка!");

            }
            else
            {
                Task2_Paint paint = new Task2_Paint(width, height, colorStatement);
                paint.Show();
            }
        }

        private void SizeAndColor_Click(object sender, RoutedEventArgs e)
        {
            Task2_SizeAndColor obj =  new Task2_SizeAndColor();
            obj.Owner = this;
            obj.Show();
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (colorStatement != "null") setUsablePaintButton();
        }
    }
}
