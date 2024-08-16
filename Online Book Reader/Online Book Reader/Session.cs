using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader
{
    internal class Session
    {
        public int PageNo { get; set; }
        public Book Book { get; set; }
        public DateTime LastReadTime { get; set; }

        public override string ToString()
        {
            return $"{Book} | {LastReadTime}";
        }
    }
}
