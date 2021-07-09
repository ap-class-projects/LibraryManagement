using LibraryManagement.Classes;
using System.Windows.Controls;

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for UserPanelPage.xaml
    /// </summary>
    public partial class UserPanelPage : Page
    {
        public event PageChangerNoArg changeToLoginPage;
        User user;

        public UserPanelPage(PageChangerNoArg changeToLoginPage, User user)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.user = user;
        }
    }
}
