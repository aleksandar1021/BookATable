using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateReservationAppendiceDTO
    {
        public List<int> AppendiceIds { get; set; }
        public int ReservationId { get; set; }
    }

    public class UpdateReservationAppendiceDTO : CreateReservationAppendiceDTO
    {
        public int Id { get;}
    }

    public class ResponseReservationAppendiceDTO 
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int AppendiceId { get; set; }
        public ResponseNamedEntityDTO Appendice { get; set; }
        public BaseResponseRestaurantDTO Restaurant { get; set; }
    }

    public class SearchReservationAppendiceDTO : PagedSearch
    {
        public int? Id { get; set; }
        public int? AppendiceId { get; set; }
        public int? ReservationId { get; set; }
        public string RestaurantName { get; set; }
        public string AppendiceName { get; set; }
    }
}
