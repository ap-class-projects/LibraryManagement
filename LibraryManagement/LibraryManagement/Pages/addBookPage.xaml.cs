using LibraryManagement.Classes;
using System;
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

namespace LibraryManagement.Pages
{
    /// <summary>
    /// Interaction logic for addBookPage.xaml
    /// </summary>
    public partial class addBookPage : Page
    {
        public event PageChanger changeToAdminPanelPage;
        Admin admin;

        public addBookPage(PageChanger changeToAdminPanelPage, Admin admin)
        {
            InitializeComponent();
            this.changeToAdminPanelPage = changeToAdminPanelPage;
            this.admin = admin;
        }

        private bool fieldIsEmpty()
        {
            if(nameBox.Text == "" ||
               writerBox.Text == "" ||
               genreBox.Text == "" ||
               printingNumberBox.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// if book already exist adds 1 to count
        /// else : makes new book with count = 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBookButton_Click(object sender, RoutedEventArgs e)
        {
            if(fieldIsEmpty())
            {
                MessageBox.Show("Please fill all of the given fields!");
            }
            else
            {
                try
                {
                    int printingNumber = int.Parse(printingNumberBox.Text);
                    Book book = new Book(nameBox.Text, writerBox.Text, genreBox.Text, printingNumber, 1);
                    this.admin.addBook(book);
                    MessageBox.Show("Book added successfully - returning to admin panel");
                    changeToAdminPanelPage(this.admin);
                }
                catch
                {
                    MessageBox.Show("Printing number must be type int!");
                }
            }
        }
        
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            changeToAdminPanelPage(this.admin);
        }
    }
}
