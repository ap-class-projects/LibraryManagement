using LibraryManagement.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for payEmployeePage.xaml
    /// </summary>
    public partial class payEmployeePage : Page
    {
        public event PageChanger changeToAdminPanelPage;
        Admin admin;
        double totalSalariesToPay;

        public payEmployeePage(PageChanger changeToAdminPanelPage, Admin admin)
        {
            InitializeComponent();
            this.changeToAdminPanelPage = changeToAdminPanelPage;
            this.admin = admin;
            updateMoneyText();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            changeToAdminPanelPage(this.admin);
        }

        private void updateMoneyText()
        {
            DataTable dataTable = PeopleTable.read();
            int countEmployee = 0;
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][PeopleTable.indexRole].ToString() == Role.Employee.ToString())
                {
                    countEmployee++;
                }
            }
            totalSalariesToPay = projectInfo.salaryPerEmployee * countEmployee;
            moneyToPay.Text = moneyToPay.Text + $" {totalSalariesToPay}";
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
                            if(passwordBox.Password == this.admin.password)
                            {
                                if(this.admin.moneyBag >= totalSalariesToPay)
                                {
                                    this.admin.paySalaries(totalSalariesToPay);
                                    MessageBox.Show("Salaries paid - returning to admin panel");
                                    changeToAdminPanelPage(this.admin);
                                }
                                else
                                {
                                    MessageBox.Show("Not enough budget!");
                                }
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
    }
}
