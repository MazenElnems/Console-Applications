using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader
{
    class Book
    {
        public int ISBN { get; set; }
        public string? BookName { get; set; }
        public string?  Author { get; set; }
        public List<string>? Pages { get; set; }
        public int PageCount { get; set; }

        public Book()
        {
            Pages = new List<string> ();
        }

        public override string ToString()
        {
            return $"ISBN: {ISBN} | Book Name: {BookName} | Author: {Author}";
        }
    }
}
