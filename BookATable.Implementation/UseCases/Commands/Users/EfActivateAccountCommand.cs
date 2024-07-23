using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Users;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Users
{
    public class EfActivateAccountCommand : EfUseCase, IActivateAccountCommand
    {
        private ActivateAccountValidator _validator;
        public EfActivateAccountCommand(Context context, ActivateAccountValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Activate account";

        public void Execute(ActivateAccountDTO data)
        {
            User user = Context.Users.FirstOrDefault(x => x.Email == data.Email);

            if(user == null)
            {
                throw new NotFoundExceptionStringEntry(nameof(User), data.Email);
            }
            if (user.IsActivatedUser)
            {
                throw new ConflictException("The user is already activated.");
            }

            _validator.ValidateAndThrow(data);

            if(user.ActivationCode != data.ActivationCode)
            {
                throw new ConflictException("Incorrect activation code.");
            }

            user.IsActivatedUser = true;

            Context.SaveChanges();
        }
    }
}
