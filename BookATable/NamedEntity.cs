using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}
