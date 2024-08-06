using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Restaurants
{
    public class EfAdminDeleteRestaurantCommand : EfUseCase, IAdminDeleteRestaurantCommand
    {
        public EfAdminDeleteRestaurantCommand(Context context) : base(context)
        {
        }

        public int Id => 38;

        public string Name => "Delete restaurant (Admin)";

        public void Execute(int data)
        {
            Restaurant restaurant = Context.Restaurants.Include(x => x.RestaurantImages)
                                                       .Include(x => x.Reservations)
                                                       .Include(x => x.AppendiceRestaurants)
                                                       .Include(x => x.Dishs)
                                                       .Include(x => x.MealCategoryRestaurants)
                                                       .Include(x => x.Ratings)
                                                       .FirstOrDefault(x => x.Id == data);

            if (restaurant == null || !restaurant.IsActive)
            {
                throw new NotFoundException(nameof(Restaurant), data);
            }

            restaurant.IsActive = false;

            restaurant.RestaurantImages.ToList().ForEach(r => r.IsActive = false);
            restaurant.Reservations.ToList().ForEach(r => r.IsActive = false);
            restaurant.AppendiceRestaurants.ToList().ForEach(r => r.IsActive = false);
            restaurant.Dishs.ToList().ForEach(r => r.IsActive = false);
            restaurant.MealCategoryRestaurants.ToList().ForEach(r => r.IsActive = false);
            restaurant.Ratings.ToList().ForEach(r => r.IsActive = false);

            

            foreach (var oldImage in restaurant.RestaurantImages)
            {
                var oldImagePath = Path.Combine("wwwroot", "restaurantPhotos", oldImage.Path);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            Context.SaveChanges();
        }
    }
}
