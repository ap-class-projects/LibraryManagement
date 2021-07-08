namespace LibraryManagement.Classes
{
    class Admin : Person
    {
        public Admin(string userName, string firstName, string lastName,
             Role role, string phoneNumber, string email, string password, double moneyBag)
         : base(userName, firstName, lastName, role, phoneNumber, email, password, moneyBag)
            Role role, string phoneNumber, string email, string password,double moneyBag)
        : base(userName, firstName, lastName, role, phoneNumber, email, password,moneyBag)
        {

        }
        //public Admin(string name, string password)
        //{
        //Regex1 a = new Regex1();
        //bool checkname = a.namecheck(name);
        //if (checkname == true)
        //{
        //    this.name = name;
        //}
        //else
        //{
        //    MessageBox.Show("name has wrong format");
        //    //throw new Exception("name has wrong format");
        //}
        //Regex1 b = new Regex1();
        //bool checkpass = b.passwordcheck(password);
        //if (checkpass == true)
        //{
        //    this.password = password;
        //}
        //else
        //{
        //    MessageBox.Show("pass has wrong format");
        //    //throw new Exception("pass has wrong format");
        //}
        //}

        public void addEmployee(User a)
        {


        }
        public void removeEmployee(User a)
        {

        }
        public void payEmployee(User a)
        {

        }
        public void addBook()
        {

        }
        public void seeBook()
        {

        }
        public void seeBank()
        {

        }
        public void increaseBank()
        {

        }

    }
}
