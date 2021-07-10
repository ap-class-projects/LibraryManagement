using LibraryManagement.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary> 
    public partial class LoginPage : Page
    {
        public event PageChangerNoArg changeToSignUpPage;
        public event PageChanger changeToAdminPanelPage;
        public event PageChanger changeToEmployeePanelPage;
        public event PageChanger changeToUserPanelPage;

        public LoginPage(PageChangerNoArg changeToSignUpPage, 
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
                Person person = null;
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
                                person = new Admin(dataTable.Rows[i][PeopleTable.indexUserName].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexFirstName].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexLastName].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexEmail].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexPassword].ToString(),
                                                   (double)dataTable.Rows[i][PeopleTable.indexMoneyBag],
                                                   dataTable.Rows[i][PeopleTable.indexImageAddress].ToString());
                            }
                            else if(dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.Employee.ToString())
                            {
                                role = Role.Employee;
                                person = new Employee(dataTable.Rows[i][PeopleTable.indexUserName].ToString(),
                                                      dataTable.Rows[i][PeopleTable.indexFirstName].ToString(),
                                                      dataTable.Rows[i][PeopleTable.indexLastName].ToString(),
                                                      dataTable.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                                      dataTable.Rows[i][PeopleTable.indexEmail].ToString(),
                                                      dataTable.Rows[i][PeopleTable.indexPassword].ToString(),
                                                      (double)dataTable.Rows[i][PeopleTable.indexMoneyBag],
                                                      dataTable.Rows[i][PeopleTable.indexImageAddress].ToString());
                            }
                            else if(dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.User.ToString())
                            {
                                role = Role.User;

                                DataTable dataTable1 = UsersInfosTable.read();
                                DateTime? subRegisterDate = null;
                                DateTime? subRenewalDate = null;
                                DateTime? subExpireDate = null;

                                for(int j = 0; j < dataTable1.Rows.Count; j++)
                                {
                                    if(dataTable.Rows[i][PeopleTable.indexUserName].ToString() == dataTable1.Rows[j][UsersInfosTable.indexUserName].ToString())
                                    {
                                        subRegisterDate = (DateTime)dataTable1.Rows[j][UsersInfosTable.indexSubRegisterDate];
                                        subRenewalDate = (DateTime)dataTable1.Rows[j][UsersInfosTable.indexSubRenewalDate];
                                        subExpireDate = (DateTime)dataTable1.Rows[j][UsersInfosTable.indexSubExpireDate];
                                        break;
                                    }
                                }

                                person = new User( dataTable.Rows[i][PeopleTable.indexUserName].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexFirstName].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexLastName].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexEmail].ToString(),
                                                   dataTable.Rows[i][PeopleTable.indexPassword].ToString(),
                                                   (double)dataTable.Rows[i][PeopleTable.indexMoneyBag],
                                                   dataTable.Rows[i][PeopleTable.indexImageAddress].ToString(),
                                                   (DateTime)subRegisterDate,
                                                   (DateTime)subRenewalDate,
                                                   (DateTime)subExpireDate);
                            }
                        }
                    }

                    if(loggedIn)
                    {
                        break;
                    }
                }



                if (loggedIn)
                {
                    switch (role)
                    {
                        case Role.Admin:
                            changeToAdminPanelPage(person);
                            break;

                        case Role.Employee:
                            changeToEmployeePanelPage(person);
                            break;

                        case Role.User:
                            changeToUserPanelPage(person);
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
