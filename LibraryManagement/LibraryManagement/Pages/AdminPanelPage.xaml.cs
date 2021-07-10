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
        Admin admin;

        public AdminPanelPage(PageChangerNoArg changeToLoginPage, PageChanger changeToAddEmployeePage,
            ChangeAdminPageToIncreaseBudgetPage changeAdminPageToIncreaseBudgetPage, PageChanger changeToPayEmployeePage,
            PageChanger changeToAddBookPage, Admin admin)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.admin = admin;
            this.changeToAddEmployeePage = changeToAddEmployeePage;
            this.changeToPayEmployeePage = changeToPayEmployeePage;
            this.changeToAddBookPage = changeToAddBookPage;
            this.changeAdminPageToIncreaseBudgetPage = changeAdminPageToIncreaseBudgetPage;
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
            DataTable dataTable = PeopleTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.Employee.ToString())
                {
                    string employeeInfo = $"{dataTable.Rows[i][PeopleTable.indexUserName].ToString()} - {dataTable.Rows[i][PeopleTable.indexFirstName].ToString()} - {dataTable.Rows[i][PeopleTable.indexLastName].ToString()} - {dataTable.Rows[i][PeopleTable.indexPhoneNumber].ToString()}";
                    employeesList.Add(employeeInfo);
                }
            }
        }

        private void updateBooksList()
        {
            booksList.Clear();
            DataTable dataTable = BooksTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string BookInfo = $"{dataTable.Rows[i][BooksTable.indexName].ToString()} - {dataTable.Rows[i][BooksTable.indexWriter].ToString()} - {dataTable.Rows[i][BooksTable.indexGenre].ToString()} - {dataTable.Rows[i][BooksTable.indexPrintingNumber].ToString()} - {dataTable.Rows[i][BooksTable.indexCount].ToString()}";
                booksList.Add(BookInfo);
            }
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("are you sure?", "exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(messageBoxResult == MessageBoxResult.Yes)
                changeToLoginPage();
            
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
                        PeopleTable.delete(deleteEmployeeBox.Text);
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
                    MessageBox.Show("Money must be positive");
                    return;
                }
                changeAdminPageToIncreaseBudgetPage(this.admin, money);
            }
            catch
            {
                MessageBox.Show("Entered money must be float!");
            }

        }
    }
}
