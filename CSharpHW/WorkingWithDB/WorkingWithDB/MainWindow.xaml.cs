using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkingWithDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Form_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Add("Please wait. The database is loading.");

           string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Git\CSharpHW\WorkingWithDB\WorkingWithDB\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            UpdateListBox1();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if(sqlConnection!=null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
            Application.Current.Shutdown();
        }

        private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text)&&string.IsNullOrEmpty(TextBox2.Text)&&
                string.IsNullOrWhiteSpace(TextBox1.Text) && !double.TryParse (TextBox2.Text, out double result))
            {
                Lable7.Content = "Field \"Name\" cannot be empty  and field \"Price\" mast be number!";
            }
            else
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Products] (Name, Price)VALUES(@Name, @Price)", sqlConnection);
                command.Parameters.AddWithValue("Name", TextBox1.Text);
                command.Parameters.AddWithValue("Price", TextBox2.Text);

                await command.ExecuteNonQueryAsync();

                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                Lable7.Content = string.Empty;

                UpdateListBox1();
            }
        }

        private async void UpdateListBox1()
        {
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Products]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                ListBox1.Items.Clear();

                while (await sqlReader.ReadAsync())
                {
                    ListBox1.Items.Add(sqlReader["Id"].ToString() + "|   " + sqlReader["Name"].ToString() + "     " + Convert.ToDouble(sqlReader["Price"]).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox3.Text) && string.IsNullOrEmpty(TextBox4.Text) &&
                string.IsNullOrEmpty(TextBox5.Text) && !int.TryParse(TextBox3.Text, out int res) &&
                string.IsNullOrWhiteSpace(TextBox4.Text) && !double.TryParse(TextBox5.Text, out double result))
            {
                Lable8.Content = "Field \"Name\" cannot be empty  and fields \"Price\", \"ID\" mast be number!";
            }
            else
            {
                SqlCommand command = new SqlCommand("UPDATE [Products] SET [Name]=@Name, [Price]=@Price WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Name", TextBox4.Text);
                command.Parameters.AddWithValue("Price", TextBox5.Text);
                command.Parameters.AddWithValue("Id", TextBox3.Text);

                await command.ExecuteNonQueryAsync();

                TextBox3.Text = string.Empty;
                TextBox4.Text = string.Empty;
                TextBox5.Text = string.Empty;
                Lable8.Content = string.Empty;

                UpdateListBox1();
            }
        }

        private async void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox6.Text)  && !int.TryParse(TextBox6.Text, out int res))
            {
                Lable9.Content = "Field \"ID\" cannot be empty and mast be number!";
            }
            else
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Products] WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", TextBox6.Text);

                await command.ExecuteNonQueryAsync();

                TextBox6.Text = string.Empty;
                Lable9.Content = string.Empty;

                UpdateListBox1();
            }
        }
    }
}
