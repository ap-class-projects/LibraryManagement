using System;
using System.Data;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace LibraryManagement.Classes
{
    public class User : Person
    {
        public DateTime subRegisterDate { get; set; }
        public DateTime subRenewalDate { get; set; }
        public DateTime subExpireDate { get; set; }

        public User(string userName, 
                    string firstName, 
                    string lastName,
                    string phoneNumber,
                    string email,
                    string password,
                    double moneyBag,
                    string imageAddress,
                    DateTime subRegisterDate,
                    DateTime subRenewalDate,
                    DateTime subExpireDate)
                : base(userName, firstName, lastName, Role.User, phoneNumber, email, password, moneyBag, imageAddress)
        {
            this.subRegisterDate = subRegisterDate;
            this.subRenewalDate = subRenewalDate;
            this.subExpireDate = subExpireDate;
        }

        /// <summary>
        /// returns remaining days as int
        /// </summary>
        /// <returns></returns>
        public int remainingDays()
        {
            TimeSpan timeSpan = subExpireDate.Subtract(subRegisterDate);
            return timeSpan.Days;
        }

        public ObservableCollection<Book> showAvailableBooks()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            DataTable dataTable = BooksTable.read();
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                if ((int)dataTable.Rows[j][BooksTable.indexCount] > 0)
                {
                    Book book = new Book(
                                    dataTable.Rows[j][BooksTable.indexName].ToString(),
                                    dataTable.Rows[j][BooksTable.indexWriter].ToString(),
                                    dataTable.Rows[j][BooksTable.indexGenre].ToString(),
                                    (int)dataTable.Rows[j][BooksTable.indexPrintingNumber],
                                    (int)dataTable.Rows[j][BooksTable.indexCount]);
                    books.Add(book);
                }
            }
            return books;
        }

        /// <summary>
        /// returns a list of borrowed books by this user
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Book> showBorrowedBooks()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();

            DataTable dataTable = UsersInfosTable.read();
            ObservableCollection<string> bookNames = new ObservableCollection<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == this.userName)
                {
                    for(int j = 1; j < 10; j += 2)
                    {
                        if(dataTable.Rows[i][j].ToString() != "" && dataTable.Rows[i][j] != null)
                        {
                            bookNames.Add(dataTable.Rows[i][j].ToString());  
                        }
                    }
                    break;
                }
            }

            dataTable = BooksTable.read();
            for(int i = 0; i <bookNames.Count; i++)
            {
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    if (dataTable.Rows[j][BooksTable.indexName].ToString() == bookNames[i])
                    {
                        Book book = new Book(
                                               dataTable.Rows[j][BooksTable.indexName].ToString(),
                                               dataTable.Rows[j][BooksTable.indexWriter].ToString(),
                                               dataTable.Rows[j][BooksTable.indexGenre].ToString(),
                                               (int)dataTable.Rows[j][BooksTable.indexPrintingNumber],
                                               (int)dataTable.Rows[j][BooksTable.indexCount]);
                        books.Add(book);
                        break;
                    }
                }
            }
            return books;
        }
        
        /// <summary>
        /// shows true if one of the borrowed books is delayed
        /// </summary>
        /// <returns></returns>
        public bool isDelayed()
        {
            DataTable dataTable = UsersInfosTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //finds userName
                if (dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == this.userName)
                {
                    //check books
                    for (int j = 1; j < 10; j++)
                    {
                        if (dataTable.Rows[i][j].ToString() != "" && dataTable.Rows[i][j] != null)
                        {
                            DateTime expirationDate = (DateTime)dataTable.Rows[i][j + 1];
                            if (DateTime.Compare(expirationDate, DateTime.Now) < 0)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                }
            }
            return false;
        }

        /// <summary>
        /// only updates database, check input!
        /// only call it when you know borrow count is less than 5
        /// </summary>
        /// <param name="bookName"></param>
        public void borrowBook(string bookName)
        {
            //update Books Table
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update Books SET count = '" + (bookCount(bookName) - 1) + "' where name ='" + bookName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();


            //update UsersInfos
            string[] books = new string[5];
            DataTable dataTable = UsersInfosTable.read();
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                if(dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == this.userName)
                {
                    for(int j = 1; j < 10; j += 2)
                    {
                        books[j] = dataTable.Rows[i][j].ToString();
                    }
                    break;
                }
            }

            SqlConnection sqlConnection1 = new SqlConnection(projectInfo.connectionString);
            sqlConnection1.Open();
            string command1 = "update UsersInfos SET count = '" + (bookCount(bookName) - 1) + "' where name ='" + bookName + "' ";
            SqlCommand sqlCommand1 = new SqlCommand(command1, sqlConnection1);
            sqlCommand1.BeginExecuteNonQuery();
            sqlConnection1.delayedClose();
        }


        /// <summary>
        /// returns book count
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        private int bookCount(string bookName)
        {
            DataTable booksData = BooksTable.read();
            for (int i = 0; i < booksData.Rows.Count; i++)
            {
                if (booksData.Rows[i][BooksTable.indexName].ToString() == bookName)
                {
                    return (int)booksData.Rows[i][BooksTable.indexCount];
                }
            }
            return 0;
        }

        /// <summary>
        /// searchbook by bookName
        /// only one book will return
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public ObservableCollection<Book> searchBookName(string bookName)
        {
            ObservableCollection<Book> findedBooks = new ObservableCollection<Book>();

            DataTable datatable = BooksTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][BooksTable.indexName].ToString() == bookName)
                {
                    Book book = new Book(
                                        datatable.Rows[i][BooksTable.indexName].ToString(),
                                        datatable.Rows[i][BooksTable.indexWriter].ToString(),
                                        datatable.Rows[i][BooksTable.indexGenre].ToString(),
                                        (int)datatable.Rows[i][BooksTable.indexPrintingNumber],
                                        (int)datatable.Rows[i][BooksTable.indexCount]);

                    findedBooks.Add(book);
                    break;
                }
            }

            return findedBooks;
        }


        /// <summary>
        /// searchbook by writerName
        /// </summary>
        /// <param name="writerName"></param>
        /// <returns></returns>
        public ObservableCollection<Book> searchBookWriter(string writerName)
        {
            ObservableCollection<Book> findedBooks = new ObservableCollection<Book>();

            DataTable datatable = BooksTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][BooksTable.indexWriter].ToString() == writerName)
                {
                    Book book = new Book(
                                        datatable.Rows[i][BooksTable.indexName].ToString(),
                                        datatable.Rows[i][BooksTable.indexWriter].ToString(),
                                        datatable.Rows[i][BooksTable.indexGenre].ToString(),
                                        (int)datatable.Rows[i][BooksTable.indexPrintingNumber],
                                        (int)datatable.Rows[i][BooksTable.indexCount]);

                    findedBooks.Add(book);
                }
            }

            return findedBooks;
        }

        public void editInfo(User user)
        {
            //same usernames
            PeopleTable.update(this.userName, user);
            this.firstName = user.firstName;
            this.lastName = user.lastName;
            this.phoneNumber = user.phoneNumber;
            this.email = user.email;
            this.imageAddress = user.imageAddress;
        }

        /// <summary>
        /// adds one month
        /// udates this user fields
        /// updates renewal date
        /// updates sub expire date
        /// updates user money bag
        /// database update
        /// </summary>
        public void extendSub()
        {
            subRenewalDate = DateTime.Now;
            subExpireDate = subExpireDate.AddDays(30);
            moneyBag -= projectInfo.monthlySubCost;

            //update UsersInfos Table
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "update UsersInfos SET subRenewalDate = '" + subRenewalDate + "', subExpireDate = '"+ subExpireDate +"' where name ='" + this.userName + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();

            //update People Table
            PeopleTable.update(this.userName, this);
        }











        /// <summary>
        /// baraye tamdid eshterak, 0:kar anjam shode -1:money null boode -2:karbari ba oon esm nist   adadmosbat:meqdar money kam oomade+bayad mojodi kafi nis chap she
        /// </summary>
        public double tamdidEshterak()
        {
            double t = 0, adad = 0;
            DataTable datatable = PeopleTable.read();
            DataTable dataTable2 = UsersInfosTable.read();
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
                                if (dataTable2.Rows[j][UsersInfosTable.indexUserName].ToString() == this.userName)
                                {
                                    datatable.Rows[j][UsersInfosTable.indexSubRenewalDate] = DateTime.Today;
                                    DateTime a = (DateTime)datatable.Rows[j][UsersInfosTable.indexSubRenewalDate];
                                    datatable.Rows[j][UsersInfosTable.indexSubExpireDate] = a.AddDays(10);
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



        //public int count(Book a)
        //{
        //    int tedad = 0;
        //    DataTable b = UsersInfosTable.read();
        //    for (int i = 0; i < b.Rows.Count; i++)
        //    {
        //        if (b.Rows[i][UsersInfosTable.indexBook1] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook2] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook3] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook4] != null)
        //        {
        //            tedad++;
        //        }
        //        if (b.Rows[i][UsersInfosTable.indexBook5] != null)
        //        {
        //            tedad++;
        //        }
        //    }
        //    return tedad;
        //}


        /// <summary>
        /// bargardondan ketab adadmanfi:mojoodi nakafi 0:kar anjam shode 1:aslan mojood nabood:|
        /// </summary>
        /// <param name="bookname"></param>
        /// <returns></returns>
        public double giveBackBbook(string bookname)
        {
            int penalty = 0;
            DataTable data2 = PeopleTable.read();
            DataTable datatable = UsersInfosTable.read();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {


                if (datatable.Rows[i][UsersInfosTable.indexBook1].ToString() == bookname)
                {
                    if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate1], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate2], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate3], DateTime.Today) < 0 ||
                        DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate4], DateTime.Today) < 0 || DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate5], DateTime.Today) < 0)
                    {
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate1], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate1];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate2], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate2];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate3], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate3];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate4], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate4];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                        if (DateTime.Compare((DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate5], DateTime.Today) < 0)
                        {
                            DateTime n = (DateTime)datatable.Rows[i][UsersInfosTable.indexExpireDate5];
                            TimeSpan q = n.Subtract(DateTime.Today);
                            int q1 = int.Parse(q.ToString());
                            penalty += q1 * 100;
                        }
                    }
                }
            }
            for (int i = 0; i < data2.Rows.Count; i++)
            {
                if (data2.Rows[i][PeopleTable.indexUserName].ToString() == this.userName)
                {
                    double cash = (double)data2.Rows[i][PeopleTable.indexMoneyBag];
                    if (cash < penalty)
                    {
                        return cash - penalty;
                    }
                    else
                    {
                        data2.Rows[i][PeopleTable.indexMoneyBag] = (double)data2.Rows[i][PeopleTable.indexMoneyBag] - penalty;
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
            DataTable data = UsersInfosTable.read();
            int sum = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][UsersInfosTable.indexUserName].ToString() == this.userName)
                {
                    DateTime a1 = (DateTime)data.Rows[i][UsersInfosTable.indexExpireDate1];
                    TimeSpan a2 = DateTime.Today.Subtract(a1);
                    int adad1 = int.Parse(a2.ToString());
                    adad1 = adad1 * 100;
                    sum = sum + adad1;
                    DateTime b1 = (DateTime)data.Rows[i][UsersInfosTable.indexExpireDate2];
                    TimeSpan b2 = DateTime.Today.Subtract(b1);
                    int adad2 = int.Parse(b2.ToString());
                    adad2 = adad2 * 100;
                    sum = sum + adad2;
                    DateTime c1 = (DateTime)data.Rows[i][UsersInfosTable.indexExpireDate3];
                    TimeSpan c2 = DateTime.Today.Subtract(c1);
                    int adad3 = int.Parse(c2.ToString());
                    adad3 = adad3 * 100;
                    sum = sum + adad3;
                    DateTime d1 = (DateTime)data.Rows[i][UsersInfosTable.indexExpireDate4];
                    TimeSpan d2 = DateTime.Today.Subtract(d1);
                    int adad4 = int.Parse(d2.ToString());
                    adad4 = adad4 * 100;
                    sum = sum + adad4;
                    DateTime e1 = (DateTime)data.Rows[i][UsersInfosTable.indexExpireDate5];
                    TimeSpan e2 = DateTime.Today.Subtract(e1);
                    int adad5 = int.Parse(e2.ToString());
                    adad5 = adad5 * 100;
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
        //public void changeInfo(string oldUserName, string userName, string firstName, string lastName,
        //              Role role1, string phoneNumber, string email, string password, double moneyBag1)
        //{
        //    Person jadid = new User(userName, firstName, lastName, this.role, phoneNumber, email, password, this.moneyBag);
        //    PeopleTable.update(oldUserName, jadid as User);
        //}
    }
}
