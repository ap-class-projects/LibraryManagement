using LibraryManagement.Classes;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
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
        public ObservableCollection<string> borrowedBooks { get; set; }
        User user;

        public UserPanelPage(PageChangerNoArg changeToLoginPage, User user)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.user = user;

            borrowedBooks = new ObservableCollection<string>();
            borrowedBooks.Add("1");
            borrowedBooks.Add("2");
            this.DataContext = this;
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
                image.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void logOutbutton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            changeToLoginPage();
        }

        private void checkBox1_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            checkBox2.IsChecked = false;
        }

        private void checkBox2_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            checkBox1.IsChecked = false;
        }
    }
}
