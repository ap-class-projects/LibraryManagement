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
    public delegate void ChangeUserPageToUserPaymentPage(User user, double money);

    public static class projectInfo
    {
        //yek user nmitavand az yek ketab 2 bar amanat bgirad

        public static string connectionString;
        public const double userSignUpCost = 100;
        //zaman sabt nam user be andaze yek mah esterak migirad
        public const double monthlySubCost = 30;
        public const double salaryPerEmployee = 50;
        public static DateTime dateTimeMinValue;
        public const int borrowBookDays = 30;

        static projectInfo()
        {
            string path = Environment.CurrentDirectory + @"\..\..\Database\db.mdf";
            FileInfo DbFile = new FileInfo(path);
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DbFile.FullName + ";Integrated Security=True;Connect Timeout=30";

            string temp = "1/1/1900 12:00:00 AM";
            dateTimeMinValue = DateTime.Parse(temp);
        }
    }
}
