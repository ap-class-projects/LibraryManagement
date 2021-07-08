namespace LibraryManagement.Classes
{
    class Employee : Person
    {
        public Employee(string userName, string firstName, string lastName,
            Role role, string phoneNumber, string email, string password)
        : base(userName, firstName, lastName, role, phoneNumber, email, password)
        {

        }
    }
}
