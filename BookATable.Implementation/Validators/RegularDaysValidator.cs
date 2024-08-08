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
    public class RegularDaysValidator : AbstractValidator<CreateRegularClosedDaysDTO>
    {
        private IApplicationActor actor;
        private Context ctx;
        public RegularDaysValidator(Context ctx, IApplicationActor actor)
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

            RuleFor(x => x.Days)
            .Must(ContainValidDays)
            .WithMessage("All items in the Days list must be valid day of week values.")
            .Must((model, days) => BeUniqueDaysForRestaurant(model, days))
            .WithMessage("Each day can only be closed once for the same restaurant.");
            
        }

        private bool BeUniqueDaysForRestaurant(CreateRegularClosedDaysDTO model, IEnumerable<System.DayOfWeek> days)
        {
            if (model.RestaurantId == null)
                return true;

            var existingDays = ctx.RegularClosedDays
                .Where(rcd => rcd.RestaurantId == model.RestaurantId)
                .Select(rcd => rcd.DayOfWeek)
                .ToList();

            var domainDays = days.Select(day => (BookATable.Domain.Tables.DayOfWeek)day).ToList();

            return !domainDays.Any(day => existingDays.Contains(day));
        }



        private bool ContainValidDays(IEnumerable<DayOfWeek> days)
        {
            if (days == null)
                return false;

            return days.All(day => Enum.IsDefined(typeof(DayOfWeek), day));
        }

        
    }
}
