using System;
using System.Collections.Generic;
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
        /// returns all of the books
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Book> showAllBooks()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            DataTable dataTable = BooksTable.read();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Book book = new Book(
                                dataTable.Rows[i][BooksTable.indexName].ToString(),
                                dataTable.Rows[i][BooksTable.indexWriter].ToString(),
                                dataTable.Rows[i][BooksTable.indexGenre].ToString(),
                                (int)dataTable.Rows[i][BooksTable.indexPrintingNumber],
                                (int)dataTable.Rows[i][BooksTable.indexCount]);
                books.Add(book);
            }
            return books;
        }

        /// <summary>
        /// ketab haye qarzi ro barmigardoone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Book> showBorrowedBooks()
        {
            ObservableCollection<Book> borrowedBooks = new ObservableCollection<Book>();
            DataTable usersInfosTable = UsersInfosTable.read();
            DataTable booksTable = BooksTable.read();

            for (int i = 0; i < usersInfosTable.Rows.Count; i++)
            {
                if (usersInfosTable.Rows[i][UsersInfosTable.indexBook1].ToString() != "")
                {
                    for (int j = 0; j < booksTable.Rows.Count; j++)
                    {
                        if (usersInfosTable.Rows[i][UsersInfosTable.indexBook1].ToString() == booksTable.Rows[j][BooksTable.indexName].ToString())
                        {
                            if (!bookExistInList(borrowedBooks, booksTable.Rows[j][BooksTable.indexName].ToString()))
                            {
                                Book book = new Book(
                                            booksTable.Rows[j][BooksTable.indexName].ToString(),
                                            booksTable.Rows[j][BooksTable.indexWriter].ToString(),
                                            booksTable.Rows[j][BooksTable.indexGenre].ToString(),
                                            (int)booksTable.Rows[j][BooksTable.indexPrintingNumber],
                                            (int)booksTable.Rows[j][BooksTable.indexCount]);
                                borrowedBooks.Add(book);
                            }
                            break;
                        }
                    }
                }

                if (usersInfosTable.Rows[i][UsersInfosTable.indexBook2].ToString() != "")
                {
                    for (int j = 0; j < booksTable.Rows.Count; j++)
                    {
                        if (usersInfosTable.Rows[i][UsersInfosTable.indexBook2].ToString() == booksTable.Rows[j][BooksTable.indexName].ToString())
                        {
                            if (!bookExistInList(borrowedBooks, booksTable.Rows[j][BooksTable.indexName].ToString()))
                            {
                                Book book = new Book(
                                            booksTable.Rows[j][BooksTable.indexName].ToString(),
                                            booksTable.Rows[j][BooksTable.indexWriter].ToString(),
                                            booksTable.Rows[j][BooksTable.indexGenre].ToString(),
                                            (int)booksTable.Rows[j][BooksTable.indexPrintingNumber],
                                            (int)booksTable.Rows[j][BooksTable.indexCount]);
                                borrowedBooks.Add(book);
                            }
                            break;
                        }
                    }
                }

                if (usersInfosTable.Rows[i][UsersInfosTable.indexBook3].ToString() != "")
                {
                    for (int j = 0; j < booksTable.Rows.Count; j++)
                    {
                        if (usersInfosTable.Rows[i][UsersInfosTable.indexBook3].ToString() == booksTable.Rows[j][BooksTable.indexName].ToString())
                        {
                            if (!bookExistInList(borrowedBooks, booksTable.Rows[j][BooksTable.indexName].ToString()))
                            {
                                Book book = new Book(
                                            booksTable.Rows[j][BooksTable.indexName].ToString(),
                                            booksTable.Rows[j][BooksTable.indexWriter].ToString(),
                                            booksTable.Rows[j][BooksTable.indexGenre].ToString(),
                                            (int)booksTable.Rows[j][BooksTable.indexPrintingNumber],
                                            (int)booksTable.Rows[j][BooksTable.indexCount]);
                                borrowedBooks.Add(book);
                            }
                            break;
                        }
                    }
                }

                if (usersInfosTable.Rows[i][UsersInfosTable.indexBook4].ToString() != "")
                {
                    for (int j = 0; j < booksTable.Rows.Count; j++)
                    {
                        if (usersInfosTable.Rows[i][UsersInfosTable.indexBook4].ToString() == booksTable.Rows[j][BooksTable.indexName].ToString())
                        {
                            if (!bookExistInList(borrowedBooks, booksTable.Rows[j][BooksTable.indexName].ToString()))
                            {
                                Book book = new Book(
                                            booksTable.Rows[j][BooksTable.indexName].ToString(),
                                            booksTable.Rows[j][BooksTable.indexWriter].ToString(),
                                            booksTable.Rows[j][BooksTable.indexGenre].ToString(),
                                            (int)booksTable.Rows[j][BooksTable.indexPrintingNumber],
                                            (int)booksTable.Rows[j][BooksTable.indexCount]);
                                borrowedBooks.Add(book);
                            }
                            break;
                        }
                    }
                }

                if (usersInfosTable.Rows[i][UsersInfosTable.indexBook5].ToString() != "")
                {
                    for (int j = 0; j < booksTable.Rows.Count; j++)
                    {
                        if (usersInfosTable.Rows[i][UsersInfosTable.indexBook5].ToString() == booksTable.Rows[j][BooksTable.indexName].ToString())
                        {
                            if (!bookExistInList(borrowedBooks, booksTable.Rows[j][BooksTable.indexName].ToString()))
                            {
                                Book book = new Book(
                                            booksTable.Rows[j][BooksTable.indexName].ToString(),
                                            booksTable.Rows[j][BooksTable.indexWriter].ToString(),
                                            booksTable.Rows[j][BooksTable.indexGenre].ToString(),
                                            (int)booksTable.Rows[j][BooksTable.indexPrintingNumber],
                                            (int)booksTable.Rows[j][BooksTable.indexCount]);
                                borrowedBooks.Add(book);
                            }
                            break;
                        }
                    }
                }
            }

            return borrowedBooks;
        }

        private bool bookExistInList(ObservableCollection<Book> books, string bookName)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (bookName == books[i].name)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// ketabaye mojood ro barmigardoone
        /// </summary>
        /// <returns></returns>
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
        /// kol karbaraye az noe user ro bar migardoone
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<User> showAllUsers()
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
        public ObservableCollection<User> showDelayedReturners()
        {
            DataTable peopleData = PeopleTable.read();
            DataTable usersInfosData = UsersInfosTable.read();

            ObservableCollection<User> users = new ObservableCollection<User>();

            for (int i = 0; i < peopleData.Rows.Count; i++)
            {
                if (peopleData.Rows[i][PeopleTable.indexRole].ToString() == Role.User.ToString())
                {
                    for (int j = 0; j < usersInfosData.Rows.Count; j++)
                    {
                        if (peopleData.Rows[i][PeopleTable.indexUserName].ToString() == usersInfosData.Rows[j][UsersInfosTable.indexUserName].ToString())
                        {
                            int compare1 = 0;
                            int compare2 = 0;
                            int compare3 = 0;
                            int compare4 = 0;
                            int compare5 = 0;

                            if (usersInfosData.Rows[j][UsersInfosTable.indexBook1].ToString() != "")
                            {
                                compare1 = DateTime.Compare((DateTime)usersInfosData.Rows[j][UsersInfosTable.indexExpireDate1], DateTime.Today);
                            }

                            if (usersInfosData.Rows[j][UsersInfosTable.indexBook2].ToString() != "")
                            {
                                compare2 = DateTime.Compare((DateTime)usersInfosData.Rows[j][UsersInfosTable.indexExpireDate2], DateTime.Today);
                            }

                            if (usersInfosData.Rows[j][UsersInfosTable.indexBook3].ToString() != "")
                            {
                                compare3 = DateTime.Compare((DateTime)usersInfosData.Rows[j][UsersInfosTable.indexExpireDate3], DateTime.Today);
                            }

                            if (usersInfosData.Rows[j][UsersInfosTable.indexBook1].ToString() != "")
                            {
                                compare4 = DateTime.Compare((DateTime)usersInfosData.Rows[j][UsersInfosTable.indexExpireDate4], DateTime.Today);
                            }

                            if (usersInfosData.Rows[j][UsersInfosTable.indexBook1].ToString() != "")
                            {
                                compare5 = DateTime.Compare((DateTime)usersInfosData.Rows[j][UsersInfosTable.indexExpireDate5], DateTime.Today);
                            }


                            if (compare1 < 0 || compare2 < 0 || compare3 < 0 || compare4 < 0 || compare5 < 0)
                            {
                                User user = new User(peopleData.Rows[i][PeopleTable.indexUserName].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexFirstName].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexLastName].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexPhoneNumber].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexEmail].ToString(),
                                                        peopleData.Rows[i][PeopleTable.indexPassword].ToString(),
                                                        (double)peopleData.Rows[i][PeopleTable.indexMoneyBag],
                                                        peopleData.Rows[i][PeopleTable.indexImageAddress].ToString(),
                                                        (DateTime)usersInfosData.Rows[j][UsersInfosTable.indexSubRegisterDate],
                                                        (DateTime)usersInfosData.Rows[j][UsersInfosTable.indexSubRenewalDate],
                                                        (DateTime)usersInfosData.Rows[j][UsersInfosTable.indexSubExpireDate]);
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
        public ObservableCollection<User> showDelaySubUsers()
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
        public User showUserInfo(string userName)
        {
            DataTable people = PeopleTable.read();

            for (int i = 0; i < people.Rows.Count; i++)
            {
                if (people.Rows[i][PeopleTable.indexUserName].ToString() == userName)
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

        public void editInfo(Employee employee)
        {
            //same usernames
            PeopleTable.update(this.userName, employee);
            this.firstName = employee.firstName;
            this.lastName = employee.lastName;
            this.phoneNumber = employee.phoneNumber;
            this.email = employee.email;
            this.imageAddress = employee.imageAddress;
        }

        public void removeUser(User user)
        {
            //delete from People Table
            SqlConnection sqlConnection = new SqlConnection(projectInfo.connectionString);
            sqlConnection.Open();
            string command = "delete from People where userName = '" + user.userName + "'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.BeginExecuteNonQuery();
            sqlConnection.delayedClose();

            //update Books Table
            DataTable dataTable = UsersInfosTable.read();
            List<string> bookNames = new List<string>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][UsersInfosTable.indexUserName].ToString() == user.userName)
                {
                    for(int j = 1; j < 10; j += 2)
                    {
                        if (dataTable.Rows[i][j].ToString() != "" &&
                            dataTable.Rows[i][j] != null)
                        {
                            bookNames.Add(dataTable.Rows[i][j].ToString());
                        }
                    }
                    break;
                }
            }

            for(int i = 0; i < bookNames.Count;i++)
            {
                SqlConnection sqlConnection1 = new SqlConnection(projectInfo.connectionString);
                sqlConnection1.Open();
                string command1 = "update Books SET count = '" + (bookCount(bookNames[i]) + 1) + "' where name ='" + bookNames[i] + "' ";
                SqlCommand sqlCommand1 = new SqlCommand(command1, sqlConnection1);
                sqlCommand1.BeginExecuteNonQuery();
                sqlConnection1.delayedClose();
            }

            //delete from UsersInfos Table
            SqlConnection sqlConnection2 = new SqlConnection(projectInfo.connectionString);
            sqlConnection2.Open();
            string command2 = "delete from UsersInfos where userName = '" + user.userName + "'";
            SqlCommand sqlCommand2 = new SqlCommand(command2, sqlConnection2);
            sqlCommand2.BeginExecuteNonQuery();
            sqlConnection2.delayedClose();
        }

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
    }

}
