using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader
{
    class User
    {
        public List<Session> sessions  = new List<Session>();
        public string Id { get; }
        public string? Name { get; set; }
        public string? Password {  get; set; }
        public bool IsAdmin { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public override string ToString()
        {
            return $"User Id: {Id} | User Name: {Name}\t\t({(IsAdmin ? "Admin": "Customer")})";
        }

    }
}
