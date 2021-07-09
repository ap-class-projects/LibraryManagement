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
            MainWindowFrame.Content = new SignUpPage(goToLoginPage);
        }

        void goToAdminPanelPage()
        {
            MainWindowFrame.Content = new AdminPanelPage(goToLoginPage);
        }

        void goToEmployeePanelPage()
        {
            MainWindowFrame.Content = new EmployeePanelPage(goToLoginPage);
        }

        void goToUserPanelPage()
        { 
            MainWindowFrame.Content = new UserPanelPage();
        }   
    }
}
