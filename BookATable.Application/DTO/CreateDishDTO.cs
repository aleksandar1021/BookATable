using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateDishDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
    public class UpdateDishDTO : CreateDishDTO
    {
        public int Id { get; set; }
    }

    public class SearchDishDTO : PagedSearch
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RestaurantName { get; set; }

        public decimal? Price { get; set; }
        public int? RestaurantId { get; set; }
    }

    public class ResponseDishDTO 
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? RestaurantId { get; set; }
    }

}
