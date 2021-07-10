using System;
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
            string pattern = @"^[a-zA-Z]{3,32}$";
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
            string pattern = @"^[a-zA-Z][a-zA-Z\d_-]{0,31}@[a-zA-Z][a-zA-Z\d]{0,7}\.[a-zA-Z]{1,3}$";
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
            string pattern = @"^09\d{9}$";
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

        public static bool cardIsValid(string cardNumber)
        {
            string pattern = @"^\d{16}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(cardNumber))
            {
                int[] numbers = new int[16];
                for(int i = 0; i < 16; i++)
                {
                    numbers[i] = int.Parse(cardNumber[i].ToString());
                }

                for(int i = 0; i < 16; i += 2)
                {
                    if( (numbers[i] * 2) > 9)
                    {
                        numbers[i] = (numbers[i] * 2) / 10 + (numbers[i] * 2) % 10;
                    }
                    else
                    {
                        numbers[i] = (numbers[i] * 2);
                    }
                }

                int sum = 0;
                for(int i = 0; i < 16; i++)
                {
                    sum += numbers[i];
                }

                if(sum%10 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// year : ^\d{4}$
        /// month : ^\d{2}$
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool expireDateIsValid(string year, string month)
        {
            string patternYear = @"^\d{4}$";
            string patternMonth = @"^\d{1,2}$";

            Regex regexYear = new Regex(patternYear);
            Regex regexMonth= new Regex(patternMonth);
            
            if (regexYear.IsMatch(year) && regexMonth.IsMatch(month))
            {
                if(int.Parse(month) > 11)
                {
                    return false;
                }

                if(int.Parse(year) < int.Parse(DateTime.Now.Year.ToString()))
                {
                    return false;
                }
                else if(int.Parse(year) == int.Parse(DateTime.Now.Year.ToString()))
                {
                    if (int.Parse(month) - int.Parse(DateTime.Now.Month.ToString()) >= 3)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // dar in halat hastim : int.Parse(year) > int.Parse(DateTime.Now.Year.ToString())
                    if (int.Parse(year) - 1 == int.Parse(DateTime.Now.Year.ToString()))
                    {
                        if (int.Parse(month) + ( 12 - int.Parse(DateTime.Now.Month.ToString()) ) >= 3)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
