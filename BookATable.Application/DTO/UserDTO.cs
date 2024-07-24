using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class UserResultDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public bool IsActivatedUser { get; set; }

        public IEnumerable<RestaurantsDTO> Restaurants { get; set;}
        public IEnumerable<RatingDTO> Ratings { get; set; }
        public IEnumerable<ReservationDTO> Reservations { get; set; }
    }

    public class RatingDTO
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
    }

    public class ReservationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int NumberOfGuests { get; set; }
        public int TimeHour { get; set; }
        public int TimeMinute { get; set; }
        public string Note { get; set; }
        public DateOnly Date { get; set; }
        public int ReservationNumber { get; set; }
        public string ReservationCode { get; set; }
        public bool IsRealised { get; set; }
    }
        public class RestaurantsDTO
    {
        public int Id { get; set; }
        public int WorkFromHour { get; set; }
        public int WorkUntilHour { get; set; }
        public int WorkFromMinute { get; set; }
        public int WorkUntilMinute { get; set; }
        public AddressDTO Address { get; set; }
        public int UserId { get; set; }
        public string RestaurantType { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int TimeInterval { get; set; }
    }

    public class AddressDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string Number { get; set; }
        public int? Floor { get; set; }
    }



    public class SearchUserDTO : PagedSearch
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActivatedUser { get; set; }
    }
}
