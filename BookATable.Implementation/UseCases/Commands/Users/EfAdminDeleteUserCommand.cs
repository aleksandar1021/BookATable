using BookATable.Application.UseCases.Commands.Users;
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

namespace BookATable.Implementation.UseCases.Commands.Users
{
    public class EfAdminDeleteUserCommand : EfUseCase, IAdminDeleteUserCommand
    {
        public EfAdminDeleteUserCommand(Context context) : base(context)
        {
        }

        public int Id => 4;

        public string Name => "Delete user (Admin)";

        public void Execute(int data)
        {
            User user = Context.Users.Include(x => x.Reservations)
                                     .Include(x => x.Ratings)
                                     .Include(x => x.Restaurants)
                                     .FirstOrDefault(x => x.Id == data && x.Email != "administrator@gmail.com");


            if (user == null || !user.IsActive)
            {
                throw new NotFoundException(nameof(User), data);
            }


            user.IsActive = false;

            user.Reservations.ToList().ForEach(r => r.IsActive = false);
            user.Ratings.ToList().ForEach(r => r.IsActive = false);
            user.Restaurants.ToList().ForEach(r => r.IsActive = false);

            Context.SaveChanges();
        }
    }
}
