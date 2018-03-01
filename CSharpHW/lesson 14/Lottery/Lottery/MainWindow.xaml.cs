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
                Info1.Text = "Sorry, but you entered a number that does not match the required parameters.";

            }
            //string mystring = @"my first string";

            //if (richTextBox1.Find(mystring) > 0)
            //{
            //    int my1stPosition = Info.Find(mystring);
            //    Info.SelectionBrush = Brushes.Blue;
            //    Info.Leng = mystring.Length;
            //    Info.SelectionColor = Color.DarkCyan;
            //}
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            Log(Info, "Serial com1 is open...\r", Brushes.Blue);
            Log(Info, "Serial com1 is open...\r", Brushes.Pink);
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
                    Info1.Foreground = Brushes.Green;
                    Info1.Text += str[i].ToString();
                }
                else
                {
                    Info1.Foreground = Brushes.Red;
                    Info1.Text += str[i].ToString();
                }
            }

            
        }
        public void Log(RichTextBox box1, string msg, object color)//Здесь у обжект будем передавать цвет Brushes.Color!!!
        {
            box1.Dispatcher.Invoke(new Action(() =>
            {
                TextRange range = new TextRange(box1.Document.ContentEnd, box1.Document.ContentEnd);
                range.Text = msg;
                range.ApplyPropertyValue(TextElement.ForegroundProperty, color);
                box1.ScrollToEnd();// функция Autoscroll
            }));

        }
    }
}
