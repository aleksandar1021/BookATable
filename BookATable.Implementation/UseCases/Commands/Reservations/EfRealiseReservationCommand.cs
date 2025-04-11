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
    public class EfRealiseReservationCommand : EfUseCase, IRealiseReservationCommand
    {
        private IEmailSender _emailSender;
        private IApplicationActor _actor;
        public EfRealiseReservationCommand(Context context, IApplicationActor actor, IEmailSender emailSender) : base(context)
        {
            _actor = actor;
            _emailSender = emailSender;
        }

        public int Id => 89;

        public string Name => "Realise reservation";

        public void Execute(int data)
        {
            Reservation targetReservation = Context.Reservations.FirstOrDefault(x => x.Id == data);

            if (targetReservation == null || !targetReservation.IsActive)
            {
                throw new NotFoundException(nameof(Reservation), data);
            }

            Restaurant targetRestaurant = targetReservation.Restaurant;

            if (targetRestaurant.UserId != _actor.Id)
            {
                throw new ConflictException("You do not have privileges for this action");
            }

            var body = "Thank you for making the reservation. Now you can rate the restaurant you've been to.";

            EmailDTO dto = new EmailDTO
            {
                SendTo = targetReservation.User.Email,
                Body = body,
                Subject = $"Successful realised booking with code: {targetReservation.ReservationCode}"
            };

            _emailSender.SendEmail(dto);

            targetReservation.IsRealised = true;
            Context.SaveChanges();
        }
    }
}
