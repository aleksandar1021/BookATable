using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.RegularClosedDays;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.RegularClosedDays
{
    public class EfCreateRegularClosedDaysCommand : EfUseCase, ICreateRegularClosedDaysCommand
    {
        private RegularDaysValidator _validator;
        public EfCreateRegularClosedDaysCommand(Context context, RegularDaysValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 75;

        public string Name => "Create regular closed days for restaurant";

        public void Execute(CreateRegularClosedDaysDTO data)
        {
            _validator.ValidateAndThrow(data);

            List<Domain.Tables.RegularClosedDays> days = new List<Domain.Tables.RegularClosedDays>();

            foreach(var day in data.Days)
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
