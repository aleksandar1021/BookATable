using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Dish : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public string Image { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        //public virtual ICollection<DishImage> DishImages { get; set; } = new HashSet<DishImage>();
    }
}
