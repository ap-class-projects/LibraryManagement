using LibraryManagement.Classes;
using System.Collections.ObjectModel;
using System.Data;
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
        public event PageChanger changeToAddEmployeePage;
        public event PageChanger changeToPayEmployeePage;
        public event PageChanger changeToAddBookPage;
        public event ChangeAdminPageToIncreaseBudgetPage changeAdminPageToIncreaseBudgetPage;

        public ObservableCollection<string> employeesList {get;set;}
        public ObservableCollection<string> booksList { get; set; }
        string placeHolderEmployeeInfo = "userName - firstName - lastName - email - phoneNumber";
        string placeHolderBookInfo = "name - writer - genre - printingNumber - count";
        Admin admin;

        public AdminPanelPage(
                            PageChangerNoArg changeToLoginPage,
                            PageChanger changeToAddEmployeePage,
                            ChangeAdminPageToIncreaseBudgetPage changeAdminPageToIncreaseBudgetPage,
                            PageChanger changeToPayEmployeePage,
                            PageChanger changeToAddBookPage,
                            Admin admin)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.changeToAddEmployeePage = changeToAddEmployeePage;
            this.changeAdminPageToIncreaseBudgetPage = changeAdminPageToIncreaseBudgetPage;
            this.changeToPayEmployeePage = changeToPayEmployeePage;
            this.changeToAddBookPage = changeToAddBookPage;
            this.admin = admin;
            
            employeesList = new ObservableCollection<string>();
            booksList = new ObservableCollection<string>();
            this.DataContext = this;
            updateEmployeeList();
            updateBooksList();
            budgetText.Text = this.admin.moneyBag.ToString();
        }

        private void updateEmployeeList()
        {
            employeesList.Clear();
            employeesList.Add(placeHolderEmployeeInfo);

            ObservableCollection<Employee> employees = this.admin.showEmployees();
            for (int i = 0; i < employees.Count; i++)
            {
                string employeeInfo = $"{employees[i].userName} - {employees[i].firstName} - {employees[i].lastName} - {employees[i].email} - {employees[i].phoneNumber}";
                employeesList.Add(employeeInfo);
            }
        }

        private void updateBooksList()
        {
            booksList.Clear();
            booksList.Add(placeHolderBookInfo);
            ObservableCollection<Book> books = this.admin.showBooks();

            for (int i = 0; i < books.Count; i++)
            {
                string BookInfo = $"{books[i].name} - {books[i].writer} - {books[i].genre} - {books[i].printingNumber} - {books[i].count}";
                booksList.Add(BookInfo);
            }
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("are you sure?", "exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(messageBoxResult == MessageBoxResult.Yes)
            {
                changeToLoginPage();
            }
        }

        private void addEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            changeToAddEmployeePage(this.admin);
        }

        private void deleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if(deleteEmployeeBox.Text == "")
            {
                MessageBox.Show("type username to delete!");
            }
            else
            {
                DataTable dataTable = PeopleTable.read();
                bool isValid = false;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i][PeopleTable.indexUserName].ToString() == deleteEmployeeBox.Text)
                    {
                        isValid = true;
                        this.admin.deleteEmployee(deleteEmployeeBox.Text);
                        deleteEmployeeBox.Text = "";
                        break;
                    }
                }

                if (isValid)
                {
                    updateEmployeeList();
                    MessageBox.Show("Deleted successfully!");
                }
                else
                {
                    MessageBox.Show("Username not found!");
                }
            }
        }

        private void payEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            changeToPayEmployeePage(this.admin);
        }

        private void addBookButton_Click(object sender, RoutedEventArgs e)
        {
            changeToAddBookPage(this.admin);
        }

        private void increaseBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double money = double.Parse(increaseBudgetBox.Text);
                if(money <= 0)
                {
                    MessageBox.Show("Money must be positive!");
                    return;
                }
                else
                {
                    changeAdminPageToIncreaseBudgetPage(this.admin, money);
                }
            }
            catch
            {
                MessageBox.Show("Entered money must be float!");
            }

        }
    }
}
