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
        public EfCreateRestaurantCommand(Context context, CreateRestaurantValidator validator) : base(context)
        {
            _validator = validator;
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
                AddressId = data.AddressId,
                RestaurantTypeId = data.RestaurantTypeId,
                Description = data.Description,
                MaxNumberOfGuests = data.MaxNumberOfGuests,
                TimeInterval = data.TimeInterval,
                UserId = data.UserId
            };

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

            Context.Restaurants.Add(restaurant);
            Context.SaveChanges();
        }
    }
}
