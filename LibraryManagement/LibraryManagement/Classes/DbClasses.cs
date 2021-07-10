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
        /// Closes connection with 4 second delay
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
        static string connectionString;
        static PeopleTable()
        {
            string path = Environment.CurrentDirectory + @"\..\..\Database\db.mdf";
            FileInfo DbFile = new FileInfo(path);
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DbFile.FullName + ";Integrated Security=True;Connect Timeout=30";
        }

        public const int indexUserName = 0;
        public const int indexFirstName = 1;
        public const int indexLastName = 2;
        public const int indexRole = 3;
        public const int indexPhoneNumber = 4;
        public const int indexEmail = 5;
        public const int indexPassword = 6;
        public const int indexMoneyBag = 7;

        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "select * from People";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.delayedClose();
            return dataTable;
        }

        public static void write(Person person)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "insert into People values ('"+ person.userName +"', '"+ person.firstName +"', '"+ person.lastName +"', '"+ person.role.ToString() +"', '"+ person.phoneNumber +"', '"+ person.email +"', '"+ person.password +"','"+person.moneyBag+"', '"+null+"')";
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
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "delete from People where userName = '"+ userName +"'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        /// <summary>
        /// !before update check person exists!
        /// </summary>
        /// <param name="person"></param>
        public static void update(string oldUserName, Person person)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "update People SET userName = '"+ person.userName +"', firstName = '"+ person.firstName +"', lastName = '"+ person.lastName +"', role = '"+ person.role.ToString() +"', phoneNumber = '"+ person.phoneNumber +"', email = '"+ person.email +"', password = '"+ person.password +"',moneyBag = '"+person.moneyBag+ "' where userName ='" + oldUserName +"' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        /// <summary>
        /// updates moneyBag
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="money"></param>
        public static void updateMoneyBag(string userName, double money)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "update People SET   moneyBag = '" + money + "' where userName ='" + userName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }
    }

    /// <summary>
    /// contains methods for manipulating Books table
    /// </summary>
    static class BooksTable
    {
        static string connectionString;
        static BooksTable()
        {
            string path = Environment.CurrentDirectory + @"\..\..\Database\db.mdf";
            FileInfo DbFile = new FileInfo(path);
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DbFile.FullName + ";Integrated Security=True;Connect Timeout=30";
        }

        public const int indexName = 0;
        public const int indexWriter = 1;
        public const int indexGenre = 2;
        public const int indexPrintingNumber = 3;
        public const int indexcount = 4;

        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "select * from Books";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.delayedClose();
            return dataTable;
        }

        public static void write(Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "insert into Books values ('" + book.name + "', '" + book.writer + "', '" + book.genre + "', '" + book.printingNumber + "', '" + book.count + "')";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        /// <summary>
        /// delete by printing number
        /// </summary>
        /// <param name="printingNumber"></param>
        public static void delete(string printingNumber)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "delete from Books where printingNumber = '" + printingNumber + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        public static void update(string oldBookName, Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "update Books SET name = '" + book.name + "', writer = '" + book.writer + "', genre = '" + book.genre + "', printingNumber = '" + book.printingNumber + "', count = '" + book.count + "' where name ='" + oldBookName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }
    }

    static class UsersInfos
    {
        static string connectionstring;
        static UsersInfos()
        {
            string path = Environment.CurrentDirectory + @"\..\..\Database\db.mdf";
            FileInfo DbFile = new FileInfo(path);
            connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DbFile.FullName + ";Integrated Security=True;Connect Timeout=30";
        }

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
        public const int indexRegisterDate = 11;
        public const int indexRenewalDate = 12;
        public const int indexExpireDateTime = 13;
        public static DataTable read()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            string command = "select * from UsersInfos";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.delayedClose();
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
            sqlConnection.delayedClose();
        }
        */

        /// <summary>
        /// delete from People table by userName
        /// </summary>
        /// <param name="userName"></param>
        public static void delete(string userName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            string command = "delete from UsersInfos where userName = '" + userName + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
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
                sqlConnection.delayedClose();
            }
        */
    }
}
