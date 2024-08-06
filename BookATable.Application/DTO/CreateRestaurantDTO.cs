using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.DTO
{
    public class CreateRestaurantDTO
    {
        public string Name { get; set; }
        public int WorkFromHour { get; set; }
        public int WorkUntilHour { get; set; }
        public int WorkFromMinute { get; set; }
        public int WorkUntilMinute { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int RestaurantTypeId { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int TimeInterval { get; set; }
        public virtual IEnumerable<string> Images { get; set; }
        public string PrimaryImagePath { get; set; }
    }

    public class UpdateRestaurantDTO : CreateRestaurantDTO
    {
        public int Id { get; set; }
    }

    public class ResponseRestaurantDTO : UpdateRestaurantDTO
    {
        public bool IsActivated { get; set; }
        public IEnumerable<ReservationDTO> Reservations { get; set; }
        public IEnumerable<RatingDTO> Ratings { get; set; }
        public IEnumerable<ResponseMealCategoryForRestaurant> MealCategories { get; set; }
        public IEnumerable<ResponseRestaurantImageForRestaurant> RestaurantImages { get; set; }
        public IEnumerable<ResponseDishForRestaurant> Dishes { get; set; }
        public IEnumerable<ResponseAppendiceRestaurantForRestaurant> AppendiceRestaurants { get; set; }

    }

 

    public class ResponseAppendiceRestaurantForRestaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ResponseDishForRestaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
    public class ResponseRestaurantImageForRestaurant
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Path { get; set; }
        public bool? IsPrimary { get; set; }
    }
    public class ResponseMealCategoryForRestaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
   

    public class SearchRestaurantDTO : PagedSearch
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? WorkFromHour { get; set; }
        public int? WorkUntilHour { get; set; }
        public int? WorkFromMinute { get; set; }
        public int? WorkUntilMinute { get; set; }
        public int? AddressId { get; set; }
        public int? UserId { get; set; }
        public int? RestaurantTypeId { get; set; }
        public string Description { get; set; }
        public int? MaxNumberOfGuests { get; set; }
        public int? TimeInterval { get; set; }
        public bool? IsActivated { get; set; }
    }
}
