using BookATable.Application.UseCases.Commands.MealCategoryRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.MealCategoryRestaurants
{
    public class EfDeleteMealCategoryRestaurantCommand : EfUseCase, IDeleteMealCategoryRestaurantCommand
    {
        public EfDeleteMealCategoryRestaurantCommand(Context context) : base(context)
        {
        }

        public int Id => 43;

        public string Name => "Delete meal category restaurant";

        public void Execute(int data)
        {
            MealCategoryRestaurant mcr = Context.MealCategoryRestaurants.FirstOrDefault(x => x.Id == data);

            if (mcr == null || !mcr.IsActive)
            {
                throw new NotFoundException(nameof(MealCategoryRestaurant), data);
            }

            mcr.IsActive = false;
            Context.SaveChanges();
        }
    }
}
