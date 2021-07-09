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

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for AdminPanelPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page
    {
        public event PageChanger changeToLoginPage;

        public AdminPanelPage(PageChanger changeToLoginPage)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            changeToLoginPage();
        }
    }
}
