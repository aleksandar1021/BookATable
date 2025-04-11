using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Appendices;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Appendices
{
    public class EfCreateAppendiceCommand : EfUseCase, ICreateAppendiceCommand
    {
        private CreateAppendiceValidator _validator;
        public EfCreateAppendiceCommand(Context context, CreateAppendiceValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "Create appendice";


        public void Execute(CreateNamedEntity data)
        {
            _validator.ValidateAndThrow(data);

            Appendice appendice = new Appendice
            {
                Name = data.Name
            };

            Context.Appendices.Add(appendice);
            Context.SaveChanges();
        }

    }
}
