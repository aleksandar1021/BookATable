using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateRegularClosedDaysDTO
    {
        public int RestaurantId { get; set; }
        public IEnumerable<DayOfWeek> Days { get; set; }
    }

    public class ResponseRegularClosedDaysDTO
    {
        public int Id { get; set; }
        public BaseResponseRestaurantDTO Restaurant { get; set; }
        public DayOfWeek Day { get; set; }
    }

    public class SearchRegularClosedDays : PagedSearch
    {
        public int? Id { get; set; }
        public int? RestaurantId { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
