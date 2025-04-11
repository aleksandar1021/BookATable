using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Users;
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

namespace BookATable.Implementation.UseCases.Commands.Users
{
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
        private IApplicationActor _actor;
        private UpdateUserValidator _validator;
        public EfUpdateUserCommand(Context context, IApplicationActor actor, UpdateUserValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 5;

        public string Name => "Update user";

        public void Execute(UpdateUserDTO data)
        {
            User user = Context.Users.FirstOrDefault(x => x.Id == _actor.Id);

            if(user == null || !user.IsActive)
            {
                throw new NotFoundException(nameof(User), data.Id);
            }

            if (_actor.Id != data.Id)
            {
                throw new UnauthorizedAccessException();
            }
            int i = 0;
            if(data.Password.IsNullOrEmpty())
            {
                data.Password = user.Password;
                i++;
            }

            _validator.ValidateAndThrow(data);

            if (i == 0)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            }
            else
            {
                user.Password = user.Password;
            }
            user.Email = data.Email;
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.UpdatedAt = DateTime.UtcNow;
            

            if (!data.Image.IsNullOrEmpty())
            {
                var tempImageName = Path.Combine("wwwroot", "temp", data.Image);
                var destinationFileName = Path.Combine("wwwroot", "userPhotos", data.Image);
                System.IO.File.Move(tempImageName, destinationFileName);

                user.Image = data.Image ;
            }

            Context.SaveChanges();

        }
    }
}
