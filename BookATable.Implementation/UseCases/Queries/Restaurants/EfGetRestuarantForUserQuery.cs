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

            if (_actor.Id != r.UserId)
            {
                throw new UnauthorizedAccessException();
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
                RestaurantType = r.RestaurantType.Name,
                Address = new ResponseAddressDTO
                {
                    Address = r.Address.AddressOfPlace,
                    Description = r.Address.Description,
                    Place = r.Address.Place,
                    Latitude = r.Address.Latitude,
                    City = new ResponseCityDTO
                    {
                        Id = r.Address.City.Id,
                        Name = r.Address.City.Name,
                        ZipCode = r.Address.City.ZipCode
                    },
                    CityId = r.Address.City.Id,
                    Floor = r.Address.Floor,
                    Longitude = r.Address.Longitude,
                    Id = r.Address.Id,
                    Number = r.Address.Number
                },
                Images = r.RestaurantImages.Where(x => x.IsActive).Select(img => img.Path).ToList(),
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
                            Time = r.Time,
                            UserId = r.UserId
                        }),
                Ratings = r.Ratings
                        .Where(r => r.IsActive)
                        .Select(r => new RatingDTO
                        {
                            Id = r.Id,
                            Rate = r.Rate,
                            RestaurantId = r.RestaurantId,
                            UserId = r.UserId,
                            Message = r.Message,
                            User = new ResponseBaseUser
                            {
                                Id = r.User.Id,
                                Email = r.User.Email,
                                FirstName = r.User.FirstName,
                                LastName = r.User.LastName,
                                Image = r.User.Image
                            }
                        }),
                MealCategories = r.MealCategoryRestaurants
                        .Where(m => m.IsActive)
                        .Select(m => new ResponseMealCategoryForRestaurant
                        {
                            Id = m.MealCategory.Id,
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
                            Price = d.Price,
                            Image = d.Image
                        }),
                AppendiceRestaurants = r.AppendiceRestaurants
                        .Where(a => a.IsActive)
                        .Select(a => new ResponseAppendiceRestaurantForRestaurant
                        {
                            Id = a.Appendice.Id,
                            Name = a.Appendice.Name
                        }),
                IsSaved = r.Saved.Any(s => s.RestaurantId == r.Id && _actor.Id == s.UserId && s.IsActive),
                Rate = r.Ratings.Where(r => r.IsActive).Average(r => (decimal?)r.Rate) ?? 0,
                NumberOfRates = r.Ratings.Count(r => r.IsActive),
                RegularColsedDays = r.RegularClosedDays.Select(x => (int)x.DayOfWeek),
                RegularClosedDaysInt = r.RegularClosedDays.Select(x => (int)x.DayOfWeek).ToList(),
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
