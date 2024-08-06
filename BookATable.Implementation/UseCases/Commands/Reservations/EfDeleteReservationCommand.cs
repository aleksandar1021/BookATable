using BookATable.Application.UseCases.Commands.Reservations;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Reservations
{
    public class EfDeleteReservationCommand : EfUseCase, IDeleteReservationCommand
    {
        public EfDeleteReservationCommand(Context context) : base(context)
        {
        }

        public int Id => 63;

        public string Name => "Delete reservation";

        public void Execute(int data)
        {
            Reservation reservation = Context.Reservations.FirstOrDefault(x => x.Id == data);

            if (reservation == null || !reservation.IsActive)
            {
                throw new NotFoundException(nameof(Reservation), data);
            }

            reservation.IsActive = false;
            reservation.ReservationAppendices.ToList().ForEach(r => r.IsActive = false);

            Context.SaveChanges();
        }
    }
}
