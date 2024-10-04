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
    public class EfAccepteReservationCommand : EfUseCase, IAccepteReservationCommand
    {
        private IEmailSender _emailSender;
        private IApplicationActor _actor;
        public EfAccepteReservationCommand(Context context, IApplicationActor actor, IEmailSender emailSender) : base(context)
        {
            _actor = actor;
            _emailSender = emailSender;
        }

        public int Id => 88;

        public string Name => "Accepte reservation";

        public void Execute(int data)
        {
            Reservation targetReservation = Context.Reservations.FirstOrDefault(x => x.Id == data);

            if (targetReservation == null || !targetReservation.IsActive)
            {
                throw new NotFoundException(nameof(Reservation), data);
            }

            Restaurant targetRestaurant = targetReservation.Restaurant;

            if(targetRestaurant.UserId != _actor.Id)
            {
                throw new ConflictException("You do not have privileges for this action");
            }

            targetReservation.IsAccepted = true;
            Context.SaveChanges();

            
            var body = "Congratulations, your reservation has been successfully accpeted. Save this email and show it to a restaurant employee when you go to a restaurant. Use reservation code below for identification";
            body += $"<br><br>Reservation code: {targetReservation.ReservationCode}";
            body += $"<br><br>Restaurant: {targetRestaurant.Name}";
            body += $"<br><br>Restaurant city: {targetRestaurant.Address.City.Name}";
            body += $"<br><br>Restaurant addres: {targetRestaurant.Address.AddressOfPlace}";
            if (targetRestaurant.Address.AddressOfPlace != null)
            {
                body += $"<br><br>Restaurant address number: {targetRestaurant.Address.Number}";
            }
            if (targetRestaurant.Address.Place != null)
            {
                body += $"<br><br>Restaurant address place: {targetRestaurant.Address.Place}";
            }
            if (targetRestaurant.Address.Floor != null)
            {
                body += $"<br><br>Restaurant is on floor: {targetRestaurant.Address.Floor}";
            }
            if (targetRestaurant.Address.Description != null)
            {
                body += $"<br><br>Restaurant place description: {targetRestaurant.Address.Description}";
            }
            body += $"<br><br>Numbers of guests: {targetReservation.NumberOfGuests}";
            body += $"<br><br>Date: {targetReservation.Date}";
            body += $"<br><br>Time: {targetReservation.Time}:{targetReservation.Time} h";
            if (targetReservation.Note != null)
            {
                body += $"<br><br>Note : {targetReservation.Note}";
            }


            var targetUserEmail = Context.Users.Where(x => x.Id == targetReservation.UserId).Select(x => x.Email).FirstOrDefault();

            EmailDTO dto = new EmailDTO
            {
                SendTo = targetUserEmail,
                Body = body,
                Subject = $"Successful accpeted booking with code: {targetReservation.ReservationCode}"
            };

            _emailSender.SendEmail(dto);



        }
    }
}
