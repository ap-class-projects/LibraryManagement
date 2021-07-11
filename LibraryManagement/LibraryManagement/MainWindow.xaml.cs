using LibraryManagement.Classes;
using LibraryManagement.Pages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            goToFirstPage();

            //SqlConnection sqlConnection2 = new SqlConnection(projectInfo.connectionString);
            //sqlConnection2.Open();
            //string command2 = "insert into UsersInfos values ('" + "hamed" + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "','" + null + "', '" + null + "', '" + null + "', '" + null + "', '" + DateTime.Now + "', '" + DateTime.Now + "' )";
            //SqlCommand sqlCommand2 = new SqlCommand(command1, sqlConnection2);
            //sqlCommand2.BeginExecuteNonQuery();
            //sqlConnection2.delayedClose();
        }

        void goToFirstPage()
        {
            MainWindowFrame.Content = new FirstPage(goToLoginPage);
        }

        void goToLoginPage()
        {
            MainWindowFrame.Content = new LoginPage(goToSignUpPage, goToAdminPanelPage,
                                                    goToEmployeePanelPage, goToUserPanelPage);
        }

        void goToSignUpPage()
        {
            MainWindowFrame.Content = new SignUpPage(goToLoginPage, goToPaymentPage);
        }

        void goToPaymentPage(Person person)
        {
            User user = person as User;
            MainWindowFrame.Content = new PaymentPage(goToLoginPage, goToSignUpPage, user);
        }

        void goToAdminPanelPage(Person person)
        {
            Admin admin = person as Admin;
            MainWindowFrame.Content = new AdminPanelPage(goToLoginPage,
                                                        goToAddEmployeePage, 
                                                        goToIncreaseBudgetPage,
                                                        goToPayEmployeePage,
                                                        goToAddBookPage,
                                                        admin);
        }

        void goToIncreaseBudgetPage(Admin admin, double increaseMoney)
        {
            MainWindowFrame.Content = new increaseBudgetPage(goToAdminPanelPage, admin, increaseMoney);
        }

        void goToPayEmployeePage(Person person)
        {
            Admin admin = person as Admin;
            MainWindowFrame.Content = new payEmployeePage(goToAdminPanelPage, admin);
        }

        void goToAddEmployeePage(Person person)
        {
            Admin admin = person as Admin;
            MainWindowFrame.Content = new addEmployeePage(goToAdminPanelPage, admin);
        }

        void goToAddBookPage(Person person)
        {
            Admin admin = person as Admin;
            MainWindowFrame.Content = new addBookPage(goToAdminPanelPage, admin);
        }

        void goToEmployeePanelPage(Person person)
        {
            Employee employee = person as Employee;
            MainWindowFrame.Content = new EmployeePanelPage(goToLoginPage, employee, goToUserInfoPage);
        }

        void goToUserInfoPage(Employee employee, User user)
        {
            MainWindowFrame.Content = new userInfoPage(employee, user, goToEmployeePanelPage);
        }

        void goToUserPanelPage(Person person)
        {
            User user = person as User;
            MainWindowFrame.Content = new UserPanelPage(goToLoginPage, user);
        }
    }
}
