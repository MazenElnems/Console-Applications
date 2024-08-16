using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class System
    {
        private UserManger userManger;

        public System()
        {
            userManger = new UserManger();
        }

        public void Run()
        {
            int choice = Menue();

            switch (choice) 
            {
                case 1:
                    userManger.AddUser();
                    break;
                case 2:
                    userManger.DeleteUser();
                    break;
                case 3:
                    userManger.UpdateUser();
                    break;
                case 4:
                    userManger.ShowUsers();
                    break;
                case 5:
                    return;
            }
            Run();
        }

        private int Menue()
        {
            Console.WriteLine("\n1.Add User.");
            Console.WriteLine("2.Delete User.");
            Console.WriteLine("3.Update User.");
            Console.WriteLine("4.Shows All Users.");
            Console.WriteLine("5.Exit.");

            Console.Write("\nenter operation number (1-5): ");
            int choice = Int32.Parse(Console.ReadLine());

            while (!choice.IsNumberBetween(1, 5))
            {
                Console.Write("\nenter operation number (1-5): ");
                choice = Int32.Parse(Console.ReadLine());
            }

            return choice;
        }

    }
}
