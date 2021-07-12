using LibraryManagement.Classes;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for UserPanelPage.xaml
    /// </summary>
    public partial class UserPanelPage : Page
    {
        public event PageChangerNoArg changeToLoginPage;
        public event ChangeUserPageToUserPaymentPage changeUserPageToUserPaymentPage;

        public ObservableCollection<string> availableBooks { get; set; }
        public ObservableCollection<string> searchResultBooks { get; set; }
        public ObservableCollection<string> borrowedBooks { get; set; }
        public ObservableCollection<Book> borrowedBooksTypeBook { get; set; }

        User user;
        string placeHolderBookInfo = "name - writer - genre - printingNumber - count";
        string newImageAddress = "";

        public UserPanelPage(
                            PageChangerNoArg changeToLoginPage,
                            User user,
                            ChangeUserPageToUserPaymentPage changeUserPageToUserPaymentPage)
        {
            InitializeComponent();

            this.changeToLoginPage = changeToLoginPage;
            this.changeUserPageToUserPaymentPage = changeUserPageToUserPaymentPage;
            this.user = user;

            availableBooks = new ObservableCollection<string>();
            searchResultBooks = new ObservableCollection<string>();
            borrowedBooks = new ObservableCollection<string>();
            borrowedBooksTypeBook = new ObservableCollection<Book>();

            this.DataContext = this;

            updateAvailableBooks();
            updateSearchResultBooks(null);
            updateBorrowedBooks();

            updateSubTab();
            updateBudgetTab();
        }

        private void updateAvailableBooks()
        {
            availableBooks.Clear();
            availableBooks.Add(placeHolderBookInfo);
            ObservableCollection<Book> tempCollection = user.showAvailableBooks();
            for (int i = 0; i < tempCollection.Count; i++)
            {
                string bookInfo = $"{tempCollection[i].name} - {tempCollection[i].writer} - {tempCollection[i].genre} - {tempCollection[i].printingNumber} - {tempCollection[i].count}";
                availableBooks.Add(bookInfo);
            }
        }

        private void updateSearchResultBooks(ObservableCollection<Book> findedBooks)
        {
            if(findedBooks == null)
            {
                searchResultBooks.Clear();
                searchResultBooks.Add(placeHolderBookInfo);
            }
            else
            {
                searchResultBooks.Clear();
                searchResultBooks.Add(placeHolderBookInfo);
                ObservableCollection<Book> tempCollection = findedBooks;
                for (int i = 0; i < tempCollection.Count; i++)
                {
                    string bookInfo = $"{tempCollection[i].name} - {tempCollection[i].writer} - {tempCollection[i].genre} - {tempCollection[i].printingNumber} - {tempCollection[i].count}";
                    searchResultBooks.Add(bookInfo);
                }
            }
        }
        
        private void updateBorrowedBooks()
        {
            borrowedBooks.Clear();
            borrowedBooks.Add(placeHolderBookInfo);

            ObservableCollection<Book> tempCollection = user.showBorrowedBooks();
            borrowedBooksTypeBook.Clear();

            foreach(var item in tempCollection)
            {
                borrowedBooksTypeBook.Add(item);
            }

            for (int i = 0; i < borrowedBooksTypeBook.Count; i++)
            {
                string bookInfo = $"{borrowedBooksTypeBook[i].name} - {borrowedBooksTypeBook[i].writer} - {borrowedBooksTypeBook[i].genre} - {borrowedBooksTypeBook[i].printingNumber} - {borrowedBooksTypeBook[i].count}";
                borrowedBooks.Add(bookInfo);
            }
        }


        void updateSubTab()
        {
            daysRemainingTextBlock.Text = user.remainingDays() > 0 ? user.remainingDays().ToString() : "0";
            daysPassedTextBlock.Text = user.remainingDays() <= 0 ? (user.remainingDays() * -1).ToString() : "0";
        }

        void updateBudgetTab()
        {
            budgetTextBlock.Text = this.user.moneyBag.ToString();
        }

        private void uploadButton_Click(object sender, System.Windows.RoutedEventArgs e)
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


        private void logOutbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("are you sure?", "exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                changeToLoginPage();
            }
        }

        private void bookNameCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            writerNameCheckBox.IsChecked = false;
        }

        private void writerNameCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            bookNameCheckBox.IsChecked = false;
        }

        private void borrowButton_Click(object sender, RoutedEventArgs e)
        {
            if(borrowTextBlock.Text == "")
            {
                MessageBox.Show("Enter a book name!");
            }
            else
            {
                if(borrowedBooksTypeBook.Count >= 5)
                {
                    MessageBox.Show("You have maximum numbers of borrowed books!");
                }
                else
                {
                    if(user.isDelayed())
                    {
                        MessageBox.Show("You have delayed to return your book/books! ");
                    }
                    else
                    {
                        if(user.remainingDays() >= 7)
                        {
                            //checks alreay have it or not
                            bool alreadyHave = false;
                            for(int i = 0; i < borrowedBooksTypeBook.Count; i++)
                            {
                                if (borrowedBooksTypeBook[i].name == borrowTextBlock.Text)
                                {
                                    alreadyHave = true;
                                    break;
                                }
                            }

                            if(alreadyHave)
                            {
                                MessageBox.Show("You already have this book!");
                            }
                            else
                            {
                                bool bookIsAvailable = false;

                                ObservableCollection<Book> allAvailableBooks = user.showAvailableBooks();

                                for(int i = 0; i < allAvailableBooks.Count; i++)
                                {
                                    if(allAvailableBooks[i].name == borrowTextBlock.Text)
                                    {
                                        bookIsAvailable = true;
                                    }
                                }

                                if(!bookIsAvailable)
                                {
                                    MessageBox.Show("We dont have book with this name!");
                                }
                                else
                                {
                                    user.borrowBook(borrowTextBlock.Text);
                                    borrowTextBlock.Text = "";
                                    updateAvailableBooks();
                                    //updateSearchResultBooks();
                                    updateBorrowedBooks();
                                    MessageBox.Show("Book borrowed successfully!");
                                } 
                            }
                        }
                        else
                        {
                            MessageBox.Show("Less than 7 days of sub can not borrow books!");
                        }
                    }
                }
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(searchTextBox.Text == "")
            {
                MessageBox.Show("Enter a book name to search!");
            }
            else
            {

                if (bookNameCheckBox.IsChecked == true)
                {
                    ObservableCollection<Book> temp = user.searchBookName(searchTextBox.Text);
                    if(temp.Count == 0)
                    {
                        MessageBox.Show("Nothing found!");
                    }
                    updateSearchResultBooks(temp);
                }
                else if (writerNameCheckBox.IsChecked == true)
                {
                    ObservableCollection<Book> temp = user.searchBookWriter(searchTextBox.Text);
                    if (temp.Count == 0)
                    {
                        MessageBox.Show("Nothing found!");
                    }
                    updateSearchResultBooks(temp);
                }
                else
                {
                    MessageBox.Show("Please check one of the checkboxes!");
                }
            }
        }

        private void returnBorrowedBookButton_Click(object sender, RoutedEventArgs e)
        {
            Book book = borrowedBooksComboBox.SelectedItem as Book;
            if(book == null)
            {
                MessageBox.Show("Select a book to return!");
            }
            else
            {

            }
        }

        private void extendSubButton_Click(object sender, RoutedEventArgs e)
        {
            if(user.moneyBag >= 30)
            {
                user.extendSub();
                MessageBox.Show("Sub extended successfully for 1 month");
            }
            else
            {
                MessageBox.Show($"Budget is not enough - min budget needed : {projectInfo.monthlySubCost}");
            }
        }

        private void increaseBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double money = double.Parse(increaseBudgetTextBox.Text);
                if (money <= 0)
                {
                    MessageBox.Show("Money must be positive!");
                    return;
                }
                else
                {
                    changeUserPageToUserPaymentPage(this.user, money);
                }
            }
            catch
            {
                MessageBox.Show("Entered money must be float!");
            }

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
                                if (passwordBox.Password == user.password)
                                {
                                    string temp = newImageAddress == "" ? user.imageAddress : newImageAddress;

                                    User userTemp = new User(
                                                            user.userName,
                                                            firstNameBox.Text,
                                                            lastNameBox.Text,
                                                            phoneNumberBox.Text,
                                                            emailBox.Text,
                                                            user.password,
                                                            user.moneyBag,
                                                            temp,
                                                            user.subRegisterDate,
                                                            user.subRenewalDate,
                                                            user.subExpireDate);

                                    user.editInfo(userTemp);
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

        private void initializeEditTabInfos()
        {
            if (user.imageAddress == "")
            {
                imageBox.Source = new BitmapImage(new Uri(@"/LibraryManagement;component/Images/no-image.jpg", UriKind.Relative));
            }
            else
            {
                try
                {
                    imageBox.Source = new BitmapImage(new Uri(user.imageAddress));
                }
                catch
                {
                    imageBox.Source = new BitmapImage(new Uri(@"/LibraryManagement;component/Images/no-image.jpg", UriKind.Relative));
                }
            }
            userNameTextBlock.Text = user.userName;
            firstNameBox.Text = user.firstName;
            lastNameBox.Text = user.lastName;
            phoneNumberBox.Text = user.phoneNumber;
            emailBox.Text = user.email;
            passwordBox.Password = "";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            initializeEditTabInfos();
        }
    }
}
