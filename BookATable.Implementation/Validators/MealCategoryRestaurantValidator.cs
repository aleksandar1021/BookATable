using BookATable.Application;
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
        private IApplicationActor actor;
        public MealCategoryRestaurantValidator(Context ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            this.actor = actor;
            RuleFor(x => x.RestaurantId)
                   .NotEmpty()
                   .WithMessage("Restaurant is required.")
                   .Must(x => ctx.Restaurants.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("Restaurant does not exists.")
                   .Must((dto, x) => ctx.Restaurants.Where(r => r.Id == x).Select(x => x.UserId).FirstOrDefault() == actor.Id)
                   .WithMessage("You do not have the right to perform this action.");

            RuleFor(x => x.MealCategoryId)
                   .NotEmpty()
                   .WithMessage("Meal category is required.")
                   .Must(x => ctx.MealCategories.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("Meal category does not exists.");
            this.actor = actor;
        }
    }
}
