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
    public class UpdateNamedValidator<T> : AbstractValidator<UpdateNamedEntity> where T : class
    {
        public UpdateNamedValidator(Context ctx, Func<Context, DbSet<T>> dbSetFunc, Func<T, string> getNameFunc, Func<T, int> getIdFunc)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Must((entity, name) =>
                {
                    var currentEntity = dbSetFunc(ctx).Find(entity.Id);
                    return !dbSetFunc(ctx).AsEnumerable().Any(e => getNameFunc(e) == name && getIdFunc(e) != entity.Id);
                })
                .WithMessage("Name is already in use.")
                .Matches("^[A-Z][a-zA-Z1-9\\s]{2,49}$")
                .WithMessage("The name must start with a capital letter and contain a minimum of 3 characters and a maximum of 50.");
        }
    }
}
