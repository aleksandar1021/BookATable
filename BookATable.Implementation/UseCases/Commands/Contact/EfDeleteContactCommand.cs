using BookATable.Application.UseCases.Commands.Contact;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Contact
{
    public class EfDeleteContactCommand : EfUseCase, IDeleteContactCommand
    {
        public EfDeleteContactCommand(Context context) : base(context)
        {
        }

        public int Id => 87;

        public string Name => "Delete contact";

        public void Execute(int data)
        {
            Domain.Tables.Contact contact = Context.Contacts.FirstOrDefault(x => x.Id == data);

            if (contact == null || !contact.IsActive)
            {
                throw new NotFoundException(nameof(Domain.Tables.Contact), data);
            }

            contact.IsActive = false;
            Context.SaveChanges();
        }
    }
}
