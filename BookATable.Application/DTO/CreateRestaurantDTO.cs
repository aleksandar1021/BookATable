using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public virtual List<string> Images { get; set; }
        public string PrimaryImagePath { get; set; }

        public List<CreateMealCategoryRestaurantDTO> MealCategoriesRestaurants { get; set; }
        public List<CreateAppendiceRestaurantDTO> Appendices { get; set; }
        public List<int> RegularClosedDays { get; set; }
        public CreateAddressDTO AddressInput { get; set; }
    }

    public class UpdateRestaurantDTO : CreateRestaurantDTO
    {
        public int Id { get; set; }
    }

    public class ResponseRestaurantDTO : UpdateRestaurantDTO
    {
        public string RestaurantType { get; set; }
        public bool IsActivated { get; set; }
        public IEnumerable<ReservationDTO> Reservations { get; set; }
        public IEnumerable<RatingDTO> Ratings { get; set; }
        public IEnumerable<ResponseMealCategoryForRestaurant> MealCategories { get; set; }
        public IEnumerable<ResponseRestaurantImageForRestaurant> RestaurantImages { get; set; }
        public IEnumerable<ResponseDishForRestaurant> Dishes { get; set; }
        public IEnumerable<ResponseAppendiceRestaurantForRestaurant> AppendiceRestaurants { get; set; }
        public IEnumerable<int> RegularColsedDays { get; set; }
        public IEnumerable<SpeceificClosedDays> SpecificColsedDays { get; set; }
        public ResponseBaseUser User { get; set; }
        public ResponseAddressDTO Address { get; set; }


    }

    public class ResponseRestaurantDTOAdmin
    {
        public int Id { get; set; }
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
        public virtual List<string> Images { get; set; }
        public string PrimaryImagePath { get; set; }
        public string RestaurantType { get; set; }
        public ResponseBaseUser User { get; set; }
        public bool IsActivated { get; set; }
        public ResponseAddressDTO Address { get; set; }


    }

    public class SpeceificClosedDays
    {
        public DateOnly ClosedFrom { get; set; }
        public DateOnly ClosedTo { get; set; }
    }

    public class ResponseRestaurantForUserDTO : ResponseRestaurantDTO
    {
        public bool IsSaved { get; set; }
        public decimal Rate { get; set; }
        public int NumberOfRates { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RestaurantType { get; set; }
        public ResponseBaseUser User { get; set; }
        public List<int> RegularClosedDaysInt { get; set; }
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
        public string Image { get; set; }
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
        public string Keyword { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? WorkFromHour { get; set; }
        public int? WorkUntilHour { get; set; }
        public int? WorkFromMinute { get; set; }
        public int? WorkUntilMinute { get; set; }
        public int? AddressId { get; set; }
        public int? MealCategoryId { get; set; }

        public int? UserId { get; set; }
        public int? RestaurantTypeId { get; set; }
        public string Description { get; set; }
        public int? MaxNumberOfGuests { get; set; }
        public int? TimeInterval { get; set; }
        public bool? IsActivated { get; set; }
        public List<SortBy> Sorts { get; set; } = new List<SortBy>();


    }
}
