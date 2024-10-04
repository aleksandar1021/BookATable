using Azure.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateContactDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class SearchContactDTO : PagedSearch
    {
        public int? Id { get; set; }
        public string Keyword { get; set; }
    }

    public class ResponseContactDTO : CreateContactDTO
    {
        public int Id { get; set; }
    }
}
