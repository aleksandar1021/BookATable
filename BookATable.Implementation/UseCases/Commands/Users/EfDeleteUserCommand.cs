using BookATable.Application;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Users
{
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        private IApplicationActor _actor;
        public EfDeleteUserCommand(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 3;

        public string Name => "Delete user";

        public void Execute(int data)
        {
            User user = Context.Users.Include(x => x.Reservations)
                                     .Include(x => x.Ratings)
                                     .Include(x => x.Restaurants)
                                     .FirstOrDefault(x => x.Id == data);
                                     

            if(user == null || !user.IsActive)
            {
                throw new NotFoundException(nameof(User), data);
            }


            if (_actor.Id != data)
            {
                throw new ConflictException("You cannot delete other users, you can only delete your account.");
            }


            user.IsActive = false;

            user.Reservations.ToList().ForEach(r => r.IsActive = false);
            user.Ratings.ToList().ForEach(r => r.IsActive = false);
            user.Restaurants.ToList().ForEach(r => r.IsActive = false);

            Context.SaveChanges();
        }
    }
}
