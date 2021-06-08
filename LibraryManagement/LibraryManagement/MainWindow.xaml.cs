﻿using System;
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


//sql requirements
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (emailUserNameBox.Text == "" || passwordBox.Password == "")
            {
                MessageBox.Show("please fill both fields!");          
            }
            else
            {
                bool loggedIn = false;
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyProjects\LibraryManagement\LibraryManagement\LibraryManagement\database\db.mdf;Integrated Security=True;Connect Timeout=30");
                sqlConnection.Open();
                string command = "select * from UsersInfo";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if ((string)dataTable.Rows[i][0] == emailUserNameBox.Text && (string)dataTable.Rows[i][3] == passwordBox.Password)
                    {
                        loggedIn = true;
                    }
                }
                sqlConnection.Close();

                if (loggedIn)
                {
                    MessageBox.Show("logged in!");
                }
                else
                {
                    MessageBox.Show("wrong!");
                }
            }
        }
    }
}
