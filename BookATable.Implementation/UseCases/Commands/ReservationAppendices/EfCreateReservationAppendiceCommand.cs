using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.ReservationAppendices;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.ReservationAppendices
{
    public class EfCreateReservationAppendiceCommand : EfUseCase, ICreateReservationAppendiceCommand
    {
        private CreateReservationAppendiceValidator _validator;
        public EfCreateReservationAppendiceCommand(Context context, CreateReservationAppendiceValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 66;

        public string Name => "Create appendice for reservation";

        public void Execute(CreateReservationAppendiceDTO data)
        {
            _validator.ValidateAndThrow(data);

            List<ReservationAppendice> reservationAppendices = new List<ReservationAppendice>();

            foreach(var appendice in data.AppendiceIds)
            {
                reservationAppendices.Add(new ReservationAppendice()
                {
                    AppendiceId = appendice,
                    ReservationId = data.ReservationId
                });
            }

            Context.ReservationAppendices.AddRange(reservationAppendices);
            Context.SaveChanges();
        }
    }
}
