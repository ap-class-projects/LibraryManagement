using System;
using System.Collections.ObjectModel;
using System.Data;

namespace LibraryManagement.Classes
{
    public class Employee : Person
    {
        public Employee(string userName, string firstName, string lastName,
            Role role, string phoneNumber, string email, string password, double moneyBag)
        : base(userName, firstName, lastName, role, phoneNumber, email, password, moneyBag)
        {
        }

        /// <summary>
        /// kol ketab haro barmigardone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Book> seeBook()
        {
            ObservableCollection<Book> book = new ObservableCollection<Book>();
            DataTable datatable1 = BooksTable.read();
            for (int i = 0; i < datatable1.Rows.Count; i++)
            {
                Book a = new Book(datatable1.Rows[i][BooksTable.indexName].ToString(), datatable1.Rows[i][BooksTable.indexWriter].ToString(), datatable1.Rows[i][BooksTable.indexGenre].ToString(), (int)datatable1.Rows[i][BooksTable.indexPrintingNumber], (int)datatable1.Rows[i][BooksTable.indexcount]);
                book.Add(a);
            }
            return book;
        }

        /// <summary>
        /// ketab haye qarzi ro barmigardoone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Book> seeBorrowBook()
        {
            ObservableCollection<Book> listbook = new ObservableCollection<Book>();
            DataTable data1 = UsersInfos.read();
            DataTable data2 = BooksTable.read();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                if (data1.Rows[i][UsersInfos.indexBook1].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfos.indexBook1].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexcount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfos.indexBook2].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfos.indexBook2].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexcount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfos.indexBook3].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfos.indexBook3].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexcount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfos.indexBook4].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfos.indexBook4].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexcount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfos.indexBook5].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfos.indexBook5].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexcount]);
                            listbook.Add(book);
                        }
                    }
                }
            }
            return listbook;
        }

        /// <summary>
        /// ketabaye mojood ro barmigardoone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Book> seeAvailableBook()
        {
            ObservableCollection<Book> listbook = new ObservableCollection<Book>();
            DataTable data2 = BooksTable.read();
            for (int j = 0; j < data2.Rows.Count; j++)
            {
                if ((int)data2.Rows[j][BooksTable.indexcount] > 0)
                {
                    Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(),
                        (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexcount]);
                    listbook.Add(book);
                }
            }
            return listbook;
        }

        /// <summary>
        /// kol karbaraye az noe user ro bar migardoone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<User> seeUsers()
        {
            DataTable data = PeopleTable.read();
            ObservableCollection<User> users = new ObservableCollection<User>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][PeopleTable.indexRole].ToString() == "User")
                {
                    User a = new User(data.Rows[i][PeopleTable.indexUserName].ToString(),
                                    data.Rows[i][PeopleTable.indexFirstName].ToString(),
                                    data.Rows[i][PeopleTable.indexLastName].ToString(),
                                    Role.User,
                                    data.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                    data.Rows[i][PeopleTable.indexEmail].ToString(), 
                                    data.Rows[i][PeopleTable.indexPassword].ToString(), 
                                    double.Parse(data.Rows[i][PeopleTable.indexMoneyBag].ToString()));
                    users.Add(a);
                }
            }
            return users;
        }

        /// <summary>
        /// user haii ke hadaqal yek ketab ba time expire darand ro namayesh mide
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<User> seeDelayGiveBackMan()
        {
            DataTable dataTable = UsersInfos.read();
            DataTable data = PeopleTable.read();
            ObservableCollection<User> users = new ObservableCollection<User>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][PeopleTable.indexRole].ToString() == "User")
                {
                    for (int j = 0; j < dataTable.Rows.Count; j++)
                    {
                        if (data.Rows[i][PeopleTable.indexUserName].ToString() == dataTable.Rows[j][UsersInfos.indexUserName].ToString())
                        {
                            int compare1 = DateTime.Compare((DateTime)dataTable.Rows[j][UsersInfos.indexExpireDate1], DateTime.Today);
                            int compare2 = DateTime.Compare((DateTime)dataTable.Rows[j][UsersInfos.indexExpireDate2], DateTime.Today);
                            int compare3 = DateTime.Compare((DateTime)dataTable.Rows[j][UsersInfos.indexExpireDate3], DateTime.Today);
                            int compare4 = DateTime.Compare((DateTime)dataTable.Rows[j][UsersInfos.indexExpireDate4], DateTime.Today);
                            int compare5 = DateTime.Compare((DateTime)dataTable.Rows[j][UsersInfos.indexExpireDate5], DateTime.Today);
                            if (compare1 < 0 || compare2 < 0 || compare3 < 0 || compare4 < 0 || compare5 < 0)
                            {
                                User a = new User(data.Rows[i][PeopleTable.indexUserName].ToString(), data.Rows[i][PeopleTable.indexFirstName].ToString(), data.Rows[i][PeopleTable.indexLastName].ToString(), Role.User,
                                    data.Rows[i][PeopleTable.indexPhoneNumber].ToString(), data.Rows[i][PeopleTable.indexEmail].ToString(), data.Rows[i][PeopleTable.indexPassword].ToString(), (double)data.Rows[i][PeopleTable.indexMoneyBag]);
                                users.Add(a);
                            }
                        }
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// user haii ke haq ozviat nadadan ro bar migardoone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<User> seeDelayPayMan()
        {
            DataTable dataTable = UsersInfos.read();
            DataTable data = PeopleTable.read();
            ObservableCollection<User> users = new ObservableCollection<User>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][PeopleTable.indexRole].ToString() == "User")
                {
                    for (int j = 0; j < dataTable.Rows.Count; j++)
                    {
                        if (data.Rows[i][PeopleTable.indexUserName].ToString() == dataTable.Rows[j][UsersInfos.indexUserName].ToString())
                        {
                            int compare = DateTime.Compare((DateTime)dataTable.Rows[j][UsersInfos.indexExpireDateTime], DateTime.Today);
                            if (compare < 0)
                            {
                                User a = new User(data.Rows[i][PeopleTable.indexUserName].ToString(), data.Rows[i][PeopleTable.indexFirstName].ToString(), data.Rows[i][PeopleTable.indexLastName].ToString(), Role.User,
                                    data.Rows[i][PeopleTable.indexPhoneNumber].ToString(), data.Rows[i][PeopleTable.indexEmail].ToString(), data.Rows[i][PeopleTable.indexPassword].ToString(), (int)data.Rows[i][PeopleTable.indexMoneyBag]);
                                users.Add(a);
                            }
                        }
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// didan etalaat yek fard khas ba search kardan esmesh null:yani fardi ba oon esm nis  dar qeir in soorat:avalin nafarr ba oon username ro bar migardone
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User seePerson(string name)
        {
            DataTable data = PeopleTable.read();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][PeopleTable.indexUserName].ToString() == name)
                {
                    User a = new User(data.Rows[i][PeopleTable.indexUserName].ToString(), data.Rows[i][PeopleTable.indexFirstName].ToString(), data.Rows[i][PeopleTable.indexLastName].ToString(),
                        (Role)data.Rows[i][PeopleTable.indexRole], data.Rows[i][PeopleTable.indexPhoneNumber].ToString(), data.Rows[i][PeopleTable.indexEmail].ToString(), data.Rows[i][PeopleTable.indexPassword].ToString(), (double)data.Rows[i][PeopleTable.indexMoneyBag]);
                    return a;
                }
            }
            return null;

        }
        /// <summary>
        /// namayesh money employee -1:agar chenin shakhsi nabashe baqieadad:meqdar money fard
        /// </summary>
        /// <returns></returns>
        public double seeCash()
        {
            DataTable data = PeopleTable.read();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][PeopleTable.indexUserName].ToString() == this.userName)
                {
                    return (double)data.Rows[i][PeopleTable.indexMoneyBag];
                }
            }
            return -1;
        }
        /// <summary>
        /// taqiir etelaat fard ro namayesh mide
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
            PeopleTable.update(oldUserName, jadid as Employee);
        }
    }

}

