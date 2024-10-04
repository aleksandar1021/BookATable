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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Reservations
{
    public class EfUserDeleteReservationCommand : EfUseCase, IUserDeleteReservation
    {
        private IApplicationActor _actor;
        private IEmailSender _emailSender;
        public EfUserDeleteReservationCommand(Context context, IApplicationActor actor, IEmailSender emailSender) : base(context)
        {
            _actor = actor;
            _emailSender = emailSender;
        }

        public int Id => 94;

        public string Name => "User remove reservation";

        public void Execute(int data)
        {
            Reservation reservation = Context.Reservations.FirstOrDefault(x => x.Id == data);

            if (reservation == null || !reservation.IsActive)
            {
                throw new NotFoundException(nameof(Reservation), data);
            }

            if (_actor.Id != reservation.UserId)
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
                Body = $"You have deleted your reservation with the code: {reservation.ReservationCode}"
            };

            _emailSender.SendEmail(dto);
        }
    }
}
