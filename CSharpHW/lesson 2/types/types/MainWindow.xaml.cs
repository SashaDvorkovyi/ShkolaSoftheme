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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (_byte.IsChecked.HasValue && _byte.IsChecked.Value)
            {
                show(byte.MaxValue, default(byte), byte.MinValue);
            }
            else if (_sbyte.IsChecked.HasValue && _sbyte.IsChecked.Value)
            {
                show(sbyte.MaxValue, default(sbyte), sbyte.MinValue);
            }
            else if (_short.IsChecked.HasValue && _short.IsChecked.Value)
            {
                show(short.MaxValue, default(short), short.MinValue);
            }
            else if (_ushort.IsChecked.HasValue && _ushort.IsChecked.Value)
            {
                show(ushort.MaxValue, default(ushort), ushort.MinValue);
            }
            else if (_int.IsChecked.HasValue && _int.IsChecked.Value)
            {
                show(int.MaxValue, default(int), int.MinValue);
            }
            else if (_uint.IsChecked.HasValue && _uint.IsChecked.Value)
            {
                show(uint.MaxValue, default(uint), uint.MinValue);
            }
            else if (_long.IsChecked.HasValue && _long.IsChecked.Value)
            {
                show(long.MaxValue, default(long), long.MinValue);
            }
            else if (_ulong.IsChecked.HasValue && _ulong.IsChecked.Value)
            {
                show(ulong.MaxValue, default(ulong), ulong.MinValue);
            }
            else if (_float.IsChecked.HasValue && _float.IsChecked.Value)
            {
                show(float.MaxValue, default(float), float.MinValue);
            }
            else if (_double.IsChecked.HasValue && _double.IsChecked.Value)
            {
                show(double.MaxValue, default(double), double.MinValue);
            }
            else if (_decimal.IsChecked.HasValue && _decimal.IsChecked.Value)
            {
                show(decimal.MaxValue, default(decimal), decimal.MinValue);
            }
            else
            {
                max.Text = "Specify the value";
                def.Text = "Specify the value";
                min.Text = "Specify the value";
            }

        }
        public void show<T>(T a, T b, T c)
        {
            max.Text = a.ToString();
            def.Text = b.ToString();
            min.Text = c.ToString();
        }
    }
}
