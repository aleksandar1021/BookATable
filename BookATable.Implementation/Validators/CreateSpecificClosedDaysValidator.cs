using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class CreateSpecificClosedDaysValidator : AbstractValidator<CreateSpecificClosedDaysDTO>
    {
        private IApplicationActor actor;
        private Context ctx;
        public CreateSpecificClosedDaysValidator(IApplicationActor actor, Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            this.actor = actor;
            this.ctx = ctx;

            RuleFor(x => x.RestaurantId)
                   .NotEmpty()
                   .WithMessage("Restaurant is required.")
                   .Must(x => ctx.Restaurants.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("Restaurant does not exists.")
                   .Must((dto, x) => ctx.Restaurants.Where(r => r.Id == x).Select(x => x.UserId).FirstOrDefault() == actor.Id)
                   .WithMessage("You do not have the right to perform this action.");

            RuleFor(x => x.ClosedFrom)
            .LessThan(x => x.ClosedTo)
            .WithMessage("'ClosedFrom' must be earlier than closed to date.");

            RuleFor(x => x.ClosedTo)
                .GreaterThan(x => x.ClosedFrom)
                .WithMessage("'ClosedTo' must be later than closed from date.");
        }
    }
}
