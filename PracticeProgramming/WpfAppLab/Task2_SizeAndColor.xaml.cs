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
    /// Логика взаимодействия для Task2_SizeAndColor.xaml
    /// </summary>
    public partial class Task2_SizeAndColor : Window
    {
        TextBox width;
        TextBox height;

        CheckBox redBox;
        CheckBox greenBox;
        CheckBox blueBox;

        bool red;
        bool green;
        bool blue;
        public Task2_SizeAndColor()
        {
            InitializeComponent();
        }

        static bool textBoxIsDigit(TextBox obj)
        {
            string buf = obj.Text;
            for (int i = 0; i < buf.Length; i++)
            {
                if ((buf[i] != ',' || buf[i] != '.') && Char.IsDigit(buf[i])) continue; else return false;
            }
            return true;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            width = (TextBox)sender;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            height = (TextBox)sender;
        }

        private void Red_Checked(object sender, RoutedEventArgs e)
        {
            if (green == true || blue == true)
            {
                redBox = (CheckBox)sender;
                redBox.IsChecked = false;
                MessageBox.Show("Вы не можете выбрать этот цвет, потому что вы выбрали другой", "Ошибка!");

            }
            else red = true;
        }

        private void Green_Checked(object sender, RoutedEventArgs e)
        {
            if (red == true || blue == true)
            {
                greenBox = (CheckBox)sender;
                greenBox.IsChecked = false;
                MessageBox.Show("Вы не можете выбрать этот цвет, потому что вы выбрали другой", "Ошибка!");

            }
            else green = true;
        }

        private void Blue_Checked(object sender, RoutedEventArgs e)
        {
            if (red == true || green == true)
            {
                blueBox = (CheckBox)sender;
                blueBox.IsChecked = false;
                MessageBox.Show("Вы не можете выбрать этот цвет, потому что вы выбрали другой", "Ошибка!");

            }
            else blue = true;
        }
        //
        private void Red_Unchecked(object sender, RoutedEventArgs e)
        {
            red = false;
        }

        private void Green_Unchecked(object sender, RoutedEventArgs e)
        {
            green = false;
        }

        private void Blue_Unchecked(object sender, RoutedEventArgs e)
        {
            blue = false;
        }
        private void Input_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxIsDigit(width) && textBoxIsDigit(height))
            {
                string color = default(string);
                if (red) color = "red";
                else if (green) color = "green";
                else color = "blue";
                Task2.InputData(Convert.ToDouble(width.Text.ToString()), Convert.ToDouble(height.Text.ToString()),color);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window ths = (Window)sender;
            ths.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
