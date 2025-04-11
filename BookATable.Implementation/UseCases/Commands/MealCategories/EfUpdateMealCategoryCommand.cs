using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategories;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.MealCategories
{
    public class EfUpdateMealCategoryCommand : EfUseCase, IUpdateMealCategoryCommand
    {
        private UpdateMealCategoryValidator _validator;
        public EfUpdateMealCategoryCommand(Context context, UpdateMealCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "Update meal category";

        public void Execute(UpdateMealCategoryDTO data)
        {
            MealCategory mealCategory = Context.MealCategories.FirstOrDefault(x => x.Id == data.Id);

            if(mealCategory == null || !mealCategory.IsActive)
            {
                throw new NotFoundException(nameof(MealCategory), data.Id);
            }

            _validator.ValidateAndThrow(data);

            mealCategory.Name = data.Name;
            mealCategory.UpdatedAt = DateTime.UtcNow;

            

            if (!data.Image.IsNullOrEmpty())
            {
                var tempImageName = Path.Combine("wwwroot", "temp", data.Image);
                var destinationFileName = Path.Combine("wwwroot", "mealCategories", data.Image);
                System.IO.File.Move(tempImageName, destinationFileName);


                var oldImagePath = Path.Combine("wwwroot", "mealCategories", mealCategory.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }


                mealCategory.Image = data.Image;
            }

           

            Context.SaveChanges();

        }
    }
}
