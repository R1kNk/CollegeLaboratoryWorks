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
    /// Логика взаимодействия для Task2_Paint.xaml
    /// </summary>
    public partial class Task2_Paint : Window
    {
        public Task2_Paint(double width, double heigth, string color)
        {
            InitializeComponent();
            
          
                Rectangle rect = new Rectangle();
                rect.Width = width;
                rect.Height = heigth;
                if(color=="red")
                rect.Fill = new SolidColorBrush(Colors.Red);
                else if(color=="green")
                rect.Fill = new SolidColorBrush(Colors.Green);
                else rect.Fill = new SolidColorBrush(Colors.Blue);

                //добавляем
                canvas.Children.Add(rect);
                //устанавливаем расположение
               Canvas.SetTop(rect, (600-heigth)/2);
                Canvas.SetLeft(rect, (600-width)/2);
            
        }
    }
}
