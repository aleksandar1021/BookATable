using BookATable.Application;
using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Restaurants
{
    public class EfDeleteRestaurantCommand : EfUseCase, IDeleteRestaurantCommand
    {
        private IApplicationActor _actor;
        public EfDeleteRestaurantCommand(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 37;

        public string Name => "Delete restaurant";

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

            if (restaurant.UserId != _actor.Id)
            {
                throw new UnauthorizedAccessException();
            }

            restaurant.IsActive = false;

            restaurant.RestaurantImages.ToList().ForEach(r => r.IsActive = false);
            restaurant.Reservations.ToList().ForEach(r => r.IsActive = false);
            restaurant.AppendiceRestaurants.ToList().ForEach(r => r.IsActive = false);
            restaurant.Dishs.ToList().ForEach(r => r.IsActive = false);
            restaurant.MealCategoryRestaurants.ToList().ForEach(r => r.IsActive = false);
            restaurant.Ratings.ToList().ForEach(r => r.IsActive = false);

            Context.SaveChanges();
        }
    }
}
