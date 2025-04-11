using BookATable.Application.DTO;
using BookATable.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class NamedEntityValidator<T> : AbstractValidator<CreateNamedEntity> where T : class
    {
        public NamedEntityValidator(Context ctx, Func<Context, DbSet<T>> dbSetFunc, Func<T, string> getNameFunc)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                   .NotEmpty()
                   .Must(name => !dbSetFunc(ctx).AsEnumerable().Any(entity => getNameFunc(entity) == name))
                   .WithMessage("Name is already in use.")
                   .Matches("^[A-Z][a-zA-Z1-9\\s]{2,49}$")
                   .WithMessage("The name must start with a capital letter and contain a minimum of 3 characters and a maximum of 50.");
        }
    }
}
