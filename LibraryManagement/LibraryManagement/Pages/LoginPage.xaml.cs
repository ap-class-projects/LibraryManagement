using LibraryManagement.Classes;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
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
                DataTable dataTable = DB.read();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if ((string)dataTable.Rows[i][Infos.indexUserName] == emailUserNameBox.Text && (string)dataTable.Rows[i][Infos.indexPassword] == passwordBox.Password)
                    {
                        loggedIn = true;
                    }
                }

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
