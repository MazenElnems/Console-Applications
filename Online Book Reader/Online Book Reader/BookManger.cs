using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader
{
    internal class BookManger
    {
        private Dictionary<int, Book> books = new Dictionary<int, Book>();

        public void LoadBooks()
        {
            List<Book> booksList = ExcelHandling.ReadBooks();
            foreach (Book book in booksList)
            {
                books[book.ISBN] = book;
            }
        }
        public void SaveBooks() 
        {
            ExcelHandling.WriteBooks(new List<Book>(books.Values),true);
        }

        public void AddBook()
        {
            Console.Clear();
            Console.WriteLine(" ======== Add Book ======== ");

            int isbn = GetISBN();
            string bookName = Helper.GetStringFromConsole("Book Name");
            string authorName = Helper.GetStringFromConsole("Author Name");

            int pageCount = -1;
            do { Console.Write("Enter Page Count (1-500): "); }
            while (!Int32.TryParse(Console.ReadLine(),out pageCount) && Helper.IsNumberInRange(pageCount,1,500));

            List<string> pages = new List<string>();
            for (int i = 0; i < pageCount; i++) 
            {
                Console.Write($"Enter Page {i+1}:- \n\t");
                pages.Add(Console.ReadLine());
            }

            books.Add(isbn,new Book() { ISBN = isbn, BookName = bookName, Author = authorName, PageCount = pageCount, Pages = pages });
            Console.WriteLine("Book Added Succussfully");
            Console.ReadLine();
        }



        public void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine(" ======== Delete Book ======== ");

            int isbn = GetISBN();
            if (books.ContainsKey(isbn)) 
            {
                books.Remove(isbn);
                Console.WriteLine("Book Is Deleted Succussfully");
            }
            else
            {
                Console.WriteLine("There Is No Book With This ISBN!");
            }
            Console.ReadLine();
        }

        public void ShowAllSystemBooks()
        {
            int i = 1;
            foreach (var book in books.Values)
            {
                Console.WriteLine($"{i}: "+ book);
                ++i;
            }
        }

        public void ReadBook(User user)
        {
            Console.Clear();
            Console.WriteLine(" ======== Read Book ======== ");

            ShowAllSystemBooks();
            int choice = Helper.GetIntegerInput(1, books.Count);

            openSession(new List<Book>(books.Values)[choice - 1],user,1);
        }
        
        public void ReadFromHistorySessions(User user)
        {
            Console.Clear();
            Console.WriteLine(" ======== History Sessions ======== ");

            bool hasSessions = false;

            int i = 1;
            foreach (var session in user.sessions)
            {
                Console.WriteLine($"{i}: " + session);
                hasSessions = true;
                ++i; 
            }

            if (!hasSessions)
            {
                Console.WriteLine("There Is No Sessions For You!");
            }
            else
            {
                int choice = Helper.GetIntegerInput(1,user.sessions.Count);
                openSession(user.sessions[choice - 1].Book ,user, user.sessions[choice - 1].PageNo);
            }            
        }

        private void openSession(Book book,User user,int PageNo)
        {
            Console.Clear();
            Console.WriteLine($" ======== Reading {book.BookName} ======== ");

            Session session;

            int pageIndex = PageNo;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(book.Pages[pageIndex-1]);

                Console.WriteLine("\n\t1:Next Page\n" +
                                  "\t2:Previous Page\n" +
                                  "\t3:Exit");

                int choice = Helper.GetIntegerInput(1, 3);

                switch (choice) 
                {
                    case 1:
                        pageIndex++;
                        pageIndex = pageIndex == book.Pages.Count + 1 ? book.Pages.Count : pageIndex;
                        break;
                    case 2:
                        pageIndex--;
                        pageIndex = pageIndex < 1 ? 1 : pageIndex;
                        break;
                    case 3:
                        session = new Session() { Book = book, PageNo = pageIndex, LastReadTime = DateTime.Now };
                        user.sessions.Add(session);
                        return;
                }
            }
        }

        private int GetISBN()
        {
            int isbn;
            do { Console.Write("Enter ISBN: "); }
            while (!Int32.TryParse(Console.ReadLine(), out isbn) && books.ContainsKey(isbn));

            return isbn;
        }   
    }
}
