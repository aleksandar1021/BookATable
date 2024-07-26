using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategoryRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.MealCategoryRestaurants
{
    public class EfUpdateMealCategoryRestaurantCommand : EfUseCase, IUpdateMealCategoryRestaurantCommand
    {
        private MealCategoryRestaurantValidator _validator;
        public EfUpdateMealCategoryRestaurantCommand(Context context, MealCategoryRestaurantValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 42;

        public string Name => "Update meal category restaurant";

        public void Execute(UpdateMealCategoryRestaurantDTO data)
        {
            MealCategoryRestaurant mcr = Context.MealCategoryRestaurants.FirstOrDefault(x => x.Id == data.Id);

            if( mcr == null || !mcr.IsActive)
            {
                throw new NotFoundException(nameof(MealCategoryRestaurant), data.Id);
            }

            mcr.RestaurantId = data.RestaurantId;
            mcr.MealCategoryId = data.MealCategoryId;
            mcr.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
