﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class EmailDTO
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SendTo { get; set; }
    }

    public class UserEmailDTO : EmailDTO
    {
        public string Email { get; set; }
    }
}
