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
    public class UpdateRestaurantValidator : BaseRestaurantValidator<CreateRestaurantDTO>
    {
        public UpdateRestaurantValidator(Context ctx, IApplicationActor actor) : base(ctx)
        {
            //RuleFor(x => x.UserId).NotEmpty()
            //                      .WithMessage("User is required.")
            //                      .Must((dto, x) => ctx.Restaurants.Where(r => r.Id == x).Select(x => x.UserId).FirstOrDefault() == actor.Id)
            //                      .WithMessage("You do not have the right to perform this action.");
        }
    }
}
