using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class StringExtension
    {
        public static bool ValidNumber(this string Number)
        {
            if (!String.IsNullOrWhiteSpace(Number))
            {
                if (Number.Length >= 5)
                {
                    bool valid = true;
                    foreach (char digit in Number)
                    {
                        int asciiValue = digit;
                        if (!(asciiValue >= 48 && asciiValue <= 57)) { valid = false; break; }
                    }
                    if(valid)return true;
                    return false;
                }
                return false;
            }
            return false;
        }

        public static bool ValidEmail(this string Email)
        {
            string[] emailParts = Email.Split('@');
            
            if(emailParts.Length == 2)
            {
                string firstPart = emailParts[0];
                string domainPart = emailParts[1];

                if(!String.IsNullOrEmpty(domainPart) && !String.IsNullOrEmpty(firstPart) && domainPart.Equals("gmail.com"))
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

    }
}
