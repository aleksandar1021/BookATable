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
    public class NamedEntityValidator : AbstractValidator<CreateNamedEntity>
    {
        public NamedEntityValidator(Context ctx)
        {
            RuleFor(x => x.Name)
                   .NotEmpty()
                   .Must(x => !ctx.MealCategories.Any(c => c.Name == x))
                   .WithMessage("Name is alredy in use.")
                   .Matches("^[A-Z][a-zA-Z1-9\\s]{2,49}$")
                   .WithMessage("The name must start with a capital letter and contain a minimum of 3 characters and a maximum of 50.");
        }
    }
}
