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
        FirstPage firstPage;
        LoginPage loginPage;
        SignUpPage signUpPage;
        AdminPanelPage adminPanelPage;
        EmployeePanelPage employeePanelPage;
        UserPanelPage userPanelPage;

        public MainWindow()
        {
            InitializeComponent();

            firstPage = new FirstPage();
            loginPage = new LoginPage();
            signUpPage = new SignUpPage();
            adminPanelPage = new AdminPanelPage();
            employeePanelPage = new EmployeePanelPage();
            userPanelPage = new UserPanelPage();

            goToFirstPage();

            subscribeToFirstPageEvents();
            subscribeToLoginPageEvents();
            subscribeToSignUpPageEvents();
            subscribeToAdminPanelPageEvents();
        }

        void subscribeToFirstPageEvents()
        {
            firstPage.changeToLoginPage += goToLoginPage;
        }

        void subscribeToLoginPageEvents()
        {
            loginPage.changeToAdminPanelPage += goToAdminPanelPage;
            loginPage.changeToEmployeePanelPage += goToEmployeePanelPage;
            loginPage.changeToUserPanelPage += goToUserPanelPage;
            loginPage.changeToSignUpPage += goToSignUpPage;
        }

        void subscribeToSignUpPageEvents()
        {
            signUpPage.changeToLoginPage += goToLoginPage;
        }

        void subscribeToAdminPanelPageEvents()
        {
            adminPanelPage.changeToLoginPage += goToLoginPage;
        }

        void goToFirstPage()
        {
            MainWindowFrame.Content = firstPage;
        }

        void goToLoginPage()
        {
            MainWindowFrame.Content = loginPage;
        }

        void goToSignUpPage()
        {
            MainWindowFrame.Content = signUpPage;
        }

        void goToAdminPanelPage()
        {
            MainWindowFrame.Content = adminPanelPage;
        }

        void goToEmployeePanelPage()
        {
            MainWindowFrame.Content = employeePanelPage;
        }

        void goToUserPanelPage()
        {
            MainWindowFrame.Content = userPanelPage;
        }   
    }
}
