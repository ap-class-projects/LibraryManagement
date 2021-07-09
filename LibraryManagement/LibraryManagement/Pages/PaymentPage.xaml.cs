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
    /// Interaction logic for PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        public PageChangerNoArg changeToLoginPage;
        public event PageChangerNoArg changeToSignUpPage;
        Person person;
        double money;

        public PaymentPage(PageChangerNoArg changeToLoginPage, PageChangerNoArg changeToSignUpPage, Person person, double moneyToPay)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.changeToSignUpPage = changeToSignUpPage;
            this.person = person;
            this.money = moneyToPay;
            this.moneyToPay.Text = $"Money to pay : {money}";
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if( cardNumber0.Text == "" ||
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
                if(mRegex.cardIsValid(cardNumber))
                {
                    if(mRegex.cvvIsValid(cvv.Text))
                    {
                        if (mRegex.expireDateIsValid(year.Text, month.Text))
                        {
                            if (person is User)
                            {
                                PeopleTable.write(person);
                                MessageBox.Show("Sign up was successful! - going to login page");
                                changeToLoginPage();
                            }
                            else if (person is Admin)
                            {

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
            changeToSignUpPage();
        }
    }
}
