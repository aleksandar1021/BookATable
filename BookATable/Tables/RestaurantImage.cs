using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class RestaurantImage : Entity
    {
        public int RestaurantId { get; set; }
        public string Path { get; set; }
        public bool? IsPrimary { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
