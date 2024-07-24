using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.RestaurantTypes;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.RestaurantTypes
{
    public class EfUpdateRestaurantTypeCommand : EfUseCase, IUpdateRestaurantTypeCommand
    {
        private RestaurantTypeValidator _validator;
        public EfUpdateRestaurantTypeCommand(Context context, RestaurantTypeValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 25;

        public string Name => "Update restaurant type";

        public void Execute(UpdateNamedEntity data)
        {
            RestaurantType rt = Context.RestaurantTypes.FirstOrDefault(x => x.Id == data.Id);

            if(rt == null || !rt.IsActive)
            {
                throw new NotFoundException(nameof(RestaurantType), data.Id);
            }

            _validator.ValidateAndThrow(data);

            rt.Name = data.Name;
            rt.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
