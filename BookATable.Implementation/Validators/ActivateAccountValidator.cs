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
    public class ActivateAccountValidator : AbstractValidator<ActivateAccountDTO>
    {
        public ActivateAccountValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ActivationCode).NotEmpty()
                                           .WithMessage("Activation code is required.")
                                           .Length(4)
                                           .WithMessage("The activation code must contain 4 digits.")
                                           .Matches(@"^\d{4}$")
                                           .WithMessage("The activation code must contain exactly 4 digits.");

            RuleFor(x => x.Email).NotEmpty()
                                 .WithMessage("Email is required.")
                                 .Must(x => ctx.Users.Any(e => e.Email == x && e.IsActive))
                                 .WithMessage("The user does not exist.");
        }
    }
}
