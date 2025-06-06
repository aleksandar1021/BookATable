﻿using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Users
{
    public class EfAdminUpdateUserCommand : EfUseCase, IAdminUpdateUserCommand
    {
        private AdminUpdateUserValidator _validator;
        public EfAdminUpdateUserCommand(Context context, AdminUpdateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Update user (Admin)";

        public void Execute(UpdateUserDTO data)
        {
            User user = Context.Users.FirstOrDefault(x => x.Id == data.Id);

            if (user == null || !user.IsActive)
            {
                throw new NotFoundException(nameof(User), data.Id);
            }

            _validator.ValidateAndThrow(data);

            user.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            user.Email = data.Email;
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.UpdatedAt = DateTime.UtcNow;
            user.Image = data.Image ?? "avatar.png";

            if (data.Image != null)
            {
                var tempImageName = Path.Combine("wwwroot", "temp", data.Image);
                var destinationFileName = Path.Combine("wwwroot", "userPhotos", data.Image);
                System.IO.File.Move(tempImageName, destinationFileName);
            }

            Context.SaveChanges();
        }
    }
}
