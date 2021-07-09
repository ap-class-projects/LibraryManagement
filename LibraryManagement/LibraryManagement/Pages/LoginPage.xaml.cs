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
        public event PageChanger changeToSignUpPage;
        public event PageChanger changeToAdminPanelPage;
        public event PageChanger changeToEmployeePanelPage;
        public event PageChanger changeToUserPanelPage;

        public LoginPage(PageChanger changeToSignUpPage, 
                         PageChanger changeToAdminPanelPage,
                         PageChanger changeToEmployeePanelPage,
                         PageChanger changeToUserPanelPage)
        {
            InitializeComponent();
            this.changeToSignUpPage = changeToSignUpPage;
            this.changeToAdminPanelPage = changeToAdminPanelPage;
            this.changeToEmployeePanelPage = changeToEmployeePanelPage;
            this.changeToUserPanelPage = changeToUserPanelPage;
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            if (emailUserNameBox.Text == "" || passwordBox.Password == "")
            {
                MessageBox.Show("please fill both fields!");
            }
            else
            {
                bool loggedIn = false;
                Role role = Role.Unknown;
                DataTable dataTable = PeopleTable.read();
                //check data
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //Login with username or email
                    if (
                         (string)dataTable.Rows[i][PeopleTable.indexUserName] == emailUserNameBox.Text || 
                         (string)dataTable.Rows[i][PeopleTable.indexEmail] == emailUserNameBox.Text
                       )
                    {
                        if ((string)dataTable.Rows[i][PeopleTable.indexPassword] == passwordBox.Password)
                        {
                            loggedIn = true;
                            if (dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.Admin.ToString())
                            {
                                role = Role.Admin;
                            }
                            else if(dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.Employee.ToString())
                            {
                                role = Role.Employee;
                            }
                            else if(dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.User.ToString())
                            {
                                role = Role.User;
                            }
                        }
                    }
                }

                if (loggedIn)
                {
                    switch (role)
                    {
                        case Role.Admin:
                            changeToAdminPanelPage();
                            break;

                        case Role.Employee:
                            changeToEmployeePanelPage();
                            break;

                        case Role.User:
                            changeToUserPanelPage();
                            break;

                        default:
                            MessageBox.Show(
                                "sth went wrong!\n" +
                                "try again.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("One of the two fields are wrong!");
                }
            }
        }

        private void createAccount_Click(object sender, RoutedEventArgs e)
        {
            changeToSignUpPage();
        }
    }
}
