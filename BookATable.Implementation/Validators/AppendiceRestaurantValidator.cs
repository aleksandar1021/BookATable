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
    public class AppendiceRestaurantValidator : AbstractValidator<CreateAppendiceRestaurantDTO>
    {
        public AppendiceRestaurantValidator(Context ctx)
        {
            RuleFor(x => x.RestaurantId)
                   .NotEmpty()
                   .WithMessage("Restaurant is required.")
                   .Must(x => ctx.Restaurants.Any(a => a.Id == x))
                   .WithMessage("Restaurant does not exists.");

            RuleFor(x => x.AppendiceId)
                   .NotEmpty()
                   .WithMessage("Appendice is required.")
                   .Must(x => ctx.Appendices.Any(a => a.Id == x))
                   .WithMessage("Appendice does not exists.");
        }
    }
}
