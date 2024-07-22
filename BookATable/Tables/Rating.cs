using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Rating : Entity
    {
        public int Rate { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public string Message { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
