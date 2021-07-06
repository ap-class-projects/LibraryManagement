using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace LibraryManagement.Classes
{
    static class DB
    {
        static string connectionString;

        static DB()
        {
            string path = Environment.CurrentDirectory + @"\..\..\Database\db.mdf";
            FileInfo DbFile = new FileInfo(path);
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DbFile.FullName + ";Integrated Security=True;Connect Timeout=30";
        }

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

        public static void write()
        {
            string a = "ali";
            int b = 10;
            bool c = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "insert into table1 values( '" + a.Trim() + "', '" + b + "', '" + c + "' )";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        public static void delete()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "delete from table1 where age = 10";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        public static void update()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string command = "update table1 SET name = '"+"reza"+"', age = '"+14+"' where name ='"+"hasan"+"'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();
        }

        static void delayedClose(this SqlConnection sqlConnection)
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
}
