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
    /// Логика взаимодействия для Task1_input.xaml
    /// </summary>
    public partial class Task1_input : Window
    {
        TextBox textBoxNumber1;
        TextBox textBoxNumber2;
        TextBox textBoxNumber3;
        bool checkSumm;
        bool checkMultiple;
        //
        public Task1_input()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Char.IsDigit(textBoxNumber1.Text[0]) && Char.IsDigit(textBoxNumber2.Text[0]) && Char.IsDigit(textBoxNumber3.Text[0]))
                Task1.updateData(Convert.ToDouble(textBoxNumber1.Text), Convert.ToDouble(textBoxNumber2.Text), Convert.ToDouble(textBoxNumber3.Text), checkSumm, checkMultiple);
            else MessageBox.Show("Значения введены неверно, попробуйте ещё раз!", "Ошибка!");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

           textBoxNumber1 = (TextBox)sender;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            textBoxNumber2 = (TextBox)sender;

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            textBoxNumber3 = (TextBox)sender;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkSumm = true;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            checkMultiple = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            checkSumm = false;

        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            checkMultiple = false;

        }
    }
}
