using BookATable.Application.UseCases.Commands.MealCategories;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.MealCategories
{
    public class EfDeleteMealCategoryCommand : EfUseCase, IDeleteMealCategoryCommand
    {
        public EfDeleteMealCategoryCommand(Context context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Delete meal category";

        public void Execute(int data)
        {
            MealCategory mealCategory = Context.MealCategories.FirstOrDefault(x => x.Id == data);

            if (mealCategory == null || !mealCategory.IsActive) 
            {
                throw new NotFoundException(nameof(MealCategory), data);
            }

            mealCategory.IsActive = false;

            Context.SaveChanges();
        }
    }
}
