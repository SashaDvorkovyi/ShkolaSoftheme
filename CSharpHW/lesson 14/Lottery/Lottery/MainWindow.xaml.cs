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
using System.Threading;

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
        string[] Chance = { "You have not guessed any value (((",
            "The chance to guess one value is 2 to 3.",
            "The chance to guess two times is 1 to 6",
            "The chance to guess three times is 1 to 36",
            "The chance to guess four times is 1 to 437",
            "The chance to guess five times is 1 to 9841",
            "The chance to guess six times is 1 to 531441" };
        LuckyNumber obj = new LuckyNumber();

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            obj.LukyNumberGenerate();
            var inputNumber = Input.Text;

            if (Validator(inputNumber))
            {
                OutPut.Document.Blocks.Clear();
                Info1.Clear();
                Print(inputNumber, obj, Chance);
            }
            else
            {
                OutPut.Document.Blocks.Clear();
                Info1.Text= "Sorry, but you entered a number that does not match the required parameters.";
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            OutPut.Document.Blocks.Clear();
            Input.Clear();
            Info1.Clear();
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

        public void Print(string str, LuckyNumber ob, string[] array)
        {
            var coincided = default(int);
            for (var i = 0; i < LuckyNumber.Lenght; i++)
            {
                if (string.Compare(ob[i].ToString(),str[i].ToString())==0)
                {
                    ColorText(ob[i].ToString(), OutPut, Brushes.Green);
                    coincided++;
                }
                else
                {
                    ColorText(ob[i].ToString(), OutPut, Brushes.Red);
                }
            }
            Info1.Text = array[coincided];;
        }

        public void ColorText(string msg, RichTextBox box1,  object color)
        {
            box1.Dispatcher.Invoke(new Action(() =>
            {
                TextRange range = new TextRange(box1.Document.ContentEnd, box1.Document.ContentEnd);
                range.Text = msg;
                range.ApplyPropertyValue(TextElement.ForegroundProperty, color);
            }));
        }

        private void Info_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
