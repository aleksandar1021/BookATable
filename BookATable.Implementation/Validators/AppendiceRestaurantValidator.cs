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
    public class AppendiceRestaurantValidator : AbstractValidator<CreateAppendiceRestaurantDTO>
    {
        private IApplicationActor actor;
        public AppendiceRestaurantValidator(Context ctx, IApplicationActor actor)
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

            RuleFor(x => x.AppendiceId)
                   .NotEmpty()
                   .WithMessage("Appendice is required.")
                   .Must(x => ctx.Appendices.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("Appendice does not exists.");
        }
    }
}
