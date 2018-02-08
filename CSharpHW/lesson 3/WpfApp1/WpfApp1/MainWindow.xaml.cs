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

namespace WpfApp1
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
        Random variable = new Random();
        int rand = 0;
        private void clik_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                var i = int.Parse(attempt.Text);
                if (i == 3)
                {
                    rand = variable.Next(1, 10);
                }
                i--;
                if (i == 0)
                {
                    i = 3;
                }
                attempt.Text = i.ToString();
                int enterValue = int.Parse(enter.Text);
                if (enterValue == rand)
                {
                    congratulation.Text = "Congatulation";
                    output.Text = rand.ToString();
                    i = 3;
                    attempt.Text = i.ToString();
                }



            }
            catch (Exception)
            {
                congratulation.Text = "Exception";
            }
        }
    }
}
