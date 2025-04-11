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
    public class UpdateUserValidator : BaseUserUpdateValidator<UpdateUserDTO>
    {
        public UpdateUserValidator(Context ctx, IApplicationActor actor) : base(ctx, actor)
        {
            RuleFor(x => x.Email)
                  .EmailAddress()
                  .WithMessage("Email address must be in format (user@gmail.com).")
                  .Must(x => !ctx.Users.Any(u => _actor.Id != u.Id && u.Email == x && u.IsActive))
                  .WithMessage("Email is already in use.");
        }
    }
}
