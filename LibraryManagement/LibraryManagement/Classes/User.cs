using System;
using System.Data;
using System.Globalization;
namespace LibraryManagement.Classes
{
    class User : Person
    {
        int daysleft = 0;
        DateTime registerdate = DateTime.Today;
        DateTime renewaldate=DateTime.Today;
        public User(string userName, string firstName, string lastName,
                    Role role, string phoneNumber, string email, string password,double moneyBag)
                : base(userName, firstName, lastName, role, phoneNumber, email, password,moneyBag)
        {
        }
        public void searchbook(Book a)
        {

        }
        public void seeeshterak()
        {
            DateTime rooz = DateTime.Today;
            TimeSpan baqi = rooz.Subtract(renewaldate);
            if(int.Parse(baqi.ToString())<=0)
            {
                //bayad qermez she
            }
            else
            {
                //bayad adad sabz chap she//
            }
        }
        public void tamdideshterak()
        {
            double t = 0;
         //   if(money<1000)
            //{
           //     t = 1000 - money;
                //chap she mojoodi kafi nis//
                //chap t//
          //  }
           // if(money>=1000)
           // {
           //     money = money - 1000;
             //   daysleft += 10;
              //  renewaldate = DateTime.Today;
           // }
        }
        public void seebook()
        {

        }
        public void borrowbook()
        {

        }
        public void  givebackbook()
        {

        }
        public void paypenalty()
        {

        }

        /// <summary>
        /// chap money
        /// </summary>
        /// <returns></returns>
        public double seeCash()
        {
            DataTable datatable = PeopleTable.read();
            for(int i=0;i<datatable.Rows.Count;i++)
            {
                if(datatable.Rows[i][PeopleTable.indexUserName].ToString()==this.userName)
                {
                    if(datatable.Rows[i][PeopleTable.indexMoneyBag]==null)
                    {
                        return 0;
                    }
                    else
                    {
                        return (double)datatable.Rows[i][PeopleTable.indexMoneyBag];
                    }
                }
            }
            return 0;
            
        }
        public void increasecash(double i)
        {
            //aval hedayat be safhe pardakht//
        //    money += i;
        }
        public void changeInfo(string oldUserName,Person m)
        {
            PeopleTable.update(oldUserName, m);
        }
    }
}
