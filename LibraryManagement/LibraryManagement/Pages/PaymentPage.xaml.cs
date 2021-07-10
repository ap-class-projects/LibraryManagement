using LibraryManagement.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public event PageChangerNoArg changeToLoginPage;
        public event PageChangerNoArg changeToSignUpPage;

        Person person;


        public PaymentPage(PageChangerNoArg changeToLoginPage, PageChangerNoArg changeToSignUpPage, Person person)
        {
            InitializeComponent();
            this.changeToLoginPage = changeToLoginPage;
            this.changeToSignUpPage = changeToSignUpPage;
            this.person = person;
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
                            //sign up User
                            SqlConnection sqlConnection1 = new SqlConnection(projectInfo.connectionString);
                            sqlConnection1.Open();
                            string command1 = "insert into People values ('" + person.userName + "', '" + person.firstName + "', '" + person.lastName + "', '" + person.role.ToString() + "', '" + person.phoneNumber + "', '" + person.email + "', '" + person.password + "','" + person.moneyBag + "', '" + null + "')";
                            SqlCommand sqlCommand1 = new SqlCommand(command1, sqlConnection1);
                            sqlCommand1.BeginExecuteNonQuery();
                            sqlConnection1.delayedClose();

                            //SqlConnection sqlConnection2 = new SqlConnection(projectInfo.connectionString);
                            //sqlConnection2.Open();
                            //string command2 = "insert into UsersInfos values ('" + person.userName + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "','" + null + "', '" + null + "', '"+ null +"', '"+ null +"', '"+ DateTime.Now +"', '"+ DateTime.Now +"', '"+ DateTime.Now 8+ "' )";
                            //SqlCommand sqlCommand2 = new SqlCommand(command2, sqlConnection2);
                            //sqlCommand2.BeginExecuteNonQuery();
                            //sqlConnection2.delayedClose();

                            MessageBox.Show("Sign up was successful! - going to login page");
                            changeToLoginPage();
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
