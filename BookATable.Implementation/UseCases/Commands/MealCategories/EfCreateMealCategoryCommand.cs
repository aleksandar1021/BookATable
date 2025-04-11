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
        private MealCategoryValidator _validator;
        public EfCreateMealCategoryCommand(Context context, MealCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Create meal category";

        public void Execute(CreateMealCategoryDTO data)
        {
            _validator.ValidateAndThrow(data);

            MealCategory mealCategory = new MealCategory
            {
                Name = data.Name,
                Image = data.Image
            };

            if (data.Image != null)
            {
                var tempImageName = Path.Combine("wwwroot", "temp", data.Image);
                var destinationFileName = Path.Combine("wwwroot", "mealCategories", data.Image);
                System.IO.File.Move(tempImageName, destinationFileName);
            }

            Context.MealCategories.Add(mealCategory);   
            Context.SaveChanges();
        }
    }
}
