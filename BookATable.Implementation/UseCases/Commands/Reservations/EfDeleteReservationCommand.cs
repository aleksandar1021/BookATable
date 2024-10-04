using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.Email;
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
        private IEmailSender _emailSender;

        private IApplicationActor _actor;
        public EfDeleteReservationCommand(Context context, IApplicationActor actor, IEmailSender emailSender) : base(context)
        {
            _actor = actor;
            _emailSender = emailSender;
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

            if(_actor.Id != reservation.Restaurant.UserId)
            {
                throw new UnauthorizedAccessException();
            }


            reservation.IsActive = false;
            reservation.ReservationAppendices.ToList().ForEach(r => r.IsActive = false);

            Context.SaveChanges();

            EmailDTO dto = new EmailDTO
            {
                SendTo = reservation.User.Email,
                Subject = "Your reservation has been deleted",
                Body = $"Your reservation has been deleted by the restaurant, with code: {reservation.ReservationCode}"
            };

            _emailSender.SendEmail(dto);
        }
    }
}
