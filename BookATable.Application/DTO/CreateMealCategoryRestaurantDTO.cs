using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateMealCategoryRestaurantDTO
    {
        public int MealCategoryId { get; set; }
        public int RestaurantId { get; set; }
    }

    public class UpdateMealCategoryRestaurantDTO : CreateMealCategoryRestaurantDTO
    {
        public int Id { get; set; }
    }

    public class SearchMealCategoryRestaurantDTO  : PagedSearch
    {
        public int? Id { get; set; }
        public int? MealCategoryId { get; set; }
        public int? RestaurantId { get; set; }
    }

    public class ResponseMealCategoryRestaurantDTO
    {
        public int Id { get; set; }
        public int MealCategoryId { get; set; }
        public int RestaurantId { get; set; }
        public ResponseNamedEntityDTO MealCategory { get; set; }
        public ResponseRestaurantForMealCategoryRestaurantDTO Restaurant { get; set; }

    }
    public class ResponseRestaurantForMealCategoryRestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }

    }
}
