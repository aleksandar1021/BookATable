using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookATable.Implementation.UseCases.Commands.Restaurants
{
    public class EfCreateRestaurantCommand : EfUseCase, ICreateRestaurantCommand
    {
        private CreateRestaurantValidator _validator;
        private IApplicationActor _actor;
        public EfCreateRestaurantCommand(Context context, CreateRestaurantValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 34;

        public string Name => "Create restaurant";

        public void Execute(CreateRestaurantDTO data)
        {
            _validator.ValidateAndThrow(data);

            

            Restaurant restaurant = new Restaurant
            {
                Name = data.Name,
                WorkFromHour = data.WorkFromHour,
                WorkUntilHour = data.WorkUntilHour,
                WorkFromMinute = data.WorkFromMinute,
                WorkUntilMinute = data.WorkUntilMinute,
                RestaurantTypeId = data.RestaurantTypeId,
                Description = data.Description,
                MaxNumberOfGuests = data.MaxNumberOfGuests,
                TimeInterval = data.TimeInterval,
                UserId = _actor.Id,
                MealCategoryRestaurants = data.MealCategoriesRestaurants?.Select(x => new MealCategoryRestaurant
                {
                    MealCategoryId = x.MealCategoryId,
                }).ToList(),
                AppendiceRestaurants = data.Appendices?.Select(x => new AppendiceRestaurant
                {
                    AppendiceId = x.AppendiceId,
                }).ToList(),
                Address = new Address
                {
                    Description = data.AddressInput.Description,
                    CityId = data.AddressInput.CityId,
                    AddressOfPlace = data.AddressInput.Address,
                    Floor = data.AddressInput.Floor != null ? data.AddressInput.Floor : null,
                    Number = data.AddressInput.Number,
                    Place = data.AddressInput.Place
                }
            };

            if (data.RegularClosedDays != null)
            {
                restaurant.RegularClosedDays = data.RegularClosedDays.Select(x => new Domain.Tables.RegularClosedDays
                {
                    DayOfWeek = (Domain.Tables.DayOfWeek)x, 
                    Restaurant = restaurant
                }).ToList();
            }


            if (data.Images != null)
            {
                restaurant.RestaurantImages = data.Images.Select(i => new RestaurantImage
                    {
                        Restaurant = restaurant,
                        Path = i
                    }).ToList();

            foreach(var image in restaurant.RestaurantImages)
            {
                if(image.Path == data.PrimaryImagePath)
                {
                    image.IsPrimary = true;
                }
            }

            foreach (var image in data.Images)
            {
                var tempImageName = Path.Combine("wwwroot", "temp", image);
                var destinationFileName = Path.Combine("wwwroot", "restaurantPhotos", image);
                System.IO.File.Move(tempImageName, destinationFileName);
            }
            }

            Context.Restaurants.Add(restaurant);
            Context.SaveChanges();
        }
    }
}
