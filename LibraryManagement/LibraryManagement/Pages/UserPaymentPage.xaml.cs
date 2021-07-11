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
    /// Interaction logic for UserPaymentPage.xaml
    /// </summary>
    public partial class UserPaymentPage : Page
    {
        public event PageChanger changeToUserPanelPage;
        User user;
        double money;
        public UserPaymentPage(User user, double moneyToPay, PageChanger changeToUserPanelPage)
        {
            InitializeComponent();

            this.user = user;
            this.money = moneyToPay;
            this.changeToUserPanelPage = changeToUserPanelPage;

            updateMoneyText();
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            changeToUserPanelPage(this.user);
        }

        private void updateMoneyText()
        {
            moneyToPay.Text = moneyToPay.Text + $" {this.money}";
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (cardNumber0.Text == "" ||
                cardNumber1.Text == "" ||
                cardNumber2.Text == "" ||
                cardNumber3.Text == "" ||
                cvv.Text == "" ||
                year.Text == "" ||
                month.Text == "")
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
                            this.user.increaseBudget(money);
                            MessageBox.Show("Budget increased - returning to user panel");
                            changeToUserPanelPage(this.user);
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
    }
}
