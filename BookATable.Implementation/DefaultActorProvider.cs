using BookATable.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation
{
    public class DefaultActorProvider : IApplicationActorProvider
    {
        public IApplicationActor GetActor()
        {
            return new Actor
            {
                Email = "actor",
                Id = 0,
                FirstName = "Actor",
                LastName = "Actor"
            };
        }
    }
}
