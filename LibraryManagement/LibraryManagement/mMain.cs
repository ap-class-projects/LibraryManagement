using LibraryManagement.Classes;

namespace LibraryManagement
{
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
