using LibraryManagement.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for userInfoPage.xaml
    /// </summary>
    public partial class userInfoPage : Page
    {
        Employee employee;
        User user;
        public event PageChanger changeToEmployeePanelPage;

        public userInfoPage(Employee employee, User user, PageChanger changeToEmployeePanelPage)
        {
            InitializeComponent();
            this.employee = employee;
            this.user = user;
            this.changeToEmployeePanelPage = changeToEmployeePanelPage;
            updateInfo();
        }

        private void updateInfo()
        {
            userNameTextBlock.Text = user.userName;
            firstNameTextBlock.Text = user.firstName;
            lastNameTextBlock.Text = user.lastName;

            emailTextBlock.Text = user.email;
            phoneNumberTextBlock.Text = user.phoneNumber;

            registerDateTextBlock.Text = user.subRegisterDate.ToString();
            renewalDateTextBlock.Text = user.subRenewalDate.ToString();

            int remainingDays = user.remainingDays();
            remainingDaysTextBlock.Text = remainingDays.ToString();
            if(remainingDays <= 0)
            {
                remainingDaysTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                remainingDaysTextBlock.Foreground = Brushes.Green;
            }

            DataTable dataTable = UsersInfosTable.read();
            List<string> booksInfoCollection = new List<string>();
            List<bool> isDelayed = new List<bool>();
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                if(dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == user.userName)
                {
                    for(int j = 1; j < 10; j += 2)
                    {
                        if( dataTable.Rows[i][j].ToString() != "" &&
                            dataTable.Rows[i][j] != null)
                        {
                            booksInfoCollection.Add($"{dataTable.Rows[i][j].ToString()} - {((DateTime)dataTable.Rows[i][j+1]).ToString()}");
                            DateTime expireDate = (DateTime)dataTable.Rows[i][j + 1];
                            if ( DateTime.Compare(expireDate, DateTime.Now) <= 0 )
                            {
                                isDelayed.Add(true);
                            }
                            else
                            {
                                isDelayed.Add(false);
                            }
                        }
                    }
                    break;
                }
            }

            List<TextBlock> textBlocks = new List<TextBlock>();
            textBlocks.Add(book1TextBlock);
            textBlocks.Add(book2TextBlock);
            textBlocks.Add(book3TextBlock);
            textBlocks.Add(book4TextBlock);
            textBlocks.Add(book5TextBlock);

            for(int i = 0; i < textBlocks.Count; i++)
            {
                textBlocks[i].Text = "";
                textBlocks[i].Foreground = Brushes.Black;
            }


            for (int i = 0; i < booksInfoCollection.Count; i++)
            {
                textBlocks[i].Text = booksInfoCollection[i];
                if(isDelayed[i])
                {
                    textBlocks[i].Foreground = Brushes.Red;
                }
                else
                {
                    textBlocks[i].Foreground = Brushes.Green;
                }
            }

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
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            changeToEmployeePanelPage(employee);
        }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                MessageBox.Show("Enter the password!");
            }
            else
            {
                if (passwordBox.Password == employee.password)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete user", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        employee.removeUser(user);
                        MessageBox.Show("User deleted - returning to employee page");
                        changeToEmployeePanelPage(employee);
                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
        }
    }
}
