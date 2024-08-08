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
    public class SavedValidator : AbstractValidator<CreateSavedDTO>
    {
        private IApplicationActor _actor;
        public SavedValidator(Context ctx, IApplicationActor actor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            _actor = actor;

            RuleFor(x => x.RestaurantId)
                               .NotEmpty()
                               .WithMessage("Restaurant is required.")
                               .Must(x => ctx.Restaurants.Any(a => a.Id == x && a.IsActive))
                               .WithMessage("Restaurant does not exists.");

            RuleFor(x => x.UserId)
                               .NotEmpty()
                               .WithMessage("User is required.")
                               .Must(x => ctx.Users.Any(a => a.Id == x && a.IsActive))
                               .WithMessage("User does not exists.")
                               .Must(x => _actor.Id == x)
                               .WithMessage("You do not have the right to perform this action."); ;
            
        }
    }
}
