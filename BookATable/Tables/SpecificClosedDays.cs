using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class SpecificClosedDays : Entity
    {
        public DateOnly ClosedFrom { get; set; }
        public DateOnly ClosedTo { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
