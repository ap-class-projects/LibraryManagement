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


//sql requirements
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
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

        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (emailUserNameBox.Text == "" || passwordBox.Password == "")
            {
                MessageBox.Show("please fill both fields!");          
            }
            else
            {
                bool loggedIn = false;

                string startupPath = Environment.CurrentDirectory;
                string path = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + startupPath + @"\database\db.mdf;Integrated Security=True;Connect Timeout=30";

                SqlConnection sqlConnection = new SqlConnection(path);
                sqlConnection.Open();
                string command = "select * from People";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if ((string)dataTable.Rows[i][Infos.indexUserName] == emailUserNameBox.Text && (string)dataTable.Rows[i][Infos.indexPassword] == passwordBox.Password)
                    {
                        loggedIn = true;
                    }
                }
                sqlConnection.Close();

                if (loggedIn)
                {
                    MessageBox.Show("logged in!");
                }
                else
                {
                    MessageBox.Show("wrong!");
                }
            }
        }
    }
}
