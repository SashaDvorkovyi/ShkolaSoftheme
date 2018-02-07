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
using System.Collections;
using System.Resources;
using System.Windows.Resources;
using System.IO;

namespace Zodiak
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

        List<string> message;
        private void batton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var d = day.Text;
                var m = month.Text;
                var y = year.Text;

                int daysInt = 0;
                int monthsInt = 0;
                int yearInt = 0;
                if (int.TryParse(d, out int result) && result < 32 && result > 0)
                {
                    daysInt = result;
                }
                if (int.TryParse(m, out int result1) && result1 < 13 && result1 > 0)
                {
                    monthsInt = result1;
                }
                if (int.TryParse(y, out int result2) && result2 > 0)
                {
                    yearInt = result2;
                }
                if (daysInt == 0 || monthsInt == 0 || yearInt == 0)
                {
                    discr.Text = "Sorry, but you were wrong";
                }
                string[] arreyZodiak = { "Resources/01_kozerog.jpg", "Resources/02_vodoley.jpg", "Resources/03_fish.jpg", "Resources/04_oven.jpg", "Resources/05_telec.jpg", "Resources/06_bleznec.jpg", "Resources/07_rak.jpg", "Resources/08_lev.jpg", "Resources/09_deva.jpg", "Resources/10_vesu.jpg", "Resources/11_skorpion.jpg", "Resources/12_strelec.jpg" };
                string[] arreyYear = { "Resources/god_09_obeciana.jpg", "Resources/god_10_petux.jpg", "Resources/god_11_sobaka.jpg", "Resources/god_12_svinja.jpg", "Resources/god_01_krysa.jpg", "Resources/god_02_buk.jpg", "Resources/god_03_tigr.jpg", "Resources/god_04_krolik.jpg", "Resources/god_05_drakon.jpg", "Resources/god_06_zmeja.jpg", "Resources/god_07_loshad.jpg", "Resources/god_08_ovca.jpg" };
                int[] arreyDataSimvols = { 19, 49, 79, 110, 140, 171, 203, 234, 266, 296, 325, 355 };
                for (int i = 0; i < 12; i++)
                {
                    if (numberOfDays(monthsInt, daysInt) > arreyDataSimvols[i])
                    {
                        continue;
                    }
                    else
                    {
                        znakImageAdd(arreyZodiak[i]);
                        break;
                    }
                }
                if (numberOfDays(monthsInt, daysInt) > arreyDataSimvols[11])
                {
                    znakImageAdd(arreyZodiak[0]);
                }

                yearImage.Source = new BitmapImage(new Uri(arreyYear[yearInt % 12], UriKind.Relative));

                string luckyNumberString = d + m + y;
                int luckydayEndMonth = daysInt + monthsInt;
                int luckyYear = 0;
                for (int i = 0; i < y.Length; i++)
                {
                    luckyYear += int.Parse(("0" + y[i]));
                }
                int lucky = luckydayEndMonth + luckyYear;
                int luckyNamber = 0;
                int luckyNamberLong = 0;
                for (int i = 0; i < lucky.ToString().Length; i++)
                {
                    luckyNamberLong += int.Parse(("0" + lucky.ToString()[i]));
                }
                if (luckyNamberLong >= 10)
                {
                    for (int i = 0; i < luckyNamber.ToString().Length; i++)
                    {
                        luckyNamber += int.Parse(("0" + luckyNamberLong.ToString()[i]));
                    }
                }
                else
                {
                    luckyNamber = luckyNamberLong;
                }

                luckyTextBox.Text = luckyNamber.ToString();
                StreamResourceInfo resInfo = Application.GetResourceStream(new Uri("Resources/text.txt", UriKind.Relative));
                StreamReader reader = new StreamReader(resInfo.Stream, System.Text.Encoding.Default);
                if (message == null)
                {
                    message=new List<string>();
                }
                var line = reader.ReadLine();
                //if (message[0] == null)
                //{
                    while (line != null)
                    {

                        message.Add(line);
                        line = reader.ReadLine();
                    }
                    reader.Close();
                //}

                int messageLong = message.Count;

                string dataTimeNow = String.Format("{0}", DateTime.Now);
                int numberData = int.Parse("" + dataTimeNow[0] + dataTimeNow[1]) + int.Parse("" + dataTimeNow[3] + dataTimeNow[4]);
                int numberOfLine = 0;
                int number = (numberData + luckydayEndMonth + luckyYear) * luckyNamber;
                while (messageLong < number)
                {
                    number -= messageLong;
                }
                numberOfLine = messageLong - number;
                discr.Text = message[numberOfLine];

            }
            catch(Exception)
            {
                MessageBox.Show("Input Error");
                
            }
        }
        private static int numberOfDays(int monthArg, int dayArg)
        {
            int[] dayInMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int result=0;
            int monthArgAdd = 0;
            for (int i=0; i<monthArg; i++)
            {
                monthArgAdd += dayInMonth[i];
            }
            result = monthArgAdd + dayArg;
            return result;
        }
        private void znakImageAdd(string s)
        {
            znakImage.Source = new BitmapImage(new Uri(s, UriKind.Relative));
        }

        private void month_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
