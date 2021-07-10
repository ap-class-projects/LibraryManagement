using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace LibraryManagement.Classes
{
    public class Employee : Person
    {
        public Employee(string userName,
                        string firstName,
                        string lastName,
                        string phoneNumber,
                        string email,
                        string password,
                        double moneyBag,
                        string imageAddress)
        : base(userName, firstName, lastName, Role.Employee, phoneNumber, email, password, moneyBag, imageAddress)
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
                Book a = new Book(datatable1.Rows[i][BooksTable.indexName].ToString(), datatable1.Rows[i][BooksTable.indexWriter].ToString(), datatable1.Rows[i][BooksTable.indexGenre].ToString(), (int)datatable1.Rows[i][BooksTable.indexPrintingNumber], (int)datatable1.Rows[i][BooksTable.indexCount]);
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
            DataTable data1 = UsersInfosTable.read();
            DataTable data2 = BooksTable.read();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                if (data1.Rows[i][UsersInfosTable.indexBook1].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfosTable.indexBook1].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexCount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfosTable.indexBook2].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfosTable.indexBook2].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexCount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfosTable.indexBook3].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfosTable.indexBook3].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexCount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfosTable.indexBook4].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfosTable.indexBook4].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexCount]);
                            listbook.Add(book);
                        }
                    }
                }
                if (data1.Rows[i][UsersInfosTable.indexBook5].ToString() != null)
                {
                    for (int j = 0; j < data2.Rows.Count; j++)
                    {
                        if (data1.Rows[i][UsersInfosTable.indexBook5].ToString() == data2.Rows[j][BooksTable.indexName].ToString())
                        {
                            Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(), (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexCount]);
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
                if ((int)data2.Rows[j][BooksTable.indexCount] > 0)
                {
                    Book book = new Book(data2.Rows[j][BooksTable.indexName].ToString(), data2.Rows[j][BooksTable.indexWriter].ToString(), data2.Rows[j][BooksTable.indexGenre].ToString(),
                        (int)data2.Rows[j][BooksTable.indexPrintingNumber], (int)data2.Rows[j][BooksTable.indexCount]);
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
            DataTable people = PeopleTable.read();

            ObservableCollection<User> users = new ObservableCollection<User>();

            for (int i = 0; i < people.Rows.Count; i++)
            {
                if (people.Rows[i][PeopleTable.indexRole].ToString() == Role.User.ToString())
                {
                    DataTable userInfos = UsersInfosTable.read();
                    DateTime? subRegisterDate = null;
                    DateTime? subRenewalDate = null;
                    DateTime? subExpireDate = null;

                    for (int j = 0; j < userInfos.Rows.Count; j++)
                    {
                        if (people.Rows[i][PeopleTable.indexUserName].ToString() == userInfos.Rows[j][UsersInfosTable.indexUserName].ToString())
                        {
                            subRegisterDate = (DateTime)userInfos.Rows[j][UsersInfosTable.indexSubRegisterDate];
                            subRenewalDate = (DateTime)userInfos.Rows[j][UsersInfosTable.indexSubRenewalDate];
                            subExpireDate = (DateTime)userInfos.Rows[j][UsersInfosTable.indexSubExpireDate];
                            break;
                        }
                    }

                    User user = new User(
                                    people.Rows[i][PeopleTable.indexUserName].ToString(),
                                    people.Rows[i][PeopleTable.indexFirstName].ToString(),
                                    people.Rows[i][PeopleTable.indexLastName].ToString(),
                                    people.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                    people.Rows[i][PeopleTable.indexEmail].ToString(),
                                    people.Rows[i][PeopleTable.indexPassword].ToString(), 
                                    (double)people.Rows[i][PeopleTable.indexMoneyBag],
                                    people.Rows[i][PeopleTable.indexImageAddress].ToString(),
                                    (DateTime)subRegisterDate,
                                    (DateTime)subRenewalDate,
                                    (DateTime)subExpireDate);
                    users.Add(user);
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
            DataTable peopleData = PeopleTable.read();
            DataTable usersInfoData = UsersInfosTable.read();
            
            ObservableCollection<User> users = new ObservableCollection<User>();

            for (int i = 0; i < peopleData.Rows.Count; i++)
            {
                if (peopleData.Rows[i][PeopleTable.indexRole].ToString() == Role.User.ToString())
                {
                    for (int j = 0; j < usersInfoData.Rows.Count; j++)
                    {
                        if (peopleData.Rows[i][PeopleTable.indexUserName].ToString() == usersInfoData.Rows[j][UsersInfosTable.indexUserName].ToString())
                        {
                            int compare1 = DateTime.Compare((DateTime)usersInfoData.Rows[j][UsersInfosTable.indexExpireDate1], DateTime.Today);
                            int compare2 = DateTime.Compare((DateTime)usersInfoData.Rows[j][UsersInfosTable.indexExpireDate2], DateTime.Today);
                            int compare3 = DateTime.Compare((DateTime)usersInfoData.Rows[j][UsersInfosTable.indexExpireDate3], DateTime.Today);
                            int compare4 = DateTime.Compare((DateTime)usersInfoData.Rows[j][UsersInfosTable.indexExpireDate4], DateTime.Today);
                            int compare5 = DateTime.Compare((DateTime)usersInfoData.Rows[j][UsersInfosTable.indexExpireDate5], DateTime.Today);

                            if (compare1 < 0 || compare2 < 0 || compare3 < 0 || compare4 < 0 || compare5 < 0)
                            {
                                User user = new User(   peopleData.Rows[i][PeopleTable.indexUserName].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexFirstName].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexLastName].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexEmail].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexPassword].ToString(),
                                                        (double)peopleData.Rows[i][PeopleTable.indexMoneyBag],
                                                        peopleData.Rows[i][PeopleTable.indexImageAddress].ToString(),
                                                        (DateTime)usersInfoData.Rows[j][UsersInfosTable.indexSubRegisterDate],
                                                        (DateTime)usersInfoData.Rows[j][UsersInfosTable.indexSubRenewalDate],
                                                        (DateTime)usersInfoData.Rows[j][UsersInfosTable.indexSubExpireDate]);
                                users.Add(user);
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
            DataTable people = PeopleTable.read();
            DataTable usersInfos = UsersInfosTable.read();

            ObservableCollection<User> users = new ObservableCollection<User>();

            for (int i = 0; i < people.Rows.Count; i++)
            {
                if (people.Rows[i][PeopleTable.indexRole].ToString() == Role.User.ToString()) 
                {
                    for (int j = 0; j < usersInfos.Rows.Count; j++)
                    {
                        if (people.Rows[i][PeopleTable.indexUserName].ToString() == usersInfos.Rows[j][UsersInfosTable.indexUserName].ToString())
                        {
                            int compare = DateTime.Compare((DateTime)usersInfos.Rows[j][UsersInfosTable.indexSubExpireDate], DateTime.Today);
                            
                            if (compare < 0)
                            {
                                User user = new User(  
                                                    people.Rows[i][PeopleTable.indexUserName].ToString(),
                                                    people.Rows[i][PeopleTable.indexFirstName].ToString(),
                                                    people.Rows[i][PeopleTable.indexLastName].ToString(),
                                                    people.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                                    people.Rows[i][PeopleTable.indexEmail].ToString(),
                                                    people.Rows[i][PeopleTable.indexPassword].ToString(),
                                                    (double)people.Rows[i][PeopleTable.indexMoneyBag],
                                                    people.Rows[i][PeopleTable.indexImageAddress].ToString(),
                                                    (DateTime)usersInfos.Rows[j][UsersInfosTable.indexSubRegisterDate],
                                                    (DateTime)usersInfos.Rows[j][UsersInfosTable.indexSubRenewalDate],
                                                    (DateTime)usersInfos.Rows[j][UsersInfosTable.indexSubExpireDate]);
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// didan etalaat yek fard khas ba search kardan esmesh 
        /// null:yani fardi ba oon esm nis dar qeir in soorat:avalin nafarr ba oon username ro bar migardone
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User seePerson(string name)
        {
            DataTable people = PeopleTable.read();

            for (int i = 0; i < people.Rows.Count; i++)
            {
                if (people.Rows[i][PeopleTable.indexUserName].ToString() == name)
                {
                    DataTable usersInfos = UsersInfosTable.read();
                    DateTime? subRegisterDate = null;
                    DateTime? subRenewalDate = null;
                    DateTime? subExpireDate = null;

                    for (int j = 0; j < usersInfos.Rows.Count; j++)
                    {
                        if (people.Rows[i][PeopleTable.indexUserName].ToString() == usersInfos.Rows[j][UsersInfosTable.indexUserName].ToString())
                        {
                            subRegisterDate = (DateTime)usersInfos.Rows[j][UsersInfosTable.indexSubRegisterDate];
                            subRenewalDate = (DateTime)usersInfos.Rows[j][UsersInfosTable.indexSubRenewalDate];
                            subExpireDate = (DateTime)usersInfos.Rows[j][UsersInfosTable.indexSubExpireDate];
                            break;
                        }
                    }

                    User user = new User(
                                        people.Rows[i][PeopleTable.indexUserName].ToString(),
                                        people.Rows[i][PeopleTable.indexFirstName].ToString(),
                                        people.Rows[i][PeopleTable.indexLastName].ToString(),
                                        people.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                        people.Rows[i][PeopleTable.indexEmail].ToString(),
                                        people.Rows[i][PeopleTable.indexPassword].ToString(),
                                        (double)people.Rows[i][PeopleTable.indexMoneyBag],
                                        people.Rows[i][PeopleTable.indexImageAddress].ToString(),
                                        (DateTime)subRegisterDate,
                                        (DateTime)subRenewalDate,
                                        (DateTime)subExpireDate);
                    return user;
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
        //public void changeInfo(string oldUserName, string userName, string firstName, string lastName,
        //               Role role1, string phoneNumber, string email, string password, double moneyBag1)
        //{
        //    Person jadid = new User(userName, firstName, lastName, this.role, phoneNumber, email, password, this.moneyBag);
        //    PeopleTable.update(oldUserName, jadid as Employee);
        //}
    }

}

