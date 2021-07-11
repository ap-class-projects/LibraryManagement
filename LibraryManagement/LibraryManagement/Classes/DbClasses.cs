using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace LibraryManagement.Classes
{
    static class DbClasses
    {
        /// <summary>
        /// Closes connection with 4 second delay.
        /// use this only after writing data
        /// </summary>
        /// <param name="sqlConnection"></param>
        public static void delayedClose(this SqlConnection sqlConnection)
        {
            void close()
            {
                Thread.Sleep(4000);
                sqlConnection.Close();
            }
            
            Thread thread = new Thread(close);
            thread.Start();
        }
    }

    /// <summary>
    /// contains methods for manipulating People table
    /// </summary>
    static class PeopleTable
    {
        public const int indexUserName = 0;
        public const int indexFirstName = 1;
        public const int indexLastName = 2;
        public const int indexRole = 3;
        public const int indexPhoneNumber = 4;
        public const int indexEmail = 5;
        public const int indexPassword = 6;
        public const int indexMoneyBag = 7;
        public const int indexImageAddress = 8;

        /// <summary>
        /// returns all of the data
        /// </summary>
        /// <returns></returns>
        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "select * from People";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        /// <summary>
        /// use for employee and 1 table of user 
        /// </summary>
        /// <param name="person"></param>
        public static void write(Person person)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "insert into People values ('"+ person.userName +"', '"+ person.firstName +"', '"+ person.lastName +"', '"+ person.role.ToString() +"', '"+ person.phoneNumber +"', '"+ person.email +"', '"+ person.password +"','"+person.moneyBag+"', '"+person.imageAddress+"')";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        /// <summary>
        /// delete from People table by userName
        /// </summary>
        /// <param name="userName"></param>
        public static void delete(string userName)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from People where userName = '"+ userName +"'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }

        /// <summary>
        /// !before update check person exists!
        /// </summary>
        /// <param name="person"></param>
        public static void update(string oldUserName, Person person)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update People SET userName = '"+ person.userName +"', firstName = '"+ person.firstName +"', lastName = '"+ person.lastName +"', role = '"+ person.role.ToString() +"', phoneNumber = '"+ person.phoneNumber +"', email = '"+ person.email +"', password = '"+ person.password +"',moneyBag = '"+person.moneyBag+ "' where userName ='" + oldUserName +"' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }

        /// <summary>
        /// updates moneyBag
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="money"></param>
        public static void updateMoneyBag(string userName, double money)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update People SET   moneyBag = '" + money + "' where userName ='" + userName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }
    }

    /// <summary>
    /// contains methods for manipulating Books table
    /// </summary>
    static class BooksTable
    {
        public const int indexName = 0;
        public const int indexWriter = 1;
        public const int indexGenre = 2;
        public const int indexPrintingNumber = 3;
        public const int indexCount = 4;

        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "select * from Books";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        public static void write(Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "insert into Books values ('" + book.name + "', '" + book.writer + "', '" + book.genre + "', '" + book.printingNumber + "', '" + book.count + "')";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }

        /// <summary>
        /// delete by printing number
        /// </summary>
        /// <param name="printingNumber"></param>
        public static void delete(string printingNumber)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from Books where printingNumber = '" + printingNumber + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }

        public static void update(string oldBookName, Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update Books SET name = '" + book.name + "', writer = '" + book.writer + "', genre = '" + book.genre + "', printingNumber = '" + book.printingNumber + "', count = '" + book.count + "' where name ='" + oldBookName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }

        /// <summary>
        /// adds 1 to the count number
        /// </summary>
        /// <param name="bookName"></param>
        public static void addOneToCount(string bookName)
        {
            DataTable dataTable = BooksTable.read();
            int count = 0;
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][indexName].ToString() == bookName)
                {
                    count = (int)dataTable.Rows[i][indexCount] + 1;
                }
            }

            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update Books SET count = '" + count + "' where name ='" + bookName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }
    }

    static class UsersInfosTable
    {
        public const int indexUserName = 0;
        public const int indexBook1 = 1;
        public const int indexExpireDate1 = 2;
        public const int indexBook2 = 3;
        public const int indexExpireDate2 = 4;
        public const int indexBook3 = 5;
        public const int indexExpireDate3 = 6;
        public const int indexBook4 = 7;
        public const int indexExpireDate4 = 8;
        public const int indexBook5 = 9;
        public const int indexExpireDate5 = 10;
        public const int indexSubRegisterDate = 11;
        public const int indexSubRenewalDate = 12;
        public const int indexSubExpireDate = 13;

        /// <summary>
        /// returns all of the data
        /// </summary>
        /// <returns></returns>
        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "select * from UsersInfos";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        /*
        public static void write(Person person)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            string command = "insert into People values ('" + person.userName + "', '" + person.firstName + "', '" + person.lastName + "', '" + person.role.ToString() + "', '" + person.phoneNumber + "', '" + person.email + "', '" + person.password + "','" + person.moneyBag + "')";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }
        */

        /// <summary>
        /// delete from UsersInfos table by userName
        /// </summary>
        /// <param name="userName"></param>
        public static void delete(string userName)
        {
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from UsersInfos where userName = '" + userName + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
        }

        /// <summary>
        /// !before update check person exists!
        /// </summary>
        /// <param name="person"></param>
        /*    public static void update(string oldUserName, Person person)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionstring);
                sqlConnection.Open();
                string command = "update People SET userName = '" + person.userName + "', firstName = '" + person.firstName + "', lastName = '" + person.lastName + "', role = '" + person.role.ToString() + "', phoneNumber = '" + person.phoneNumber + "', email = '" + person.email + "', password = '" + person.password + "',moneyBag = '" + person.moneyBag + "' where name ='" + oldUserName + "' ";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.BeginExecuteNonQuery();
            }
        */
    }
}
