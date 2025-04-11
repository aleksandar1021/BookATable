using BookATable.Application.UseCases.Commands.RegularClosedDays;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.RegularClosedDays
{
    public class EfDeleteRegularClosedDaysCommand : EfUseCase, IDeleteRegularClosedDaysCommand
    {
        public EfDeleteRegularClosedDaysCommand(Context context) : base(context)
        {
        }

        public int Id => 77;

        public string Name => "Delete regular day for restaurant";

        public void Execute(int data)
        {
            Domain.Tables.RegularClosedDays day = Context.RegularClosedDays.FirstOrDefault(x => x.Id == data);

            if (day == null || !day.IsActive)
            {
                throw new NotFoundException(nameof(Domain.Tables.RegularClosedDays), data);
            }

            day.IsActive = false;
            Context.SaveChanges();
        }
    }
}
