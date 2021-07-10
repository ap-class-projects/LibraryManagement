using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public event PageChangerNoArg changeToLoginPage;

        public FirstPage(PageChangerNoArg changeToLoginPage)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            changeToLoginPage();
        }
    }
}
