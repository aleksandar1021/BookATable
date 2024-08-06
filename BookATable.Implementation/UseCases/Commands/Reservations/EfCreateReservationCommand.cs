using BCrypt.Net;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Reservations;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
