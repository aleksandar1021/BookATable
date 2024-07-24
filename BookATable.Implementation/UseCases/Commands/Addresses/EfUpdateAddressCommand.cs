using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Addresses;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookATable.Implementation.UseCases.Commands.Addresses
{
    public class EfUpdateAddressCommand : EfUseCase, IUpdateAddressCommand
    {
        private UpdateAddressValidator _validator;
        public EfUpdateAddressCommand(Context context, UpdateAddressValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 20;

        public string Name => "Update address";

        public void Execute(UpdateAddressDTO data)
        {
            Address address = Context.Addresses.FirstOrDefault(x => x.Id == data.Id);

            if(address == null || !address.IsActive)
            {
                throw new NotFoundException(nameof(Address), data.Id);
            }

            _validator.ValidateAndThrow(data);

            address.CityId = data.CityId;
            address.Place = data.Place;
            address.AddressOfPlace = data.Address;
            address.Number = data.Number;
            address.Floor = data.Floor;
            address.Description = data.Description;
            address.Latitude = data.Latitude;
            address.Longitude = data.Longitude;
            address.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
