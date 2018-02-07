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

namespace types
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
       public  void show<T>(T a) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
        {
            max.Text = default( T ).ToString();
            def.Text=T.MaxValue.ToString();
            min.Text = T.MinValue.ToString();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int a = default(int);
            a = int.MaxValue;
            a = int.MinValue;

        }
    }
}
