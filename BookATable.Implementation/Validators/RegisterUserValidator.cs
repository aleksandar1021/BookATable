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
    public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FirstName)
                   .NotEmpty()
                   .Matches("^[A-Z][a-zA-Z]{2,29}$")
                   .WithMessage("The name must start with a capital letter and contain a minimum of 3 characters and a maximum of 30");

            RuleFor(x => x.LastName)
                   .NotEmpty()
                   .Matches("^[A-Z][a-zA-Z]{2,29}$")
                   .WithMessage("The lastname must start with a capital letter and contain a minimum of 3 characters and a maximum of 30");

            RuleFor(x => x.Password)
                  .NotEmpty()
                  .Matches("^(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}$")
                  .WithMessage("The password must contain at least 8 characters and must contain at least one capital letter, one number and one special character.");

            RuleFor(x => x.Email)
                  .EmailAddress()
                  .WithMessage("Email address must be in format (user@gmail.com)")
                  .Must(x => !ctx.Users.Any(u => u.Email == x))
                  .WithMessage("Email is already in use.");

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

        }
    }
}
