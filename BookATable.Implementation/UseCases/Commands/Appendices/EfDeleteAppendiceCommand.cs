using BookATable.Application.UseCases.Commands.Appendices;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Appendices
{
    public class EfDeleteAppendiceCommand : EfUseCase, IDeleteAppendiceCommand
    {
        public EfDeleteAppendiceCommand(Context context) : base(context)
        {
        }

        public int Id => 31;

        public string Name => "Delete appendice";

        public void Execute(int data)
        {
            Appendice appendice = Context.Appendices.Include(x => x.AppendiceRestaurants).FirstOrDefault(x => x.Id == data);

            if (appendice == null || !appendice.IsActive) 
            {
                throw new NotFoundException(nameof(Appendice), data);
            }

            if (appendice.AppendiceRestaurants.Any())
            {
                throw new ConflictException("Appendice does not can be deleted.");
            }

            appendice.IsActive = false;
            Context.SaveChanges();
        }
    }
}
