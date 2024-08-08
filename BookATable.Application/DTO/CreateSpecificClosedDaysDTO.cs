using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateSpecificClosedDaysDTO
    {
        public int RestaurantId { get; set; }
        public DateOnly ClosedFrom { get; set; }
        public DateOnly ClosedTo { get; set; }
    }

    public class UpdateSpecificClosedDaysDTO : CreateSpecificClosedDaysDTO
    {
        public int Id { get; set; }
    }

    public class  ResponseSpecificClosedDays : UpdateSpecificClosedDaysDTO
    {
        public BaseResponseRestaurantDTO Restaurant { get; set; }
    }

    public class SearchSpecificClosedDays
    {
        public int? Id { get; set; }
        public int? RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public DateOnly? ClosedFrom { get; set; }
        public DateOnly? ClosedTo { get; set; }
    }
}
