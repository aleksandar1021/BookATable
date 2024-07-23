using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class ActivateAccountDTO
    {
        public string Email { get; set; }
        public string ActivationCode { get; set; }
    }
}
