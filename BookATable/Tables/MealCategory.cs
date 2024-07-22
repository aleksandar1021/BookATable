using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class MealCategory : NamedEntity
    {
        public virtual ICollection<MealCategoryRestaurant> MealCategoryRestaurants { get; set; } = new HashSet<MealCategoryRestaurant>();
    }
}
