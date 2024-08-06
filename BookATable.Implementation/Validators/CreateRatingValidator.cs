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
    public class CreateRatingValidator : AbstractValidator<CreateRatingDTO>
    {
        public CreateRatingValidator(Context ctx)
        {
            RuleFor(x => x.RestaurantId).NotEmpty()
                                        .WithMessage("Restaurant is required.")
                                        .Must(x => ctx.Restaurants.Any(a => a.Id == x && a.IsActive))
                                        .WithMessage("Restaurant does not exists.");

            RuleFor(x => x.Rate).NotEmpty()
                                        .WithMessage("Rate is required.")
                                        .Must(rate => int.TryParse(rate.ToString(), out _))
                                        .WithMessage("Rate must be an integer.")
                                        .InclusiveBetween(1, 5)
                                        .WithMessage("Rate must be between 1 and 5.");

            RuleFor(x => x.UserId).NotEmpty()
                                        .WithMessage("User is required.")
                                        .Must(x => ctx.Users.Any(a => a.Id == x && a.IsActive))
                                        .WithMessage("User does not exists.");
        }
    }
}
