using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateRatingDTO
    {
        public int Rate { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public string Message { get; set; }
    }

    public class UpdateRatingDTO : CreateRatingDTO
    {
        public int Id { get; set; }
    }

    public class ResponseRatingDTO : UpdateRatingDTO
    {
        public ResponseBaseUser User { get; set; }
        public ResponseRestaurantForMealCategoryRestaurantDTO Restaurant { get; set; }
    }

    public class ResponseBaseUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }

    public class SearchRatingDTO : PagedSearch
    {
        public int? Id { get; set; }
        public int? Rate { get; set; }
        public int? UserId { get; set; }
        public int? RestaurantId { get; set; }
        public string Message { get; set; }
        public string RestaurantName { get; set; }
        public string UserName { get; set; }


    }

}
