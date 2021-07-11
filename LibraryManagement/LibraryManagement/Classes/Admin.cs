using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement.Classes
{
    public class Admin : Person
    {
        public Admin(
                    string userName,
                    string firstName,
                    string lastName,
                    string phoneNumber,
                    string email,
                    string password,
                    double moneyBag,
                    string imageAddress)
         : base(userName, firstName, lastName, Role.Admin, phoneNumber, email, password, moneyBag, imageAddress)
        {

        }

        public ObservableCollection<Employee> showEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            DataTable dataTable = PeopleTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.Employee.ToString())
                {
                    Employee employee = new Employee(
                        dataTable.Rows[i][PeopleTable.indexUserName].ToString(),
                        dataTable.Rows[i][PeopleTable.indexFirstName].ToString(),
                        dataTable.Rows[i][PeopleTable.indexLastName].ToString(),
                        dataTable.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                        dataTable.Rows[i][PeopleTable.indexEmail].ToString(),
                        dataTable.Rows[i][PeopleTable.indexPassword].ToString(),
                        (double)dataTable.Rows[i][PeopleTable.indexMoneyBag],
                        dataTable.Rows[i][PeopleTable.indexImageAddress].ToString());
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public ObservableCollection<Book> showBooks()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            DataTable dataTable = BooksTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Book book = new Book(
                                    dataTable.Rows[i][BooksTable.indexName].ToString(),
                                    dataTable.Rows[i][BooksTable.indexWriter].ToString(),
                                    dataTable.Rows[i][BooksTable.indexGenre].ToString(),
                                    (int)dataTable.Rows[i][BooksTable.indexPrintingNumber],
                                    (int)dataTable.Rows[i][BooksTable.indexCount]);
                books.Add(book);
            }
            return books;
        }

        public void deleteEmployee(string userName)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from People where userName = '" + userName + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        public void addEmployee(Employee employee)
        {
            PeopleTable.write(employee);
        }

        public void addBook(Book book)
        {
            DataTable dataTable = BooksTable.read();
            bool exists = false;
            int count = 0;
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                if(dataTable.Rows[i][BooksTable.indexName].ToString() == book.name)
                {
                    exists = true;
                    count = (int)dataTable.Rows[i][BooksTable.indexCount];
                    break;
                }
            }

            if(exists)
            {
                book.count = count + 1;
                BooksTable.update(book.name, book);
            }
            else
            {
                BooksTable.write(book);
            }
        }

        /// <summary>
        /// money : money to add
        /// </summary>
        /// <param name="money"></param>
        public void updateLibraryBudget(double money)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            money += this.moneyBag;
            this.moneyBag = money;
            string command = "update People SET moneyBag = '" + this.moneyBag + "' where userName ='" + this.userName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

    }
}
