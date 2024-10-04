using BookATable.Application.DTO;
using BookATable.Application.Email;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Users
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private RegisterUserValidator _validator;
        private readonly IEmailSender _emailSender;
        public EfRegisterUserCommand(Context context, RegisterUserValidator validator, IEmailSender emailSender) : base(context)
        {
            _validator = validator;
            _emailSender = emailSender;
        }

        public int Id => 1;

        public string Name => "Register user";

        private List<int> allowedCasesForUser = new List<int>
        { 
            2, 3, 5, 61, 85, 56, 63, 34, 70, 23, 22, 49, 50, 32,
            33, 18, 17, 55, 54, 13, 12, 44, 45, 59, 78, 79, 68, 69, 
            64, 65, 39, 73, 40, 74, 27, 28, 71, 72, 83, 84, 6, 7, 94, 95, 96
            
        };

        public void Execute(RegisterUserDTO data)
        {
            _validator.ValidateAndThrow(data);

            string activationCode = GenerateActivationCode();

            User user = new User
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Image = data.Image ?? "avatar.png",
                ActivationCode = activationCode,
                IsActive = false,
                UserUseCases = allowedCasesForUser.Select(u => new UserUseCase
                {
                    UseCaseId = u
                }).ToList()
            };

            if (data.Image != null)
            {
                var tempImageName = Path.Combine("wwwroot", "temp", data.Image);
                var destinationFileName = Path.Combine("wwwroot", "userPhotos", data.Image);
                System.IO.File.Move(tempImageName, destinationFileName);
            }

            Context.Users.Add(user);
            Context.SaveChanges();

            EmailDTO email = new EmailDTO
            {
                Subject = "Registration",
                Body = $"<h1>Successfull registration!</h1>Your activation code is: <span style='color: red'>{activationCode}</span>",
                SendTo = data.Email
            };


            _emailSender.SendEmail(email);
        }

        private string GenerateActivationCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
