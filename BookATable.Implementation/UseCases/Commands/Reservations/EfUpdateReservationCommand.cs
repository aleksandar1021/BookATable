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
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookATable.Implementation.UseCases.Commands.Reservations
{
    public class EfUpdateReservationCommand : EfUseCase, IUpdateReservationCommand
    {
        private CreateReservationValidator _validator;
        public EfUpdateReservationCommand(Context context, CreateReservationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 62;

        public string Name => "Update reservation";

        public void Execute(UpdateReservationDTO data)
        {
            Reservation reservation = Context.Reservations.FirstOrDefault(x => x.Id == data.Id);

            if (reservation == null || !reservation.IsActive)
            {
                throw new NotFoundException(nameof(Reservation), data.Id);
            }

            _validator.ValidateAndThrow(data);

            Restaurant targetRestaurant = Context.Restaurants.FirstOrDefault(x => x.Id == data.RestaurantId);

            if (data.TimeHour < targetRestaurant.WorkFromHour || data.TimeHour >= targetRestaurant.WorkUntilHour)
            {
                throw new ConflictException("The restaurant is not open at that time.");
            }

            reservation.UserId = data.UserId;
            reservation.RestaurantId = data.RestaurantId;
            reservation.NumberOfGuests = data.NumberOfGuests;
            reservation.TimeHour = data.TimeHour;
            reservation.TimeMinute = data.TimeMinute;
            reservation.Note = data.Note;
            reservation.Date = data.Date;
            reservation.IsRealised = data.IsRealised;
            reservation.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
