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
            string command = "insert into People values ('"+ person.userName +"', '"+ person.firstName +"', '"+ person.lastName +"', '"+ person.role.ToString() +"', '"+ person.phoneNumber +"', '"+ person.email +"', '"+ person.password +"')";
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
            string command = "update People SET userName = '"+ person.userName +"', firstName = '"+ person.firstName +"', lastName = '"+ person.lastName +"', role = '"+ person.role.ToString() +"', phoneNumber = '"+ person.phoneNumber +"', email = '"+ person.email +"', password = '"+ person.password +"' where name ='"+ oldUserName +"' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }
    }
}
