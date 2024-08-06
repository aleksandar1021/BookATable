using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateSavedDTO
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
    }
    public class UpdateSavedDTO : CreateSavedDTO
    {
        public int Id { get; set; }
    }

    public class ResponseSavedDTO : UpdateSavedDTO
    {
        public BaseResponseRestaurantDTO Restaurant { get; set; }
        public ResponseBaseUser User { get; set; }
    }

    public class SearchSavedDTO : PagedSearch
    {
        public int? Id { get; set; }
        public int? RestaurantId { get; set; }
        public int? UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string RestaurantName { get; set; }
    }
}
