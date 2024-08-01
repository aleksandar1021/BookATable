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
    public class CreateDishValidator : AbstractValidator<CreateDishDTO>
    {
        public CreateDishValidator(Context ctx)
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Dish name is required.")
                                .MaximumLength(70)
                                .WithMessage("Maximum length for name is 70 charatcers.");

            RuleFor(x => x.Description).NotEmpty()
                                .WithMessage("Dish description is required.")
                                .MaximumLength(150)
                                .WithMessage("Maximum length for description is 150 charatcers.");

            RuleFor(x => x.Description).NotEmpty()
                                .WithMessage("Dish description is required.")
                                .MaximumLength(150)
                                .WithMessage("Maximum length for description is 150 charatcers.");

            RuleFor(x => x.Price).NotEmpty()
                                 .WithMessage("Dish price is required.")
                                 .Must(x => x > 0 || x < 100000)
                                 .WithMessage("Price must be between 0 and 100000.");

            RuleFor(x => x.RestaurantId)
                               .NotEmpty()
                               .WithMessage("Restaurant is required.")
                               .Must(x => ctx.Restaurants.Any(a => a.Id == x))
                               .WithMessage("Restaurant does not exists.");
        }
    }
}
