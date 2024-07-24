using BookATable.Application.UseCases.Commands.Cities;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Cities
{
    public class EfDeleteCityCommand : EfUseCase, IDeleteCityCommand
    {
        public EfDeleteCityCommand(Context context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => "Delete city";

        public void Execute(int data)
        {
            City city = Context.Cities.Include(x => x.Addresses).FirstOrDefault(x => x.Id == data);

            if(city == null || !city.IsActive)
            {
                throw new NotFoundException(nameof(City), data);
            }

            city.IsActive = false;

            city.Addresses.ToList().ForEach(r => r.IsActive = false);

            Context.SaveChanges();

        }
    }
}
