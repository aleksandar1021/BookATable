using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.ReservationAppendices;
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

namespace BookATable.Implementation.UseCases.Commands.ReservationAppendices
{
    public class EfUpdateReservationAppendiceCommand : EfUseCase, IUpdateReservationAppendiceCommand
    {
        private CreateReservationAppendiceValidator _validator;
        public EfUpdateReservationAppendiceCommand(Context context, CreateReservationAppendiceValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 67;

        public string Name => "Update appendices for restaurant";

        public void Execute(UpdateReservationAppendiceDTO data)
        {
            ReservationAppendice reservationAppendice = Context.ReservationAppendices.FirstOrDefault(x => x.Id == data.Id);

            if (reservationAppendice == null || !reservationAppendice.IsActive)
            {
                throw new NotFoundException(nameof(ReservationAppendice), data.Id);
            }

            _validator.ValidateAndThrow(data);

            var oldReservationAppendices = Context.ReservationAppendices.Where(x => x.ReservationId == data.ReservationId);

            if(oldReservationAppendices.Any())
            {
                Context.ReservationAppendices.RemoveRange(oldReservationAppendices);
            }

            List<ReservationAppendice> reservationAppendices = new List<ReservationAppendice>();

            foreach (var appendice in data.AppendiceIds)
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
