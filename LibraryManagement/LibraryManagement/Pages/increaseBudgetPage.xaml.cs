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
    /// Interaction logic for increaseBudgetPage.xaml
    /// </summary>
    public partial class increaseBudgetPage : Page
    {
        public event PageChanger changeToAdminPanelPage;
        Admin admin;
        double money;

        public increaseBudgetPage(PageChanger changeToAdminPanelPage, Admin admin, double money)
        {
            InitializeComponent();
            this.changeToAdminPanelPage = changeToAdminPanelPage;
            this.money = money;
            this.admin = admin;
            moneyToPay.Text = moneyToPay.Text + $" {money}";
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (cardNumber0.Text == "" ||
                cardNumber1.Text == "" ||
                cardNumber2.Text == "" ||
                cardNumber3.Text == "" ||
                cvv.Text == "" ||
                year.Text == "" ||
                month.Text == ""||
                adminPasswordBox.Password == "")
            {
                MessageBox.Show("Please fill all of the given fields!");
            }
            else
            {
                string cardNumber = cardNumber0.Text + cardNumber1.Text + cardNumber2.Text + cardNumber3.Text;
                if (mRegex.cardIsValid(cardNumber))
                {
                    if (mRegex.cvvIsValid(cvv.Text))
                    {
                        if (mRegex.expireDateIsValid(year.Text, month.Text))
                        {
                            if(adminPasswordBox.Password == this.admin.password)
                            {
                                PeopleTable.updateMoneyBag(this.admin.userName, this.money + admin.moneyBag);
                                this.admin.moneyBag = this.money + admin.moneyBag;
                                MessageBox.Show("Budget updated! - returning to admin panel");
                                changeToAdminPanelPage(this.admin);
                            }
                            else
                            {
                                MessageBox.Show("Wrong password!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Expire date is not valid!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("CVV is not valid!");
                    }
                }
                else
                {
                    MessageBox.Show("Card number is not valid!");
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            changeToAdminPanelPage(this.admin);
        }
    }
}
