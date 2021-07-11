using LibraryManagement.Classes;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for EmployeePanelPage.xaml
    /// </summary>
    public partial class EmployeePanelPage : Page
    {
        public event PageChangerNoArg changeToLoginPage;
        public event ChangeEmployeePageToUserInfoPage changeEmployeePageToUserInfoPage;

        Employee employee;
        public ObservableCollection<string> booksCollection { get; set; }
        public ObservableCollection<string> usersCollection { get; set; }
        string placeHolderBook = "name - writer - genre - printingNumber - count";
        string placeHolderUser = "userName - firstName - lastName - phoneNumber - email";
        string newImageAddress = "";

        public EmployeePanelPage(PageChangerNoArg changeToLoginPage,
                                Employee employee,
                                ChangeEmployeePageToUserInfoPage changeEmployeePageToUserInfoPage)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.employee = employee;
            this.changeEmployeePageToUserInfoPage = changeEmployeePageToUserInfoPage;
            booksCollection = new ObservableCollection<string>();
            usersCollection = new ObservableCollection<string>();
            this.DataContext = this;
            showBorrowedBooks();
            showDelayedReturners();
            employeeBudgetTextBlock.Text = employee.moneyBag.ToString();
            initializeEditTabInfos();
        }

        private void initializeEditTabInfos()
        {
            if (employee.imageAddress == "")
            {
                imageBox.Source = new BitmapImage(new Uri(@"/LibraryManagement;component/Images/no-image.jpg", UriKind.Relative));
            }
            else
            {
                try
                {
                    imageBox.Source = new BitmapImage(new Uri(employee.imageAddress));
                }
                catch
                {
                    imageBox.Source = new BitmapImage(new Uri(@"/LibraryManagement;component/Images/no-image.jpg", UriKind.Relative));
                }
            }
            userNameTextBlock.Text = employee.userName;
            firstNameBox.Text = employee.firstName;
            lastNameBox.Text = employee.lastName;
            phoneNumberBox.Text = employee.phoneNumber;
            emailBox.Text = employee.email;
            passwordBox.Password = "";
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("are you sure?", "exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                changeToLoginPage();
            }
        }

        private void showAllBooksButton_Click(object sender, RoutedEventArgs e)
        {
            booksCollection.Clear();
            booksCollection.Add(placeHolderBook);
            ObservableCollection<Book> tempCollection = employee.showAllBooks();
            for (int i = 0; i < tempCollection.Count; i++)
            {
                string bookInfo = $"{tempCollection[i].name} - {tempCollection[i].writer} - {tempCollection[i].genre} - {tempCollection[i].printingNumber} - {tempCollection[i].count}";
                booksCollection.Add(bookInfo);
            }
        }

        private void showBorrowedBooksButton_Click(object sender, RoutedEventArgs e)
        {
            showBorrowedBooks();
        }

        private void showBorrowedBooks()
        {
            booksCollection.Clear();
            booksCollection.Add(placeHolderBook);
            ObservableCollection<Book> tempCollection = employee.showBorrowedBooks();
            for (int i = 0; i < tempCollection.Count; i++)
            {
                string bookInfo = $"{tempCollection[i].name} - {tempCollection[i].writer} - {tempCollection[i].genre} - {tempCollection[i].printingNumber} - {tempCollection[i].count}";
                booksCollection.Add(bookInfo);
            }
        }

        private void showAvailableBooksButton_Click(object sender, RoutedEventArgs e)
        {
            booksCollection.Clear();
            booksCollection.Add(placeHolderBook);
            ObservableCollection<Book> tempCollection = employee.showAvailableBooks();
            for (int i = 0; i < tempCollection.Count; i++)
            {
                string bookInfo = $"{tempCollection[i].name} - {tempCollection[i].writer} - {tempCollection[i].genre} - {tempCollection[i].printingNumber} - {tempCollection[i].count}";
                booksCollection.Add(bookInfo);
            }
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
                imageBox.Source = new BitmapImage(new Uri(op.FileName));
                newImageAddress = op.FileName;
            }
        }

        private void allMembersButton_Click(object sender, RoutedEventArgs e)
        {
            usersCollection.Clear();
            usersCollection.Add(placeHolderUser);
            ObservableCollection<User> tempCollection = employee.showAllUsers();
            for (int i = 0; i < tempCollection.Count; i++)
            {
                string userInfo = $"{tempCollection[i].userName} - {tempCollection[i].firstName} - {tempCollection[i].lastName} - {tempCollection[i].phoneNumber} - {tempCollection[i].email}";
                usersCollection.Add(userInfo);
            }
        }

        private void delayedReturnersButton_Click(object sender, RoutedEventArgs e)
        {
            showDelayedReturners();
        }

        private void showDelayedReturners()
        {
            usersCollection.Clear();
            usersCollection.Add(placeHolderUser);
            ObservableCollection<User> tempCollection = employee.showDelayedReturners();
            for (int i = 0; i < tempCollection.Count; i++)
            {
                string userInfo = $"{tempCollection[i].userName} - {tempCollection[i].firstName} - {tempCollection[i].lastName} - {tempCollection[i].phoneNumber} - {tempCollection[i].email}";
                usersCollection.Add(userInfo);
            }
        }

        private void delayedSubButton_Click(object sender, RoutedEventArgs e)
        {
            usersCollection.Clear();
            usersCollection.Add(placeHolderUser);
            ObservableCollection<User> tempCollection = employee.showDelaySubUsers();
            for (int i = 0; i < tempCollection.Count; i++)
            {
                string userInfo = $"{tempCollection[i].userName} - {tempCollection[i].firstName} - {tempCollection[i].lastName} - {tempCollection[i].phoneNumber} - {tempCollection[i].email}";
                usersCollection.Add(userInfo);
            }
        }

        private void showUserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (showUserInfoBox.Text == "")
            {
                MessageBox.Show("Please type an user name");
            }
            else
            {
                User user = employee.showUserInfo(showUserInfoBox.Text);
                if(user == null)
                {
                    MessageBox.Show("No one found!");
                }
                else
                {
                    changeEmployeePageToUserInfoPage(employee, user);
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            initializeEditTabInfos();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                MessageBox.Show("Enter the password!");
            }
            else
            {
                if (mRegex.nameIsValid(firstNameBox.Text))
                {
                    if (mRegex.nameIsValid(lastNameBox.Text))
                    {
                        if (mRegex.phoneNumberIsValid(phoneNumberBox.Text))
                        {
                            if (mRegex.emailIsValid(emailBox.Text))
                            {
                                if (passwordBox.Password == employee.password)
                                {
                                    string temp = newImageAddress == "" ? employee.imageAddress : newImageAddress;

                                    Employee employeeTemp = new Employee(employee.userName,
                                                                        firstNameBox.Text,
                                                                        lastNameBox.Text,
                                                                        phoneNumberBox.Text,
                                                                        emailBox.Text,
                                                                        employee.password,
                                                                        employee.moneyBag,
                                                                        temp);
                                    employee.editInfo(employeeTemp);
                                    initializeEditTabInfos();
                                    MessageBox.Show("Infos changed successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Wrong password!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Email is not valid!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Phone number is not valid!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Last name is not valid!");
                    }
                }
                else
                {
                    MessageBox.Show("First name is not valid!");
                }
            }
        }

        private void resetFields_Click(object sender, RoutedEventArgs e)
        {
            initializeEditTabInfos();
        }
    }
}
