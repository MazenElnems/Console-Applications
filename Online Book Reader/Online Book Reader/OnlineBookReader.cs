using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader
{
    class OnlineBookReader
    {
        User currentUser = new User();   
        private UserManger userManger = new UserManger();
        private BookManger bookManger = new BookManger();

        public OnlineBookReader()
        {
            userManger.LoadUsers();
            bookManger.LoadBooks();
        }

        private void AdminPage()
        {
            Console.Clear();
            Console.WriteLine(" ======== Admin Page ======== ");

            Console.WriteLine("1:Promote User\n" +
                              "2:Delete User\n" +
                              "3:Show All Users\n" +
                              "4:Search By Username\n" +
                              "5:Search By User Id\n" +
                              "6:Add Book\n" +
                              "7:Delete Book\n" +
                              "8:Exit");

            int choice = Helper.GetIntegerInput(1, 8);

            switch(choice)
            {
                case 1:
                    userManger.PromoteUser();
                    break;
                case 2:
                    userManger.DeleteUser();
                    break;
                case 3:
                    userManger.ShowListOfUsers();
                    break;
                case 4:
                    userManger.SearchByUserName();
                    break;
                case 5:
                    userManger.SearchById();
                    break;
                case 6:
                    bookManger.AddBook();
                    break;
                case 7:
                    bookManger.DeleteBook();
                    break;
                case 8:
                    return;
            }

            AdminPage();
        }

        private void CustomerPage()
        {
            Console.Clear();
            Console.WriteLine(" ======== Customer Page ======== ");

            Console.WriteLine("1:Read Book\n" +
                              "2:Read From History Sessions\n" +
                              "3:Exit");


            int choice = Helper.GetIntegerInput(1, 3);

            switch (choice) 
            {
                case 1:
                    bookManger.ReadBook(currentUser);
                    break;
                case 2:
                    bookManger.ReadFromHistorySessions(currentUser);
                    break;
                case 3:
                    return;
            }
            CustomerPage();
        }

        private void sinup()
        {
            Console.Clear();
            Console.WriteLine(" ======== Sinup Page ======== ");

            string username;
            do { Console.Write("Enter Your Username Or -1 To Exit: "); username = Console.ReadLine(); }
            while (String.IsNullOrEmpty(username));

            if (userManger.Equals("-1")) return;

            string password;
            do { Console.Write("Enter Your Password Or -1 To Exit: "); password = Console.ReadLine(); }
            while (String.IsNullOrEmpty(username));

            if (password.Equals("-1")) return;

            Console.Write("Enter Your Email: ");
            Console.ReadLine();

            userManger.AddUser(new User() { IsAdmin = false, Name = username , Password = password});

            login();
        }

        private void login()
        {
            Console.Clear();
            Console.WriteLine(" ======== Login Page ======== ");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (userManger.UserExists(username, password,out currentUser))
            {
                if(currentUser.IsAdmin) AdminPage();
                else CustomerPage();
            }
            else
            {
                Console.WriteLine("Invalid Username or Password!");
                Console.Write("Try Again (Y/N): ");
                string answer = Console.ReadLine();
                if (answer.ToLower().Equals("y") || answer.ToLower().Equals("yes")) login();
            }
        }


        public void Run()
        {
            Console.Clear();
            Console.WriteLine(" ======== Welcome In Online Book Reader ======== ");

            Console.WriteLine("1:login\n2:sinup\n3:exit");
            int choice = Helper.GetIntegerInput(1,3);

            switch (choice)
            {
                case 1:
                    login();
                    break;
                case 2:
                    sinup();
                    break;
                case 3:
                    userManger.SaveUsers();
                    bookManger.SaveBooks();
                    return;
            }

            Run();

        }
    }
}
