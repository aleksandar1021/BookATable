using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.Email;
using BookATable.Application.UseCases.Commands.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Restaurants
{
    public class EfAcceptRestaurantCommand : EfUseCase, IAcceptRestaurantCommand
    {
        private IEmailSender _emailSender;

        public EfAcceptRestaurantCommand(Context context, IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
        }

        public int Id => 90;

        public string Name => "Accept restaurant";

        public void Execute(AcceptRestaurantDTO data)
        {
            var restaurant = Context.Restaurants.Find(data.RestaurantId);
            var user = Context.Users.Find(data.UserId);

            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), data.RestaurantId);
            }

            if (user == null)
            {
                throw new NotFoundException(nameof(User), data.UserId);
            }

            restaurant.IsActivated = true;



            List<int> allowedCasesForUser = new List<int>
            {
                19, 21, 20, 46, 48, 47, 51, 53, 52, 41, 43, 42, 58, 57, 75, 77, 76, 66, 67, 88, 89, 62, 37, 35, 80, 82, 81, 92, 93
            };

            List<UserUseCase> userUseCases = allowedCasesForUser
                                            .Select(useCaseId => new UserUseCase { UserId = user.Id, UseCaseId = useCaseId })
                                            .ToList();


            if(user.Restaurants.Where(x => x.IsActivated && x.IsActive).Count() == 1)
            {
                Context.UserUseCases.AddRange(userUseCases);

            }


            Context.SaveChanges();

            EmailDTO dto = new EmailDTO
            {
                SendTo = restaurant.User.Email,
                Subject = "Success accepted restaurant",
                Body = $"Your restaurant has been successfully accepted ({restaurant.Name}) for marketing by the administrator, now you can if you want to change the restaurant or add new dishes"
            };

            _emailSender.SendEmail(dto);
        }
    }
}
