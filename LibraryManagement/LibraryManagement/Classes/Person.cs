namespace LibraryManagement.Classes
{
    abstract class Person
    {
        public string userName { get; }
        public string firstName { get; }
        public string lastName { get; }
        public Role role { get; }
        public string phoneNumber { get; }
        public string email { get; }
        public string password { get; }

        public Person(string userName, string firstName, string lastName, 
                      Role role, string phoneNumber, string email, string password)
        {
            this.userName = userName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.role = role;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
        }
    }
}
