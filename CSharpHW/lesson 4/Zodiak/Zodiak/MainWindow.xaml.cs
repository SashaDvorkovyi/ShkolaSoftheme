using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
        ListMonthsImages list_Months_Images = new ListMonthsImages();

        private void batton_Click(object sender, RoutedEventArgs e)
        {
            var d = day.Text;
            var m = month.Text;
            var y = year.Text;
            var date = new DateTime();
            try
            {
                date = new DateTime(int.Parse(y), int.Parse(m), int.Parse(d));
                if (date > DateTime.Now)
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentOutOfRangeException){ }
            catch (FormatException) { }
            catch (ArgumentException) { }
            message = ReturnListOfPredictions(message);

            ZnakImageAdd(date);

            YearImageAdd(date);

            var luckyNumber = ReturnLuckyNumber(date);

            luckyTextBox.Text = luckyNumber.ToString();

            discr.Text = ReturnPrediction(message, date, luckyNumber);
        }


        public void ZnakImageAdd(DateTime date)
        { 
            foreach (var item in list_Months_Images.listMonthsImages)
            {
                if(date.Month > item.monthsAhdDays.Month)
                {
                    continue;
                }
                else if(date.Month == item.monthsAhdDays.Month)
                {
                    if (date.Day > item.monthsAhdDays.Day)
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            znakImage.Source = new BitmapImage(new Uri("Resources/" + item.imageName, UriKind.Relative));
                            break;
                        }
                        catch { }
                    }
                }
                else
                {
                    try
                    {
                        znakImage.Source = new BitmapImage(new Uri("Resources/" + item.imageName, UriKind.Relative));
                        break;
                    }
                    catch { }
                }
            }
        }

        public void YearImageAdd(DateTime date)
        {
            var a = ArreyYear.arreyYear[date.Year  % 12];
            try
            {
                yearImage.Source = new BitmapImage(new Uri("Resources/" + a, UriKind.Relative));
            }
            catch { }
        }

        public int ReturnLuckyNumber(DateTime date)
        {
            var lakyMonthAndDay = PressNumbers(PressNumbers(date.Month) + PressNumbers(date.Day));
            var lakyYear = PressNumbers(date.Year);
            var lakyNumber = PressNumbers(lakyMonthAndDay + lakyYear);
            return lakyNumber;
        }

        public int PressNumbers (int number)
        {
            if(number > 9)
            {
                var newNumber = default(int);
                var str = number.ToString();
                for (var i=0; i<str.Length; i++)
                {
                    newNumber +=int.Parse(str[i].ToString());
                }
                if (newNumber > 9)
                {
                    newNumber=PressNumbers(newNumber);
                }
                return newNumber;
            }
            return number;
        }

        public List<string> ReturnListOfPredictions(List<string> message)
        {
            if (message == null)
            {
                try
                {
                    StreamResourceInfo resInfo = Application.GetResourceStream(new Uri("Resources/Text.txt", UriKind.Relative));

                    using (StreamReader reader = new StreamReader(resInfo.Stream, System.Text.Encoding.Default))
                    {
                        var line = reader.ReadLine();

                        message = new List<string>();
                        while (line != null)
                        {
                            message.Add(line);
                            line = reader.ReadLine();
                        }
                    }
                }
                catch { }
                return message;
            }
            else
            {
                return message;
            }
        }

        public string ReturnPrediction(List<string> message, DateTime date, int luckyNumber)
        {
            var count = message.Count;
            var nubmer = date.Day + date.Month + date.Year;
            while (nubmer>count)
            {
                nubmer -= count;
            }
            nubmer *= luckyNumber;
            while (nubmer > count)
            {
                nubmer -= count;
            }
            return message[nubmer];
        }

        private void month_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
