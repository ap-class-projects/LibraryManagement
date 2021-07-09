using LibraryManagement.Classes;
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
        public PageChangerNoArg changeToLoginPage;
        Employee employee;
        public ObservableCollection<string> collection { get; set; }

        public EmployeePanelPage(PageChangerNoArg changeToLoginPage, Employee employee)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.employee = employee;

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
