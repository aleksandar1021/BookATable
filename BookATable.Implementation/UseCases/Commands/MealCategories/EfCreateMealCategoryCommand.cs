using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategories;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.MealCategories
{
    public class EfCreateMealCategoryCommand : EfUseCase, ICreateMealCategoryCommand
    {
        private NamedEntityValidator _validator;
        public EfCreateMealCategoryCommand(Context context, NamedEntityValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Create meal category";

        public void Execute(CreateNamedEntity data)
        {
            _validator.ValidateAndThrow(data);

            MealCategory mealCategory = new MealCategory
            {
                Name = data.Name
            };

            Context.MealCategories.Add(mealCategory);   
            Context.SaveChanges();
        }
    }
}
