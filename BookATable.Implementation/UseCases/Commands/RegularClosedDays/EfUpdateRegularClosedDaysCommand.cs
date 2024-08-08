using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.RegularClosedDays;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.RegularClosedDays
{
    public class EfUpdateRegularClosedDaysCommand : EfUseCase, IUpdateRegularClosedDaysCommand
    {
        private RegularDaysValidator _validator;
        public EfUpdateRegularClosedDaysCommand(Context context, RegularDaysValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 76;

        public string Name => "Update regular days for restaurant";

        public void Execute(CreateRegularClosedDaysDTO data)
        {
            Restaurant targetRestaurant = Context.Restaurants.FirstOrDefault(x => x.Id == data.RestaurantId);
            if (targetRestaurant == null || !targetRestaurant.IsActive)
            {
                throw new NotFoundException(nameof(Restaurant), data.RestaurantId);
            }

            var oldDays = targetRestaurant.RegularClosedDays;

            Context.RegularClosedDays.RemoveRange(oldDays);
            Context.SaveChanges();

            _validator.ValidateAndThrow(data);

            List<Domain.Tables.RegularClosedDays> days = new List<Domain.Tables.RegularClosedDays>();

            foreach (var day in data.Days)
            {
                days.Add(new Domain.Tables.RegularClosedDays
                {
                    RestaurantId = data.RestaurantId,
                    DayOfWeek = (Domain.Tables.DayOfWeek)day
                });
            }

            Context.RegularClosedDays.AddRange(days);
            Context.SaveChanges();
        }
    }
}
