using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Collections.ObjectModel;
namespace LibraryManagement.Classes
{
    public class User : Person
    {
        //int daysleft = 0;
        //DateTime registerdate = DateTime.Today;
        //DateTime renewaldate=DateTime.Today;
        public User(string userName, string firstName, string lastName,
                    Role role, string phoneNumber, string email, string password, double moneyBag)
                : base(userName, firstName, lastName, role, phoneNumber, email, password, moneyBag)
        {
            //tarikh  sakht accaunt ro sabt kon//
        }
        
        /// <summary>
        /// searchbook az noe esm ketab
        /// </summary>
        /// <param name="name"></param>
        public void searchBookName(string name)
        {
            DataTable datatable = BooksTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][BooksTable.indexName].ToString() == name)
                {

                }
            }
        }

        /// <summary>
        /// searchbook az noe nevisande
        /// </summary>
        /// <param name="writerman"></param>
        public void searchBookWriter(string writerman)
        {
            DataTable datatable = BooksTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][BooksTable.indexWriter].ToString() == writerman)
                {

                }
            }
        }
        /// <summary>
        /// baraye namayesh eshterak.mosabat sabz,manfi qermez -100:yani mojood nis;
        /// </summary>
        /// <returns></returns>
        public int seeEshterak()
        {
            DataTable datatable2 = UsersInfos.read();
            for (int i = 0; i < datatable2.Rows.Count; i++)
            {
                if (datatable2.Rows[i][UsersInfos.indexUserName].ToString() == this.userName)
                {
                    DateTime a = (DateTime)datatable2.Rows[i][UsersInfos.indexExpireDateTime];
                    TimeSpan b =a.Subtract(DateTime.Today);

                    int c = int.Parse(b.ToString());
                    return c;
                }
            }
            return -100;
        }
        /// <summary>
        /// baraye tamdid eshterak, 0:kar anjam shode -1:money null boode -2:karbari ba oon esm nist   adadmosbat:meqdar money kam oomade+bayad mojodi kafi nis chap she
        /// </summary>
        public double tamdidEshterak()
        {
            double t = 0, adad = 0;
            DataTable datatable = PeopleTable.read();
            DataTable dataTable2 = UsersInfos.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][PeopleTable.indexUserName].ToString() == this.userName)
                {
                    if (datatable.Rows[i][PeopleTable.indexMoneyBag] == null)
                    {
                        t = 0;
                        return -1;
                    }
                    else
                    {
                        t = (double)datatable.Rows[i][PeopleTable.indexMoneyBag];
                        if (t < 1000)
                        {
                            adad = 1000 - t;
                            return adad;
                        }
                        if (t >= 1000)
                        {
                            adad = t - 1000;
                            for (int j = 0; j < dataTable2.Rows.Count; j++)
                            {
                                if (dataTable2.Rows[j][UsersInfos.indexUserName].ToString() == this.userName)
                                {
                                    datatable.Rows[j][UsersInfos.indexRenewalDate] = DateTime.Today;
                                    DateTime a =(DateTime) datatable.Rows[j][UsersInfos.indexRenewalDate];

                                    datatable.Rows[j][UsersInfos.indexExpireDateTime] = a.AddDays(10);
                                }
                            }
                        }
                        datatable.Rows[i][PeopleTable.indexMoneyBag] = adad;
                        return 0;
                    }
                }

            }
            return -2;

        }
        public ObservableCollection<Book> seeBook()
        {
            ObservableCollection<Book> book = new ObservableCollection<Book>();
            DataTable datatable1 = BooksTable.read();
            for(int i=0;i<datatable1.Rows.Count;i++)
            {
                Book a = new Book(datatable1.Rows[i][BooksTable.indexName].ToString(), datatable1.Rows[i][BooksTable.indexWriter].ToString(), datatable1.Rows[i][BooksTable.indexGenre].ToString(),(int)datatable1.Rows[i][BooksTable.indexPrintingNumber],(int) datatable1.Rows[i][BooksTable.indexcount]);

                book.Add(a);
            }
            return book;
        }
        /// <summary>
        /// qarz dadan false:shart ha barqarar nis true:okaye
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool borrowBook(Book m)
        {
            int t = count(m);
            if(t==5)

            {
                return false;
            }
            DataTable datatable1 = UsersInfos.read();
            for (int i = 0; i < datatable1.Rows.Count; i++)
            {
                if (datatable1.Rows[i][UsersInfos.indexUserName].ToString() == this.userName)
                {
                    if (DateTime.Compare((DateTime)datatable1.Rows[i][UsersInfos.indexExpireDate1], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable1.Rows[i][UsersInfos.indexExpireDate2], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable1.Rows[i][UsersInfos.indexExpireDate3], DateTime.Today) < 0 ||
                        DateTime.Compare((DateTime)datatable1.Rows[i][UsersInfos.indexExpireDate4], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable1.Rows[i][UsersInfos.indexExpireDate5], DateTime.Today) < 0)
                    {
                        return false;
                    }
                    DateTime n =(DateTime) datatable1.Rows[i][UsersInfos.indexExpireDateTime];
                    TimeSpan q = n.Subtract((DateTime)datatable1.Rows[i][UsersInfos.indexRenewalDate]);
                    int q1 = int.Parse(q.ToString());
                    if (q1<7)
                    {
                        return false;
                    }
                    if(m.count==0)
                    {
                        return false;
                    }
                }
            }
            m.count--;
            return true;
        }

       public int count(Book a)
        {
            int tedad = 0;
            DataTable b = UsersInfos.read();
            for(int i=0;i<b.Rows.Count;i++)
            {
                if(b.Rows[i][UsersInfos.indexBook1]!=null)
                {
                    tedad++;
                }
                if (b.Rows[i][UsersInfos.indexBook2] != null)
                {
                    tedad++;
                }
                if (b.Rows[i][UsersInfos.indexBook3] != null)
                {
                    tedad++;
                }
                if (b.Rows[i][UsersInfos.indexBook4] != null)
                {
                    tedad++;
                }
                if (b.Rows[i][UsersInfos.indexBook5] != null)
                {
                    tedad++;
                }
            }
            return tedad;
        }



       /// <summary>
       /// bargardondan ketab adadmanfi:mojoodi nakafi 0:kar anjam shode 1:aslan mojood nabood:|
       /// </summary>
       /// <param name="bookname"></param>
       /// <returns></returns>
        public double  giveBackBbook(string bookname)
        {
            int penalty = 0;
            DataTable data2 = PeopleTable.read();
            DataTable datatable = UsersInfos.read() ;
            for(int i=0;i<datatable.Rows.Count;i++)
            {
                
                
                if (datatable.Rows[i][UsersInfos.indexBook1].ToString()==bookname)
                {
                    if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate1], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate2], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate3], DateTime.Today) < 0 ||
                        DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate4], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate5], DateTime.Today) < 0)
                    {
                        if(DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate1], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfos.indexExpireDate1];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate2], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfos.indexExpireDate2];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate3], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfos.indexExpireDate3];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate4], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfos.indexExpireDate4];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfos.indexExpireDate5], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfos.indexExpireDate5];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                    }
                }
            }
            for(int i=0;i<data2.Rows.Count;i++)
            {
                if(data2.Rows[i][PeopleTable.indexUserName].ToString()==this.userName)
                {
                    double cash = (double)data2.Rows[i][PeopleTable.indexMoneyBag];
                    if(cash<penalty)
                    {
                        return cash - penalty;
                    }
                    else
                    {
                        data2.Rows[i][PeopleTable.indexMoneyBag]= (double)data2.Rows[i][PeopleTable.indexMoneyBag]-penalty;
                        return 0;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// mohasebe jarime takhir va bargardandanesh
        /// </summary>
        public int payPenalty()
        {
            DataTable data= UsersInfos.read();
            int sum = 0;
            for(int i=0;i<data.Rows.Count;i++)
            {
                if (data.Rows[i][UsersInfos.indexUserName].ToString() == this.userName)
                {
                    DateTime a1 = (DateTime)data.Rows[i][UsersInfos.indexExpireDate1];
                    TimeSpan a2 = DateTime.Today.Subtract(a1);
                    int adad1 = int.Parse(a2.ToString());
                    adad1 = adad1 * 100;
                    sum = sum + adad1;
                    DateTime b1 = (DateTime)data.Rows[i][UsersInfos.indexExpireDate2];
                    TimeSpan b2 = DateTime.Today.Subtract(b1);
                    int adad2 = int.Parse(b2.ToString());
                    adad2 = adad2 * 100;
                    sum = sum + adad2;
                    DateTime c1 = (DateTime)data.Rows[i][UsersInfos.indexExpireDate3];
                    TimeSpan c2 = DateTime.Today.Subtract(c1);
                    int adad3 = int.Parse(c2.ToString());
                    adad3 = adad3 * 100;
                    sum = sum + adad3;
                    DateTime d1 = (DateTime)data.Rows[i][UsersInfos.indexExpireDate4];
                    TimeSpan d2 = DateTime.Today.Subtract(d1);
                    int adad4 = int.Parse(d2.ToString());
                    adad4 = adad4 * 100;
                    sum = sum + adad4;
                    DateTime e1 = (DateTime)data.Rows[i][UsersInfos.indexExpireDate5];
                    TimeSpan e2 = DateTime.Today.Subtract(e1);
                    int adad5 = int.Parse(e2.ToString());
                    adad5= adad5 * 100;

                    sum = sum + adad5;
                }
            }
            return sum;
        }

        /// <summary>
        /// chap money 0:null ya mojood nabashe qeir sefr:meqdar money mojood
        /// </summary>
        /// <returns></returns>
        public double seeCash()
        {
            DataTable datatable = PeopleTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][PeopleTable.indexUserName].ToString() == this.userName)
                {
                    if (datatable.Rows[i][PeopleTable.indexMoneyBag] == null)
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
        /// <summary>
        /// charge hesab true:charge anjam mishe   false:karbar mojood nis ya hesabesh null ast
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool increaseCash(double a)
        {
            //aval hedayat be safhe pardakht//
            DataTable datatable = PeopleTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][PeopleTable.indexUserName].ToString() == this.userName)
                {
                    if (datatable.Rows[i][PeopleTable.indexMoneyBag] == null)
                    {
                        return false;
                    }
                    else
                    {
                        datatable.Rows[i][PeopleTable.indexMoneyBag] = (double)datatable.Rows[i][PeopleTable.indexMoneyBag] + a;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// taqir etelaat shakhs user
        /// </summary>
        /// <param name="oldUserName"></param>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="role1"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="moneyBag1"></param>
        public void changeInfo(string oldUserName, string userName, string firstName, string lastName,
                      Role role1, string phoneNumber, string email, string password, double moneyBag1)
        {
            Person jadid = new User(userName, firstName, lastName, this.role, phoneNumber, email, password, this.moneyBag);
            PeopleTable.update(oldUserName, jadid as User);
        }
    }
}
