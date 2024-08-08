using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Saved;
using BookATable.Application.UseCases.Commands.SpecificClosedDays;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.SpecificClosedDays
{
    public class EfCreateSpecificClosedDaysCommand : EfUseCase, ICreateSpecificClosedDaysCommand
    {
        private CreateSpecificClosedDaysValidator _validator;
        public EfCreateSpecificClosedDaysCommand(Context context, CreateSpecificClosedDaysValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 80;

        public string Name => "Create specific closed days for restaurant";

        public void Execute(CreateSpecificClosedDaysDTO data)
        {
            _validator.ValidateAndThrow(data);

            Domain.Tables.SpecificClosedDays spDays = new Domain.Tables.SpecificClosedDays
            {
                RestaurantId = data.RestaurantId,
                ClosedFrom = data.ClosedFrom,
                ClosedTo = data.ClosedTo
            };

            Context.SpecificClosedDays.Add(spDays);
            Context.SaveChanges();
        }
    }
}
