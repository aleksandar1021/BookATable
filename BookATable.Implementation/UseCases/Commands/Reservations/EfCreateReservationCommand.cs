using BCrypt.Net;
using BookATable.Application.DTO;
using BookATable.Application.Email;
using BookATable.Application.UseCases.Commands.Reservations;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookATable.Implementation.UseCases.Commands.Reservations
{
    public class EfCreateReservationCommand : EfUseCase, ICreateReservationCommand
    {
        private IEmailSender _emailSender;
        private CreateReservationValidator _validator;
        public EfCreateReservationCommand(Context context, CreateReservationValidator validator, IEmailSender emailSender) : base(context)
        {
            _validator = validator;
            _emailSender = emailSender;
        }

        public int Id => 61;

        public string Name => "Create reservation";

        public void Execute(CreateReservationDTO data)
        {
            _validator.ValidateAndThrow(data);

            string code = Functions.GenerateRandomCode(8);

            Restaurant targetRestaurant = Context.Restaurants.FirstOrDefault(x => x.Id == data.RestaurantId);

            if(data.TimeHour < targetRestaurant.WorkFromHour || data.TimeHour >= targetRestaurant.WorkUntilHour)
            {
                throw new ConflictException("The restaurant is not open at that time.");
            }

            DateOnly targetDate = data.Date;

            foreach(var date in targetRestaurant.SpecificClosedDays)
            {
                if(targetDate > date.ClosedTo && targetDate < date.ClosedFrom) 
                {
                    throw new ConflictException($"The restaurant is not open at that date. Reserve restaurant after {date.ClosedTo}");
                }
            }

            string dayOfWeek = targetDate.DayOfWeek.ToString();

            string message = "";
            int i = 0;
            foreach (var day in targetRestaurant.RegularClosedDays)
            {
                if(targetRestaurant.RegularClosedDays.Count() == 1)
                {
                    message = day.DayOfWeek.ToString();
                }
                else if(targetRestaurant.RegularClosedDays.Count() == i)
                {
                    message += day.DayOfWeek.ToString();
                }
                else
                {
                    message += day.DayOfWeek.ToString() + ", ";
                }
                i++;
            }

            foreach (var day in targetRestaurant.RegularClosedDays)
            {
                if(day.DayOfWeek.ToString() == dayOfWeek)
                {
                    throw new ConflictException($"The restaurant is not open at that day. The restaurant is closed on: {message}");
                }
            }

            Reservation reservation = new Reservation
            {
                UserId = data.UserId,
                RestaurantId = data.RestaurantId,
                NumberOfGuests = data.NumberOfGuests,
                TimeHour = data.TimeHour,
                TimeMinute = data.TimeMinute,
                Note = data.Note,
                Date = data.Date,
                ReservationCode = BCrypt.Net.BCrypt.HashPassword(code),
                IsRealised = false
            };


            var body = "Save this email and show it to a restaurant employee when you go to a restaurant. Use reservation code below for identification";
            body += $"<br><br>Reservation code: {code}";
            body += $"<br><br>Restaurant: {targetRestaurant.Name}";
            body += $"<br><br>Restaurant city: {targetRestaurant.Address.City.Name}";
            body += $"<br><br>Restaurant addres: {targetRestaurant.Address.AddressOfPlace}";
            if(targetRestaurant.Address.AddressOfPlace != null)
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
            body += $"<br><br>Numbers of guests: {data.NumberOfGuests}";
            body += $"<br><br>Date: {data.Date}";
            body += $"<br><br>Time: {data.TimeHour}:{data.TimeMinute} h";
            body += $"<br><br>Note : {data.Note}";

           

            Context.Reservations.Add(reservation);
            Context.SaveChanges();

            var targetUserEmail = Context.Users.Where(x => x.Id == data.UserId).Select(x => x.Email).FirstOrDefault();

            EmailDTO dto = new EmailDTO
            {
                SendTo = targetUserEmail,
                Body = body,
                Subject = $"Successful booking with code: {code}"
            };

            _emailSender.SendEmail(dto);
        }
    }
}
