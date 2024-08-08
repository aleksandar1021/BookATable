using BookATable.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class ContactValidator : AbstractValidator<CreateContactDTO>
    {
        public ContactValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email)
                  .EmailAddress()
                  .WithMessage("Email address must be in format (user@gmail.com).");

            RuleFor(x => x.Subject).NotEmpty()
                                   .WithMessage("Subject is required.")
                                   .Must(x => x.Length >= 3)
                                   .WithMessage("Minimum length is 3 characters.");

            RuleFor(x => x.Message).NotEmpty()
                                   .WithMessage("Message is required.")
                                   .Must(x => x.Length >= 3)
                                   .WithMessage("Minimum length is 3 characters.");

            RuleFor(x => x.Name).NotEmpty()
                                   .WithMessage("Name is required.")
                                   .Must(x => x.Length >= 3)
                                   .WithMessage("Minimum length is 3 characters.");
        }
    }
}
