using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Addresses;
using BookATable.Application.UseCases.Commands.Appendices;
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

namespace BookATable.Implementation.UseCases.Commands.Appendices
{
    public class EfUpdateAppendiceCommand : EfUseCase, IUpdateAppendiceCommand
    {
        private UpdateAppendiceValidator _validator;
        public EfUpdateAppendiceCommand(Context context, UpdateAppendiceValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 30;

        public string Name => "Update appendice";

        public void Execute(UpdateNamedEntity data)
        {
            Appendice appendice = Context.Appendices.FirstOrDefault(x => x.Id == data.Id);

            if (appendice == null || !appendice.IsActive) 
            {
                throw new NotFoundException(nameof(Appendice), data.Id);
            }
            _validator.ValidateAndThrow(data);

            appendice.Name = data.Name;
            appendice.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
