using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Online_Book_Reader
{
    static class ExcelHandling
    {
        // you must to edit the current working directory for the project.
        private const string path = @".\Data\data.xlsx";

        private static _Application excelApp = new Application();
        private static Workbook wb = excelApp.Workbooks.Open(path);
        private static Worksheet userSheet = wb.Worksheets[1];
        private static Worksheet bookSheet = wb.Worksheets[2];

        public static void WriteUsers(List<User> users, bool saveChanges)
        {

            for (int i = 0; i < users.Count; i++) 
            {
                userSheet.Cells[i+2,1].Value = users[i].Id;
                userSheet.Cells[i+2,2].Value = users[i].Name;
                userSheet.Cells[i+2,3].Value = users[i].Password;
                userSheet.Cells[i+2,4].Value = users[i].IsAdmin;

                int idx = 0;
                for (int j = 0; j < users[i].sessions.Count; j++) 
                {
                    userSheet.Cells[i+2, idx + 5].Value = users[i].sessions[j].Book.ISBN;
                    userSheet.Cells[i+2, idx + 6].Value = users[i].sessions[j].PageNo;
                    userSheet.Cells[i+2, idx + 7].Value = users[i].sessions[j].LastReadTime;
                    idx += 3;
                }
            }
            if(saveChanges)
                CloseAndSave();
        }

        public static List<User> ReadUsers()
        {
            List<User> users = new List<User>();

            int row = 2;
            while (userSheet.Cells[row, 1].Value != null)
            {
                string name = userSheet.Cells[row, 2].Value.ToString();
                string password = userSheet.Cells[row, 3].Value.ToString();
                bool isAdmin = userSheet.Cells[row, 4].Value;

                List<Session> sessions = new List<Session>();

                int idx = 0;
                while (userSheet.Cells[row,idx + 5].Value != null)
                {
                    int isbn = (int)userSheet.Cells[row, idx + 5].Value;
                    int pageNo = (int)userSheet.Cells[row, idx + 6].Value;
                    DateTime lastreadTime = userSheet.Cells[row, idx + 7].Value;
                    idx += 3;

                    Book book = ReadBooks().Find(b => b.ISBN == isbn);
                    if (book == null)
                    {
                        continue;
                    }
                    else
                    {
                        sessions.Add(new Session() { Book = book, PageNo = pageNo, LastReadTime = lastreadTime });
                    }
                    idx += 3;
                }

                users.Add(new User() { Name = name, Password = password, IsAdmin = isAdmin , sessions = sessions});

                row++;
            }

            return users;
        } 

        public static List<Book> ReadBooks()
        {
            List<Book> books = new List<Book>();
            
            int row = 2;
            while (bookSheet.Cells[row, 1].Value != null)
            {
                double isbn = bookSheet.Cells[row, 1].Value;
                double pageCount = bookSheet.Cells[row, 2].Value;
                string bookName = bookSheet.Cells[row, 3].Value.ToString();
                string author = bookSheet.Cells[row, 4].Value.ToString();

                Book book = new Book() { ISBN = (int)isbn,PageCount = (int)pageCount ,BookName = bookName, Author = author };

                int col = 5;
                while (bookSheet.Cells[row, col].Value != null)
                {
                    book?.Pages?.Add(bookSheet.Cells[row, col].Value.ToString());
                    col++;
                } 
                books.Add(book);
                row++;
            }

            return books;
        }

        public static void WriteBooks(List<Book> books, bool saveChanges)
        {
            for(int i=0; i<books.Count; i++)
            {
                bookSheet.Cells[i+2,1].Value = books[i].ISBN;
                bookSheet.Cells[i+2,2].Value = books[i].PageCount;
                bookSheet.Cells[i+2,3].Value = books[i].BookName;
                bookSheet.Cells[i+2,4].Value = books[i].Author;

                for(int j = 0;j < books[i].Pages?.Count; ++j)
                {
                    bookSheet.Cells[i + 2, j + 5].Value = books[i]?.Pages?[j];
                }
            }
            if(saveChanges)
                CloseAndSave();
        }
        
        public static void CloseAndSave()
        {

            wb.Save();
            Marshal.ReleaseComObject(userSheet);
            Marshal.ReleaseComObject(bookSheet);
            wb.Close();
            Marshal.ReleaseComObject(wb);
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);

            foreach (Process process in Process.GetProcessesByName("EXCEL"))
            {
                process.Kill();
            }

        }
    }

}
