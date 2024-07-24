using BookATable.Application.UseCases.Commands.Addresses;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Addresses
{
    public class EfDeleteAddressCommand : EfUseCase, IDeleteAddressCommand
    {
        public EfDeleteAddressCommand(Context context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Update address";

        public void Execute(int data)
        {
            Address address = Context.Addresses.Include(x => x.Restaurants)
                                               .FirstOrDefault(x => x.Id == data);

            if(address == null || !address.IsActive) 
            {
                throw new NotFoundException(nameof(Address), data);
            }

            if (address.Restaurants.Any())
            {
                throw new ConflictException("Address does not can be deleted.");
            }

            address.IsActive = false;
            Context.SaveChanges();
        }
    }
}
