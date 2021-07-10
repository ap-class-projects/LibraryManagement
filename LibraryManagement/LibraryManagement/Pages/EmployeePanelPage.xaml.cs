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
        public PageChangerNoArg changeToLoginPage;
        Employee employee;
        public ObservableCollection<string> collection { get; set; }

        public EmployeePanelPage(PageChangerNoArg changeToLoginPage, Employee employee)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.employee = employee;

            collection = new ObservableCollection<string>();
            collection.Add($"Book name \t Count");
            this.DataContext = this;
        }

        private void logOutbutton_Click(object sender, RoutedEventArgs e)
        {
            changeToLoginPage();
        }

        private void showAllBooksButton_Click(object sender, RoutedEventArgs e)
        {
            collection.Add("hi");
        }

        private void showBorrowedBooksButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void showAvailableBooksButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
