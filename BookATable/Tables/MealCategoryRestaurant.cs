using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class MealCategoryRestaurant : Entity
    {
        public int MealCategoryId { get; set; }
        public int RestaurantId { get; set; }

        public virtual MealCategory MealCategory { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
