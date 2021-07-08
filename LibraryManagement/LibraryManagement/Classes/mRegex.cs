using System.Text.RegularExpressions;

namespace LibraryManagement.Classes
{
    static class mRegex
    {
        public static bool userNameIsValid(string userName)
        {
            string pattern = @"^[a-zA-Z][a-zA-Z\d]{2,31}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(userName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool nameIsValid(string name)
        {
            string pattern = @"^[a-zA-Z][a-zA-Z]{1,30}[a-zA-Z]$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool emailIsValid(string email)
        {
            string pattern = @"^[a-zA-Z][a-zA-Z\d_-]{0,31}@[a-zA-Z][a-zA-Z\d]{0,7}\.[a-zA-Z]{0,2}[a-zA-Z]$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool phoneNumberIsValid(string phoneNumber)
        {
            string pattern = @"^09\d{8}\d$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(phoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool passwordIsValid(string password)
        {
            //(?=.*[a-z]) The string must contain at least 1 lowercase alphabetical character
            //(?=.*[A-Z]) The string must contain at least 1 uppercase alphabetical character
            //(?=.*[0-9]) The string must contain at least 1 numeric character
            string pattern = @"^(?=.*[A-Z])[a-zA-Z\d]{8,32}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool cvvIsValid(string cvv)
        {
            string pattern = @"^\d{3,4}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(cvv))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
