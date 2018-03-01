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

namespace Lottery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        double[] Chance = { 1.5, 5.5, 36.46, 437.4, 9841.5, 531441};
        LuckyNumber obj = new LuckyNumber();

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var inputNumber = Input.Text;
            if (Validator(inputNumber))
            {
                CompareAndPrint(inputNumber, obj, Chance);
                OutPut.Text = obj.LukyNumberIs;
            }
            else
            {
                Info.Text = "Sorry, but you entered a number that does not match the required parameters.";
                RichTextBox.TextChangedEvent.
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {

        }
        public static bool Validator(string str)
        {
            var ansver = true;
            string numberInString = string.Empty;
            int value;
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length != LuckyNumber.Lenght)
                {
                    ansver = false;
                }
                else
                {
                    foreach (var item in str)
                    {
                        if (!int.TryParse(item.ToString(), out value) || value ==0)
                        {
                            ansver = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                ansver = false;
            }
            return ansver;
        }

        public void CompareAndPrint(string str, LuckyNumber ob, double[] array)
        {
            for (var i = 0; i < LuckyNumber.Lenght; i++)
            {
                if (int.Parse(ob[i].ToString()) == int.Parse(str[i].ToString()))
                {
                    Info.Foreground = Brushes.Green;
                    Info.Text += str[i].ToString();
                }
                else
                {
                    Info.Foreground = Brushes.Red;
                    Info.Text += str[i].ToString();
                }
            }

            
        }
    }
}
