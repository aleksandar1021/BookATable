using BookATable.Application.DTO;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class MealCategoryValidator : AbstractValidator<CreateMealCategoryDTO>
    {
        private Context _ctx;
        public MealCategoryValidator(Context ctx)
        {
            _ctx = ctx;
            RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
                        .Must(BeUniqueName).WithMessage("Name must be unique.");

            RuleFor(x => x.Image)
                        .NotEmpty().WithMessage("Image is required.");

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

        private bool BeUniqueName(string name)
        {
            return !_ctx.MealCategories.Any(x => x.Name == name);
        }
    }
}
