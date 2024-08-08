using BCrypt.Net;
using BookATable.Application.DTO;
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
        private CreateReservationValidator _validator;
        public EfCreateReservationCommand(Context context, CreateReservationValidator validator) : base(context)
        {
            _validator = validator;
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
                if(targetDate < date.ClosedTo || targetDate > date.ClosedFrom) 
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

            Context.Reservations.Add(reservation);
            Context.SaveChanges();
        }
    }
}
