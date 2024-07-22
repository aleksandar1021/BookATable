using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Domain.Tables
{
    public class Restaurant : NamedEntity
    {
        public int WorkFromHour { get; set; }
        public int WorkUntilHour { get; set; }
        public int WorkFromMinute { get; set; }
        public int WorkUntilMinute { get; set; }
        public int AddressId { get; set; }
        public int RestaurantTypeId { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int TimeInterval { get; set; }

        public virtual Address Address { get; set; }
        public virtual RestaurantType RestaurantType { get; set;}
        public virtual ICollection<MealCategoryRestaurant> MealCategoryRestaurants { get; set; } = new HashSet<MealCategoryRestaurant>();
        public virtual ICollection<RestaurantImage> RestaurantImages { get; set; } = new HashSet<RestaurantImage>();
        public virtual ICollection<Dish> Dishs { get; set; } = new HashSet<Dish>();
        public virtual ICollection<AppendiceRestaurant> AppendiceRestaurants { get; set; } = new HashSet<AppendiceRestaurant>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();


    }
}
