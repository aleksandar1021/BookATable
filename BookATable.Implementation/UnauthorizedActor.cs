using BookATable.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 38;

        public string Email => "/";

        public string Username => "unauthorized";

        public string FirstName => "unauthorized";

        public string LastName => "unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1, 2, 7, 8, 12, 13, 17, 18, 22, 23, 27, 28, 32, 33, 39, 40, 44, 45, 49, 50, 54, 55, 59, 60, 64, 65, 68, 69, 83, 84, 85, 73, 74 , 95, 96};
    }
}
