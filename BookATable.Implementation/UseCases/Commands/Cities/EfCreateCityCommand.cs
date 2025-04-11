using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Cities;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookATable.Implementation.UseCases.Commands.Cities
{
    public class EfCreateCityCommand : EfUseCase, ICreateCityCommand
    {
        private CreateCityValidator _validator;
        public EfCreateCityCommand(Context context, CreateCityValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 14;

        public string Name => "Create city";

        public void Execute(CreateCityDTO data)
        {
            _validator.ValidateAndThrow(data);

            City city = new City
            {
                Name = data.Name,
                ZipCode = data.ZipCode
            };

            Context.Cities.Add(city);
            Context.SaveChanges();
        }
    }
}
