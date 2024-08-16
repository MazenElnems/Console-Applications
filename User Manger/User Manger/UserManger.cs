using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class UserManger
    {
        private Dictionary<string, User> users;

        public UserManger()
        {
            users = new Dictionary<string, User>();
        }

        public void AddUser()
        {
            Console.WriteLine("\n\t========= AddUser =========\n");

            string userName = NameValidate();
            string email = EmailValidate();
            string phonNo = PhonNoValidate();

            users[phonNo] = new User(userName,phonNo,email);
        }

        public void DeleteUser() 
        {
            Console.WriteLine("\n\t========= DeleteUser =========\n");

            Console.Write("enter user phone number: ");
            string phonNo = Console.ReadLine();
            if (users.ContainsKey(phonNo))
            {
                users.Remove(phonNo);
                Console.WriteLine("user deleted successfully");
                return;
            }
            else
            {
                Console.WriteLine("no user has this phone number!!");
            }
        }

        public void UpdateUser()
        {
            Console.WriteLine("\n\t========= UpdateUser =========\n");

            Console.Write("enter user phone number: ");
            string phonNo = Console.ReadLine();
            if (users.ContainsKey(phonNo))
            {
                EditUserData(phonNo);
            }
            else
            {
                Console.WriteLine("no user has this phone number!!");
            }
        }

        public void ShowUsers()
        {
            Console.WriteLine("\n\t========= Users =========\n");

            int counter = 1;
            foreach (var user in users)
            {
                Console.WriteLine($"User {counter}  {user.Value}");
                counter++;
            }
        }

        private string NameValidate()
        {
            string userName = string.Empty;
            while (String.IsNullOrEmpty(userName))
            {
                Console.Write("User Name: ");
                userName = Console.ReadLine();
            }
            return userName;
        }

        private string PhonNoValidate()
        {
            string phonNo = string.Empty;
            while (String.IsNullOrEmpty(phonNo) || !phonNo.ValidNumber() || users.ContainsKey(phonNo))
            {
                if (users.ContainsKey(phonNo)) Console.WriteLine("make sure you enterd a unique phone number!!");
                Console.Write("Phone Number: ");
                phonNo = Console.ReadLine();
            }
            return phonNo;
        }

        private string EmailValidate()
        {
            string email = string.Empty;
            while (String.IsNullOrEmpty(email) || !email.ValidEmail())
            {
                if (!String.IsNullOrEmpty(email)) Console.WriteLine("make sure you enterd a valid email!!");
                Console.Write("Email: ");
                email = Console.ReadLine();
            }
            return email;
        }

        private void EditUserData(string phonNo)
        {
            while (true)
            {
                Console.WriteLine("\n1.Edit name");
                Console.WriteLine("2.Edit email");
                Console.WriteLine("3.Edit phone number");

                Console.Write("\nedit (1-3) or cancel -1 : ");
                int choice = Int32.Parse(Console.ReadLine());

                while (choice != -1 && !choice.IsNumberBetween(1, 3))
                {
                    Console.Write("\nedit (1-3) or cancel -1 : ");
                    choice = Int32.Parse(Console.ReadLine());
                }

                switch (choice)
                {
                    case -1:
                        return;
                    case 1:
                        string userName = NameValidate();
                        users[phonNo].Name = userName;
                        break;
                    case 2:
                        string email = EmailValidate();
                        users[phonNo].Email = email;
                        break;
                    case 3:
                        string phone = PhonNoValidate();
                        users[phonNo].PhoneNo = phone;
                        break;
                }
            }
        }

    }
}
