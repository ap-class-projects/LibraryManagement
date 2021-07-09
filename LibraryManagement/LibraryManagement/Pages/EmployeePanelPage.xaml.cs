using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for EmployeePanelPage.xaml
    /// </summary>
    public partial class EmployeePanelPage : Page
    {
        public PageChanger changeToLoginPage;
        public ObservableCollection<string> collection { get; set; }

        public EmployeePanelPage(PageChanger changeToLoginPage)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            collection = new ObservableCollection<string>();
            collection.Add($"Book name \t Count");
            this.DataContext = this;
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            changeToLoginPage();
        }

        private void showAllBooksButton_Click(object sender, RoutedEventArgs e)
        {
            collection.Add("hi");
        }

        private void showBorrowedBooksButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void showAvailableBooksButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
