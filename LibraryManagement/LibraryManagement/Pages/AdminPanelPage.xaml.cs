using LibraryManagement.Classes;
using System.Windows;
using System.Windows.Controls;


namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for AdminPanelPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page
    {
        public event PageChangerNoArg changeToLoginPage;
        Admin admin;

        public AdminPanelPage(PageChangerNoArg changeToLoginPage, Admin admin)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.admin = admin;
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            changeToLoginPage();
        }
    }
}
