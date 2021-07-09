using LibraryManagement.Classes;
using LibraryManagement.Pages;
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
            double money;
            if (person is User)
            {
                money = 100;
            }
            else
            {
                //person is admin
                money = 0;
            }
            
            MainWindowFrame.Content = new PaymentPage(goToLoginPage, goToSignUpPage, goToAdminPanelPage,person, money);
        }

        void goToAdminPanelPage(Person person)
        {
            Admin admin = new Admin(person.userName, person.firstName,
                                    person.lastName, person.role,
                                    person.phoneNumber, person.email,
                                    person.password, person.moneyBag);

            MainWindowFrame.Content = new AdminPanelPage(goToLoginPage, goToAddEmployeePage, 
                                                        goToIncreaseBudgetPage, goToPayEmployeePage,
                                                        goToAddBookPage, admin);
        }

        void goToIncreaseBudgetPage(Admin admin, double increaseMoney)
        {
            MainWindowFrame.Content = new increaseBudgetPage(goToAdminPanelPage, admin, increaseMoney);
        }

        void goToPayEmployeePage(Person person)
        {
            Admin admin = new Admin(person.userName, person.firstName,
                                    person.lastName, person.role,
                                    person.phoneNumber, person.email,
                                    person.password, person.moneyBag);
            MainWindowFrame.Content = new payEmployeePage(goToAdminPanelPage, admin);
        }

        void goToAddEmployeePage(Person person)
        {
            Admin admin = new Admin(person.userName, person.firstName,
                                    person.lastName, person.role,
                                    person.phoneNumber, person.email,
                                    person.password, person.moneyBag);

            MainWindowFrame.Content = new addEmployeePage(goToAdminPanelPage, admin);
        }

        void goToAddBookPage(Person person)
        {
            Admin admin = new Admin(person.userName, person.firstName,
                                    person.lastName, person.role,
                                    person.phoneNumber, person.email,
                                    person.password, person.moneyBag);

            MainWindowFrame.Content = new addBookPage(goToAdminPanelPage, admin);
        }

        void goToEmployeePanelPage(Person person)
        {
            Employee employee = new Employee(person.userName, person.firstName,
                                             person.lastName, person.role,
                                             person.phoneNumber, person.email,
                                             person.password, person.moneyBag);

            MainWindowFrame.Content = new EmployeePanelPage(goToLoginPage, employee);
        }

        void goToUserPanelPage(Person person)
        {
            User user = new User(person.userName, person.firstName,
                                 person.lastName, person.role,
                                 person.phoneNumber, person.email,
                                 person.password, person.moneyBag);

            MainWindowFrame.Content = new UserPanelPage(goToLoginPage, user);
        }
    }
}
