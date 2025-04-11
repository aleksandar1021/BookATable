using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class CreateDishValidator : AbstractValidator<CreateDishDTO>
    {
        private IApplicationActor actor;
        public CreateDishValidator(Context ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            this.actor = actor;
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
                               .Must(x => ctx.Restaurants.Any(a => a.Id == x && a.IsActive))
                               .WithMessage("Restaurant does not exists.")
                               .Must((dto, x) => ctx.Restaurants.Where(r => r.Id == x).Select(x => x.UserId).FirstOrDefault() == actor.Id)
                               .WithMessage("You do not have the right to perform this action.");


            RuleFor(x => x.Image).Must((x, fileName) =>
            {
                if (fileName == null)
                {
                    return true;
                }
                var path = Path.Combine("wwwroot", "temp", fileName);

                var exists = Path.Exists(path);

                return exists;
            }).WithMessage("File doesn't exist.");
            this.actor = actor;
        }
    }


}
