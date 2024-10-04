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
    public class BaseAddressValidator<T> : AbstractValidator<T> where T : CreateAddressDTO
    {
        public BaseAddressValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.CityId)
                   .NotEmpty()
                   .WithMessage("City Id is required.")
                   .Must(x => !ctx.Addresses.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("City does not exists.");

            RuleFor(x => x.Place)
                   .Matches("^[A-Z][a-zA-Z1-9\\s]{2,49}$")
                   .WithMessage("The Place must start with a capital letter and contain a minimum of 3 characters and a maximum of 50.");

            RuleFor(x => x.Address)
                   .NotEmpty()
                   .WithMessage("Address is required.")
                   .Matches("^[A-Z][a-zA-Z1-9\\s]{2,199}$")
                   .WithMessage("The Address must start with a capital letter and contain a minimum of 3 characters and a maximum of 50.");

            RuleFor(x => x.Number)
                   .MaximumLength(10)
                   .WithMessage("Maximum length is 10.");

            RuleFor(x => x.Floor)
                    .Must(x => x > 0 && x < 101)  
                    .WithMessage("Floor must be in range 1-100.");

        }
    }
}
