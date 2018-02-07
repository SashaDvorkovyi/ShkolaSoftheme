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
using System.Text.RegularExpressions;

namespace data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.birthD.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
            //this.birthD.KeyDown += new KeyEventHandler(TextBox_OnPreviewKeyDown);
            this.birthM.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
            this.birthY.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
            this.phone.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
            this.firstN.PreviewTextInput += new TextCompositionEventHandler(TextBox_OnPreviewTextInput);
            this.lastN.PreviewTextInput += new TextCompositionEventHandler(TextBox_OnPreviewTextInput);
            gender.Items.Add("male");
            gender.Items.Add("female");
            firstN.MaxLength = 255;
            lastN.MaxLength = 255;
            birthD.MaxLength = 2;
            birthM.MaxLength = 2;
            birthY.MaxLength = 4;
            email.MaxLength = 255;
            phone.MaxLength = 12;
            info.MaxLength = 2000;


        }
        //private void TextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Space)
        //        e.Handled =false;
        //}
        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
        void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if ((inp < 'A' || inp > 'Z')&& (inp < 'a' || inp > 'z') && (inp < 'А' || inp > 'Я') && (inp < 'а' || inp > 'я'))
                e.Handled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string emailStr = null;
            for(int i=0; i< email.Text.Length; i++)
            {
                if (email.Text[i] == '@')
                {
                    emailStr = email.Text;
                    break;
                }
            }
            if ((firstN.Text!="")&& (lastN.Text != "") && (birthD.Text != "") && (birthM.Text != "") && (birthY.Text != "") && (int.Parse(birthD.Text)>0) && (int.Parse(birthD.Text) < 32) && (int.Parse(birthM.Text) > 0) && (int.Parse(birthM.Text) <13) && (int.Parse(birthY.Text) > 1900) && (int.Parse(birthY.Text) < DateTime.Now.Year) && (phone.Text != "") && (emailStr != null) && (gender.Text != null))
            {
                List<string> user = new List<string>();
                user.Add(firstN.Text);
                user.Add(lastN.Text);
                user.Add(birthD.Text);
                user.Add(birthM.Text);
                user.Add(birthY.Text);
                user.Add(gender.Text);
                user.Add(emailStr);
                user.Add(phone.Text);
                user.Add(info.Text);

                success.Text = "Success";
            }
            else
            {
                success.Text = "You filled out the form incorrectly";
            }





        }


    }
}
