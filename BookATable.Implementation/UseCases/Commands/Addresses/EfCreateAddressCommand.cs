using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Addresses;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Addresses
{
    public class EfCreateAddressCommand : EfUseCase, ICreateAddressCommand
    {
        private CreateAddressValidator _validator;
        public EfCreateAddressCommand(Context context, CreateAddressValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "Create address";

        public void Execute(CreateAddressDTO data)
        {
            _validator.ValidateAndThrow(data);

            Address address = new Address
            {
                CityId = data.CityId,
                Place = data.Place,
                AddressOfPlace = data.Address,
                Number = data.Number,
                Floor = data.Floor,
                Description = data.Description,
                Latitude = data.Latitude,
                Longitude = data.Longitude
            };

            Context.Addresses.Add(address);
            Context.SaveChanges();
        }
    }
}
