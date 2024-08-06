using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateRestaurantImageDTO 
    {
        public int RestaurantId { get; set; }
        public string Path { get; set; }
    }

    public class UpdateRestaurantImageDTO : CreateRestaurantImageDTO
    {
        public int Id { get; set; }
    }

    public class ResponseRestaurantImageDTO : UpdateRestaurantImageDTO
    {
        public BaseResponseRestaurantDTO Restaurant { get; set; }
    }

    public class SearchRestaurantImageDTO : PagedSearch
    {
        public int? Id { get; set; }
        public int? RestaurantId { get; set; }
        public string Path { get; set; }
    }
}
