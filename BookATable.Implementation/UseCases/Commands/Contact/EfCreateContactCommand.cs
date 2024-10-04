using BookATable.Application.DTO;
using BookATable.Application.Email;
using BookATable.Application.UseCases.Commands.Contact;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Contact
{
    public class EfCreateContactCommand : EfUseCase, ICreateContactCommand
    {
        private IUserEmailSender _emailSender;
        private ContactValidator _validator;
        public EfCreateContactCommand(Context context, ContactValidator validator, IUserEmailSender emailSender) : base(context)
        {
            _validator = validator;
            _emailSender = emailSender;
        }

        public int Id => 85;

        public string Name => "Creeate contact";

        public void Execute(CreateContactDTO data)
        {
            _validator.ValidateAndThrow(data);

            Domain.Tables.Contact message = new Domain.Tables.Contact
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Subject = data.Subject,
                Message = data.Message
            };

            Context.Contacts.Add(message);
            Context.SaveChanges();

            string body = "<b>" + data.FirstName + "</b><br>" + data.Message;



            UserEmailDTO dto = new UserEmailDTO
            {
                Email = data.Email,
                Subject = data.Subject,
                SendTo = "bookatable12345@gmail.com",
                Body = body
            };

            _emailSender.SendUserEmail(dto);
        }
    }
}
