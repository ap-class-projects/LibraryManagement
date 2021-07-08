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
    }
}
