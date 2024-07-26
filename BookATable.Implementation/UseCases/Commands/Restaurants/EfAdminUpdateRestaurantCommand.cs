using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Restaurants
{
    public class EfAdminUpdateRestaurantCommand : EfUseCase, IAdminUpdateRestaurantCommand
    {
        private AdminUpdateRestourantValidator _validator;
        public EfAdminUpdateRestaurantCommand(Context context, AdminUpdateRestourantValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 36;

        public string Name => "Update restaurant (Admin)";

        public void Execute(UpdateRestaurantDTO data)
        {
            Restaurant restaurant = Context.Restaurants.FirstOrDefault(x => x.Id == data.Id);

            if (restaurant == null || !restaurant.IsActive)
            {
                throw new NotFoundException(nameof(Restaurant), data.Id);
            }

            _validator.ValidateAndThrow(data);

            restaurant.Name = data.Name;
            restaurant.WorkFromHour = data.WorkFromHour;
            restaurant.WorkUntilHour = data.WorkUntilHour;
            restaurant.WorkFromMinute = data.WorkFromMinute;
            restaurant.WorkUntilMinute = data.WorkUntilMinute;
            restaurant.AddressId = data.AddressId;
            restaurant.RestaurantTypeId = data.RestaurantTypeId;
            restaurant.Description = data.Description;
            restaurant.MaxNumberOfGuests = data.MaxNumberOfGuests;
            restaurant.TimeInterval = data.TimeInterval;
            restaurant.UserId = data.UserId;
            restaurant.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
