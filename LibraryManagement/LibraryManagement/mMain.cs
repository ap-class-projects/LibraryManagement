using LibraryManagement.Classes;

namespace LibraryManagement
{
    // user sign up cost : 100
    // user monthly sub cost : 20
    // employee salary : 50 
    public enum Role
    {
        Admin,
        Employee,
        User,
        Unknown
    }

    public delegate void PageChangerNoArg();
    public delegate void PageChanger(Person person);
}
