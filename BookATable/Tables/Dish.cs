using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Dish : NamedEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<DishImage> DishImages { get; set; } = new HashSet<DishImage>();
    }
}
