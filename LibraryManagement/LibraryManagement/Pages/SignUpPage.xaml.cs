using System.Windows.Controls;

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
    }
}
