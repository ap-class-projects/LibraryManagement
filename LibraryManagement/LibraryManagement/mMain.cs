using LibraryManagement.Classes;
using System;
using System.IO;

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
    public delegate void ChangeAdminPageToIncreaseBudgetPage(Admin admin, double increaseMoney);
    public delegate void ChangeEmployeePageToUserInfoPage(Employee employee, User user);

    public static class projectInfo
    {
        public static string connectionString;
        static projectInfo()
        {
            string path = Environment.CurrentDirectory + @"\..\..\Database\db.mdf";
            FileInfo DbFile = new FileInfo(path);
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DbFile.FullName + ";Integrated Security=True;Connect Timeout=30";
        }
    }
}
