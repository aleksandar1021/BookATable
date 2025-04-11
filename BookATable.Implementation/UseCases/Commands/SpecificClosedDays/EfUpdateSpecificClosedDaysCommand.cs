using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.SpecificClosedDays;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.SpecificClosedDays
{
    public class EfUpdateSpecificClosedDaysCommand : EfUseCase, IUpdateSpecificClosedDaysCommand
    {
        private CreateSpecificClosedDaysValidator _validator;
        public EfUpdateSpecificClosedDaysCommand(Context context, CreateSpecificClosedDaysValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 81;

        public string Name => "Update specific closed days for restaurant";

        public void Execute(UpdateSpecificClosedDaysDTO data)
        {
            Domain.Tables.SpecificClosedDays sd = Context.SpecificClosedDays.FirstOrDefault(x => x.Id == data.Id);

            if (sd == null || !sd.IsActive)
            {
                throw new NotFoundException(nameof(Domain.Tables.SpecificClosedDays), data.Id);
            }

            sd.ClosedFrom = data.ClosedFrom;
            sd.ClosedTo = data.ClosedTo;

            Context.SaveChanges();
        }
    }
}
