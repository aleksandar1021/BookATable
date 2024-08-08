using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Restaurants
{
    public class EfGetRestuarantForUserQuery : EfUseCase, IGetRestaurantForUserQuery
    {
        private IApplicationActor _actor;
        public EfGetRestuarantForUserQuery(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 74;

        public string Name => "Find restaurant for user by Id";

        public ResponseRestaurantForUserDTO Execute(int data)
        {
            Restaurant r = Context.Restaurants.FirstOrDefault(x => x.Id == data);

            if (r == null || !r.IsActive)
            {
                throw new NotFoundException(nameof(Restaurant), data);
            }


            return new ResponseRestaurantForUserDTO
            {
                Id = r.Id,
                Name = r.Name,
                WorkFromHour = r.WorkFromHour,
                WorkUntilHour = r.WorkUntilHour,
                WorkFromMinute = r.WorkFromMinute,
                WorkUntilMinute = r.WorkUntilMinute,
                AddressId = r.AddressId,
                UserId = r.UserId,
                RestaurantTypeId = r.RestaurantTypeId,
                Description = r.Description,
                MaxNumberOfGuests = r.MaxNumberOfGuests,
                TimeInterval = r.TimeInterval,
                IsActivated = r.IsActivated,
                Images = r.RestaurantImages.Select(img => img.Path),
                Reservations = r.Reservations
                        .Where(r => r.IsActive)
                        .Select(r => new ReservationDTO
                        {
                            Id = r.Id,
                            NumberOfGuests = r.NumberOfGuests,
                            IsRealised = r.IsRealised,
                            Date = r.Date,
                            Note = r.Note,
                            ReservationCode = r.ReservationCode,
                            RestaurantId = r.RestaurantId,
                            TimeHour = r.TimeHour,
                            TimeMinute = r.TimeMinute,
                            UserId = r.UserId
                        }),
                Ratings = r.Ratings
                        .Where(r => r.IsActive)
                        .Select(r => new RatingDTO
                        {
                            Id = r.Id,
                            Rate = r.Rate,
                            RestaurantId = r.RestaurantId,
                            UserId = r.UserId
                        }),
                MealCategories = r.MealCategoryRestaurants
                        .Where(m => m.IsActive)
                        .Select(m => new ResponseMealCategoryForRestaurant
                        {
                            Id = m.Id,
                            Name = m.MealCategory.Name
                        }),
                RestaurantImages = r.RestaurantImages
                        .Where(img => img.IsActive)
                        .Select(img => new ResponseRestaurantImageForRestaurant
                        {
                            Id = img.Id,
                            Path = img.Path,
                            RestaurantId = img.RestaurantId,
                            IsPrimary = img.IsPrimary
                        }),
                Dishes = r.Dishs
                        .Where(d => d.IsActive)
                        .Select(d => new ResponseDishForRestaurant
                        {
                            Id = d.Id,
                            Name = d.Name,
                            RestaurantId = d.RestaurantId,
                            Description = d.Description,
                            Price = d.Price
                        }),
                AppendiceRestaurants = r.AppendiceRestaurants
                        .Where(a => a.IsActive)
                        .Select(a => new ResponseAppendiceRestaurantForRestaurant
                        {
                            Id = a.Id,
                            Name = a.Appendice.Name
                        }),
                IsSaved = r.Saved.Any(s => s.RestaurantId == r.Id && _actor.Id == s.UserId && s.IsActive),
                Rate = r.Ratings.Where(r => r.IsActive).Average(r => (decimal?)r.Rate) ?? 0,
                NumberOfRates = r.Ratings.Count(r => r.IsActive),
                RegularColsedDays = r.RegularClosedDays.Select(x => x.DayOfWeek.ToString()),
                SpecificColsedDays = r.SpecificClosedDays.Where(sc => sc.ClosedFrom > DateOnly.FromDateTime(DateTime.Now))
                                                         .Select(sc => new SpeceificClosedDays
                {
                    ClosedFrom = sc.ClosedFrom,
                    ClosedTo = sc.ClosedTo
                })
            };
            

        }

    }
}
