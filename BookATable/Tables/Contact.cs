using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Contact : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
