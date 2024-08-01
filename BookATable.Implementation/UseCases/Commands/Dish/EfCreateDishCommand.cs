using BookATable.Application.DTO;
using BookATable.Domain.Tables;
using BookATable.Application.UseCases.Commands.Dish;
using BookATable.DataAccess;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Dish
{
    public class EfCreateDishCommand : EfUseCase, ICreateDishCommand
    {
        private CreateDishValidator _validator;
        public EfCreateDishCommand(Context context, CreateDishValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 51;

        public string Name => "Create dish";

        public void Execute(CreateDishDTO data)
        {
            _validator.ValidateAndThrow(data);

            Domain.Tables.Dish dish = new Domain.Tables.Dish
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                RestaurantId = data.RestaurantId
            };

            Context.Dishs.Add(dish);
            Context.SaveChanges();
        }
    }
}
