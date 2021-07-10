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

        public EmployeePanelPage(PageChangerNoArg changeToLoginPage, Employee employee, ChangeEmployeePageToUserInfoPage changeEmployeePageToUserInfoPage)
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
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            changeToLoginPage();
        }

        private void showAllBooksButton_Click(object sender, RoutedEventArgs e)
        {
            booksCollection.Clear();
            booksCollection.Add(placeHolderBook);
            ObservableCollection<Book> tempCollection = employee.seeBook();
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
            ObservableCollection<Book> tempCollection = employee.seeBorrowBook();
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
            ObservableCollection<Book> tempCollection = employee.seeAvailableBook();
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
                image.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void allMembersButton_Click(object sender, RoutedEventArgs e)
        {
            usersCollection.Clear();
            usersCollection.Add(placeHolderUser);
            ObservableCollection<User> tempCollection = employee.seeUsers();
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
            ObservableCollection<User> tempCollection = employee.seeDelayGiveBackMan();
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
            ObservableCollection<User> tempCollection = employee.seeDelayPayMan();
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
                User user = employee.seePerson(showUserInfoBox.Text);
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
    }
}
