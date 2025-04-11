using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class UseCaseLog
    {
        public string Username { get; set; }
        public string UseCaseName { get; set; }
        public object UseCaseData { get; set; }
    }
}
