using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateReservationDTO
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int NumberOfGuests { get; set; }
        public string Time {  get; set; }
        public string Note { get; set; }
        public DateOnly Date { get; set; }
        public IEnumerable<CreateReservationAppendice> Appendices { get; set; }
    }
    public class CreateReservationAppendice
    {
        public int RestaurantId { get; set; }
        public int AppendiceId { get; set; }

    }
    public class UpdateReservationDTO : CreateReservationDTO
    {
        public int Id { get; set; }
        public bool IsRealised { get; set; }
        public bool IsAccepted { get; set; }

    }

    public class SearchReservationDTO : PagedSearch
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? RestaurantId { get; set; }
        public int? NumberOfGuests { get; set; }
        public string Time { get; set; }
        public string Note { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string RestaurantName { get; set; }
        public List<SortBy> Sorts { get; set; } = new List<SortBy>();
        public string Keyword { get; set; }

    }

    public class ResponseReservationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int NumberOfGuests { get; set; }
        public string Time {  get; set; }
        public string Note { get; set; }
        public DateOnly Date { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRealised { get; set; }
        public ResponseBaseUser User { get; set; }
        public BaseResponseRestaurantDTO Restaurant { get; set; }
        public ICollection<ResponseNamedEntityDTO> Appendices { get; set; }
        public string Code { get; set; }
    }
}
