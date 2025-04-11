using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookATable.Implementation.UseCases.Commands.Restaurants
{
    public class EfUpdateRestaurantCommand : EfUseCase, IUpdateRestaurantCommand
    {
        private IApplicationActor _actor;
        private UpdateRestaurantValidator _validator;
        public EfUpdateRestaurantCommand(Context context, UpdateRestaurantValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 35;

        public string Name => "Update restaurant";

        public void Execute(UpdateRestaurantDTO data)
        {
            Restaurant restaurant = Context.Restaurants
                                            .Include(r => r.Address) 
                                            .FirstOrDefault(x => x.Id == data.Id);

            if (restaurant == null || !restaurant.IsActive)
            {
                throw new NotFoundException(nameof(Restaurant), data.Id);
            }

            if (restaurant.UserId != _actor.Id)
            {
                throw new UnauthorizedAccessException();
            }

            _validator.ValidateAndThrow(data);

            restaurant.Name = data.Name;
            restaurant.WorkFromHour = data.WorkFromHour;
            restaurant.WorkUntilHour = data.WorkUntilHour;
            restaurant.WorkFromMinute = data.WorkFromMinute;
            restaurant.WorkUntilMinute = data.WorkUntilMinute;
            restaurant.RestaurantTypeId = data.RestaurantTypeId;
            restaurant.Description = data.Description;
            restaurant.MaxNumberOfGuests = data.MaxNumberOfGuests;
            restaurant.TimeInterval = data.TimeInterval;
            restaurant.UserId = _actor.Id;
            restaurant.UpdatedAt = DateTime.UtcNow;

            if (restaurant.Address != null)
            {
                restaurant.Address.CityId = data.AddressInput.CityId;
                restaurant.Address.AddressOfPlace = data.AddressInput.Address;
                restaurant.Address.Place = data.AddressInput.Place;
                restaurant.Address.Number = data.AddressInput.Number;
                restaurant.Address.Floor = data.AddressInput.Floor;
                restaurant.Address.Description = data.AddressInput.Description;
            }

            if (data.Images.Count() > 0)
            {
                var oldImages = restaurant.RestaurantImages.Select(i => i.Path).ToList();
                Context.RestaurantImages.RemoveRange(restaurant.RestaurantImages);

                restaurant.RestaurantImages = data.Images.Select(i => new RestaurantImage
                {
                    Restaurant = restaurant,
                    Path = i
                }).ToList();


                foreach (var image in data.Images)
                {
                    var tempImageName = Path.Combine("wwwroot", "temp", image);
                    var destinationFileName = Path.Combine("wwwroot", "restaurantPhotos", image);
                    System.IO.File.Move(tempImageName, destinationFileName);
                }




                foreach (var oldImage in oldImages)
                {
                    var oldImagePath = Path.Combine("wwwroot", "restaurantPhotos", oldImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                restaurant.RestaurantImages = data.Images.Select(i => new RestaurantImage
                {
                    Restaurant = restaurant,
                    Path = i
                }).ToList();

                foreach (var image in restaurant.RestaurantImages)
                {
                    if (image.Path == data.PrimaryImagePath)
                    {
                        image.IsPrimary = true;
                    }
                }
            }

            var oldClosedDays = restaurant.RegularClosedDays;
            Context.RegularClosedDays.RemoveRange(oldClosedDays);
            restaurant.RegularClosedDays = data.RegularClosedDays.Select(x => new Domain.Tables.RegularClosedDays
            {
                Restaurant = restaurant,
                DayOfWeek = (Domain.Tables.DayOfWeek)x
            }).ToList();

            var oldAppendices = restaurant.AppendiceRestaurants;
            Context.AppendiceRestaurants.RemoveRange(oldAppendices);
            restaurant.AppendiceRestaurants = data.Appendices.Select(x => new AppendiceRestaurant
            {
                Restaurant = restaurant,
                AppendiceId = x.AppendiceId
            }).ToList();

            var oldMealCategories = restaurant.MealCategoryRestaurants;
            Context.MealCategoryRestaurants.RemoveRange(oldMealCategories);
            restaurant.MealCategoryRestaurants = data.MealCategoriesRestaurants.Select(x => new MealCategoryRestaurant
            {
                Restaurant = restaurant,
                MealCategoryId = x.MealCategoryId
            }).ToList();

            

            Context.SaveChanges();

        }
    }
}
