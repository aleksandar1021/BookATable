using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategories;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.MealCategories
{
    public class EfUpdateMealCategoryCommand : EfUseCase, IUpdateMealCategoryCommand
    {
        private NamedEntityValidator _validator;
        public EfUpdateMealCategoryCommand(Context context, NamedEntityValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "Update meal category";

        public void Execute(UpdateNamedEntity data)
        {
            MealCategory mealCategory = Context.MealCategories.FirstOrDefault(x => x.Id == data.Id);

            if(mealCategory == null)
            {
                throw new NotFoundException(nameof(MealCategory), data.Id);
            }

            _validator.ValidateAndThrow(data);

            mealCategory.Name = data.Name;
            mealCategory.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();

        }
    }
}
