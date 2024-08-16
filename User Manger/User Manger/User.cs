using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class User
    {

        public string PhoneNo {  get; set; }
        public string Name {  get; set; }
        public string Email {  get; set; }

        public User(string name,string phonNo,string email) 
        { 
            Name = name;
            PhoneNo = phonNo;
            Email = email;
        }

        public override string ToString()
        {
            return $"Name: {Name,-15} | Phone Number: {PhoneNo,-15} | Email: {Email}";
        }
    }
}
