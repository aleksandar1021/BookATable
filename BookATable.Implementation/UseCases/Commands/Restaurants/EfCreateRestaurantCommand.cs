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

            Context.Restaurants.Add(restaurant);
            Context.SaveChanges();
        }
    }
}
