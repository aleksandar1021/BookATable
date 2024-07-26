using BookATable.Application.DTO;
using BookATable.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class MealCategoryRestaurantValidator : AbstractValidator<CreateMealCategoryRestaurantDTO>
    {
        public MealCategoryRestaurantValidator(Context ctx)
        {
            RuleFor(x => x.RestaurantId)
                   .NotEmpty()
                   .WithMessage("Restaurant is required.")
                   .Must(x => ctx.Restaurants.Any(a => a.Id == x))
                   .WithMessage("Restaurant does not exists.");

            RuleFor(x => x.MealCategoryId)
                   .NotEmpty()
                   .WithMessage("Meal category is required.")
                   .Must(x => ctx.MealCategories.Any(a => a.Id == x))
                   .WithMessage("Meal category does not exists.");
        }
    }
}
