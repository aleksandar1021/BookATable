using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategoryRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.MealCategoryRestaurants
{
    public class EfCreateMealCategoryRestaurantCommand : EfUseCase, ICreateMealCategoryRestaurantCommand
    {
        private MealCategoryRestaurantValidator _validator;
        public EfCreateMealCategoryRestaurantCommand(Context context, MealCategoryRestaurantValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 41;

        public string Name => "Create meal category restaurant";

        public void Execute(CreateMealCategoryRestaurantDTO data)
        {
            _validator.ValidateAndThrow(data);

            MealCategoryRestaurant mcr = new MealCategoryRestaurant
            {
                RestaurantId = data.RestaurantId,
                MealCategoryId = data.MealCategoryId
            };

            Context.MealCategoryRestaurants.Add(mcr);
            Context.SaveChanges();
        }
    }
}
