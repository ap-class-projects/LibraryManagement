using System.Data;

namespace LibraryManagement.Classes
{
    public class Admin : Person
    {
        public Admin(string userName, string firstName, string lastName,
             Role role, string phoneNumber, string email, string password, double moneyBag)
         : base(userName, firstName, lastName, role, phoneNumber, email, password, moneyBag)
        {

        }

        //public void addEmployee(User a)
        //{


        //}
        //public void removeEmployee(User a)
        //{

        //}
        //public void payEmployee(User a)
        //{

        //}

        public void addBook(Book book)
        {
            DataTable dataTable = BooksTable.read();
            bool exists = false;
            int count = 0;
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                if(dataTable.Rows[i][BooksTable.indexName].ToString() == book.name)
                {
                    exists = true;
                    count = int.Parse(dataTable.Rows[i][BooksTable.indexcount].ToString());
                    break;
                }
            }

            if(exists)
            {
                book.count = count + 1;
                BooksTable.update(book.name, book);
            }
            else
            {
                BooksTable.write(book);
            }
        }

        //public void seeBook()
        //{

        //}
        //public void seeBank()
        //{

        //}
        //public void increaseBank()
        //{

        //}

    }
}
