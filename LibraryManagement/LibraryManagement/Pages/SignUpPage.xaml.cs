using System.Windows;
using System.Windows.Controls;
using System.Data;
using LibraryManagement.Classes;

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public PageChanger changeToLoginPage;
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void signInButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            changeToLoginPage();
        }

        private bool fieldIsEmpty()
        {
            if(userNameBox.Text == "" ||
               firstNameBox.Text == "" ||
               lastNameBox.Text == "" ||
               emailBox.Text == "" ||
               phoneNumberBox.Text == "" ||
               passwordBox.Password == "" ||
               confirmPasswordBox.Password == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool userNameExists()
        {
            DataTable dataTable = PeopleTable.read();
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                if(userNameBox.Text == dataTable.Rows[i][PeopleTable.indexUserName].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void signUpButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(fieldIsEmpty())
            {
                MessageBox.Show("Please fill all of the given fields!");
            }
            else
            {
                if(userNameExists())
                {
                    MessageBox.Show("This username already exists!");
                }
                else
                {
                    //check regex
                    //sign up
                }
            }
        }
    }
}
