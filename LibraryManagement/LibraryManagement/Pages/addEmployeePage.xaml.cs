using LibraryManagement.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for addEmployeePage.xaml
    /// </summary>
    public partial class addEmployeePage : Page
    {
        public event PageChanger changeToAdminPanelPage;
        Admin admin;
        string employeeImageAddress = "";

        public addEmployeePage(PageChanger changeToAdminPanelPage, Admin admin)
        {
            InitializeComponent();
            this.changeToAdminPanelPage = changeToAdminPanelPage;
            this.admin = admin;
        }

        private bool fieldIsEmpty()
        {
            if (userNameBox.Text == "" ||
               firstNameBox.Text == "" ||
               lastNameBox.Text == "" ||
               emailBox.Text == "" ||
               phoneNumberBox.Text == "" ||
               passwordBox.Password == "" ||
               confirmPasswordBox.Password == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool userNameExists()
        {
            DataTable dataTable = PeopleTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (userNameBox.Text == dataTable.Rows[i][PeopleTable.indexUserName].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void signUpButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (fieldIsEmpty())
            {
                MessageBox.Show("Please fill all of the given fields!");
            }
            else
            {
                if (userNameExists())
                {
                    MessageBox.Show("This username already exists!");
                }
                else
                {
                    //check regex

                    bool fieldsAreValid = true;

                    //userName
                    if (mRegex.userNameIsValid(userNameBox.Text))
                    {
                        userNameRegexWarn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        userNameRegexWarn.Visibility = Visibility.Visible;
                        fieldsAreValid = false;
                    }

                    //firstName
                    if (mRegex.nameIsValid(firstNameBox.Text))
                    {
                        firstNameRegexWarn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        firstNameRegexWarn.Visibility = Visibility.Visible;
                        fieldsAreValid = false;
                    }

                    //lastName
                    if (mRegex.nameIsValid(lastNameBox.Text))
                    {
                        lastNameRegexWarn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lastNameRegexWarn.Visibility = Visibility.Visible;
                        fieldsAreValid = false;
                    }

                    //email
                    if (mRegex.emailIsValid(emailBox.Text))
                    {
                        emailRegexWarn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        emailRegexWarn.Visibility = Visibility.Visible;
                        fieldsAreValid = false;
                    }

                    //phoneNumber
                    if (mRegex.phoneNumberIsValid(phoneNumberBox.Text))
                    {
                        phoneNumberRegexWarn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        phoneNumberRegexWarn.Visibility = Visibility.Visible;
                        fieldsAreValid = false;
                    }

                    //password
                    if (mRegex.passwordIsValid(passwordBox.Password))
                    {
                        passwordRegexWarn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        passwordRegexWarn.Visibility = Visibility.Visible;
                        fieldsAreValid = false;
                    }

                    if (fieldsAreValid)
                    {
                        //all regexes were ok
                        //sign up
                        if (confirmPasswordBox.Password != passwordBox.Password)
                        {
                            MessageBox.Show("Please make sure your passwords match");
                        }
                        else
                        {
                            Employee employee = new Employee
                                                (userNameBox.Text,
                                                 firstNameBox.Text,
                                                 lastNameBox.Text,
                                                 phoneNumberBox.Text,
                                                 emailBox.Text,
                                                 passwordBox.Password,
                                                 0,
                                                 this.employeeImageAddress);
                            //PeopleTable.write(employee);
                            this.admin.addEmployee(employee);
                            MessageBox.Show("Employee added successfully! - returning to admin panel");
                            changeToAdminPanelPage(admin);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please fill fields like mentioned patterns!");
                    }

                }
            }
        }

        private void userNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userNameRegexWarn.Visibility = Visibility.Hidden;
        }

        private void firstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            firstNameRegexWarn.Visibility = Visibility.Hidden;
        }

        private void lastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lastNameRegexWarn.Visibility = Visibility.Hidden;
        }

        private void emailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            emailRegexWarn.Visibility = Visibility.Hidden;
        }

        private void phoneNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            phoneNumberRegexWarn.Visibility = Visibility.Hidden;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordRegexWarn.Visibility = Visibility.Hidden;
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(op.FileName));
                this.employeeImageAddress = op.FileName;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            changeToAdminPanelPage(admin);
        }
    }
}
