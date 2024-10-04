using BookATable.Application.DTO;
using BookATable.Application.Email;
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
        private IEmailSender _emailSender;

        public EfAdminDeleteRestaurantCommand(Context context, IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
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

            List<int> allowedCasesForUser = new List<int>
            {
                19, 21, 20, 46, 48, 47, 51, 53, 52, 41, 43, 42, 58, 57, 75, 77, 76, 66, 67, 88, 89, 62, 37, 35, 80, 82, 81, 92, 93
            };

            List<UserUseCase> userUseCases = allowedCasesForUser
                                            .Select(useCaseId => new UserUseCase { UserId = restaurant.User.Id, UseCaseId = useCaseId })
                                            .ToList();

            if (restaurant.User.Restaurants.Where(x => x.IsActivated && x.IsActive).Count() == 0)
            {
                Context.UserUseCases.RemoveRange(userUseCases);
            }


            Context.SaveChanges();


            EmailDTO dto = new EmailDTO
            {
                SendTo = restaurant.User.Email,
                Subject = "Your restaurant has been deleted",
                Body = $"Your restaurant has been deleted by the administrator, contact support for more information"
            };

            _emailSender.SendEmail(dto);
        }
    }
}
