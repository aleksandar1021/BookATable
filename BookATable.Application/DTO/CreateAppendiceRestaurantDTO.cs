using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateAppendiceRestaurantDTO
    {
        public int AppendiceId { get; set; }
        public int RestaurantId { get; set; }
    }

    public class UpdateAppendiceRestaurantDTO : CreateAppendiceRestaurantDTO
    {
        public int Id { get; set; }
    }

    public class SearchAppendiceRestaurantDTO : PagedSearch
    {
        public int? Id { get; set; }
        public int? AppendiceId { get; set; }
        public int? RestaurantId { get; set; }
    }

    public class ResponseAppendiceRestaurantDTO : UpdateAppendiceRestaurantDTO
    {
        public ResponseNamedEntityDTO Appendice { get; set; }
        public BaseResponseRestaurantDTO Restaurant { get; set; }
    }
}
