using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Cities;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Cities
{
    public class EfUpdateCityCommand : EfUseCase, IUpdateCityCommand
    {
        private UpdateCityValidator _validator;
        public EfUpdateCityCommand(Context context, UpdateCityValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "Update city";

        public void Execute(UpdateCityDTO data)
        {
            City city = Context.Cities.FirstOrDefault(x => x.Id == data.Id);

            if(city == null)
            {
                throw new NotFoundException(nameof(City), data.Id);
            }

            _validator.ValidateAndThrow(data);

            city.Name = data.Name;
            city.ZipCode = data.ZipCode;
            city.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
