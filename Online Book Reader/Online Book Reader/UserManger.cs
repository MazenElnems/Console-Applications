using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader
{
    class UserManger
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();

        public void LoadUsers()
        {
            List<User> usersList = ExcelHandling.ReadUsers();
            foreach (User user in usersList)
            {
                users[user.Id] = user;
            }
        }

        public void SaveUsers()
        {
            ExcelHandling.WriteUsers(new List<User>(users.Values),false);
        }

        public void PromoteUser()
        {
            Console.Clear();
            Console.WriteLine(" ======== Promote User ======== ");

            string id = Helper.GetStringFromConsole("User id");

            if (users.ContainsKey(id)) 
            {
                if (users[id].IsAdmin) Console.WriteLine("User Is Already Admin!");
                else 
                {
                    Console.WriteLine($"User {users[id].Name} Becomes Admin.");
                    users[id].IsAdmin = true;
                }
            }
            else
            {
                Console.WriteLine("No User With This Id!");
            }
            Console.ReadLine();
        }

        public void DeleteUser()
        {
            Console.Clear();
            Console.WriteLine(" ======== DeleteUser ======== ");

            string id = Helper.GetStringFromConsole("User id");

            if (users.ContainsKey(id)) 
            {
                if (users[id].IsAdmin)
                {
                    Console.WriteLine("Can't Delete Admin User!");
                }
                else
                {
                    Console.WriteLine($"User {users[id].Name} Deleted Succussfully");
                    users.Remove(id);
                }
            }
            else
            {
                Console.WriteLine("No User With This Id!");
            }
            Console.ReadLine();
        }

        public void AddUser(User user)
        {
            users.Add(user.Id, user);
        }

        public void ShowListOfUsers()
        {
            Console.Clear();
            Console.WriteLine(" ======== All System Users ======== ");

            foreach (User user in users.Values)
            {
                Console.WriteLine(user);
            }
            Console.ReadLine();
        }

        public void SearchByUserName()
        {
            Console.Clear();
            Console.WriteLine(" ======== Search By User Name ======== ");

            string username = Helper.GetStringFromConsole("User Name");

            bool found = false;

            foreach (var user in users.Values)
            {
                if (username.Equals(user.Name))
                {
                    Console.WriteLine(user);
                    found = true;
                }
            }

            if (!found) Console.WriteLine("There Is No Results!");
            Console.ReadLine();
        }

        public void SearchById()
        {
            Console.Clear();
            Console.WriteLine(" ======== Search By User Id ======== ");

            string id = Helper.GetStringFromConsole("id");

            if (users.ContainsKey(id))
            {
                Console.WriteLine(users[id]);
            }
            else
            {
                Console.WriteLine("There Is No Results!");
            }
            Console.ReadLine();
        }
        public bool UserExists(string username , string password, out User User) 
        {
            User = new User();
            foreach (var user in users.Values)
            {
                if (user.Name.Equals(username) && user.Password.Equals(password)) { User = user; return true; }
            }
            return false;
        }
    }
}
