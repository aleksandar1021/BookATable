using BookATable.Application.UseCases.Commands.SpecificClosedDays;
using BookATable.DataAccess;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.SpecificClosedDays
{
    public class EfDeleteSpecificClosedDaysCommand : EfUseCase, IDeleteSpecificClosedDaysCommand
    {
        public EfDeleteSpecificClosedDaysCommand(Context context) : base(context)
        {
        }

        public int Id => 82;

        public string Name => "Delete specific closed days";

        public void Execute(int data)
        {
            Domain.Tables.SpecificClosedDays sd = Context.SpecificClosedDays.FirstOrDefault(x => x.Id == data);

            if (sd == null || !sd.IsActive)
            {
                throw new NotFoundException(nameof(Domain.Tables.SpecificClosedDays), data);
            }

            sd.IsActive = false;

            Context.SaveChanges();
        }
    }
}
