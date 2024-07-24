using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Addresses;
using BookATable.Application.UseCases.Commands.RestaurantTypes;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.RestaurantTypes
{
    public class EfCreateRestaurantTypesCommand : EfUseCase, ICreateRestaurantTypeCommand
    {
        private RestaurantTypeValidator _validator;
        public EfCreateRestaurantTypesCommand(Context context, RestaurantTypeValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 24;

        public string Name => "Create restaurant type";

        public void Execute(CreateNamedEntity data)
        {
            _validator.ValidateAndThrow(data);

            RestaurantType rt = new RestaurantType
            {
                Name = data.Name
            };

            Context.RestaurantTypes.Add(rt);
            Context.SaveChanges();
        }
    }
}
