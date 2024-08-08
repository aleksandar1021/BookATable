using BookATable.Application.DTO;
using BookATable.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDTO>
    {
        public CreateReservationValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.TimeHour)
                .InclusiveBetween(0, 23)
                .WithMessage("Time hour must be between 0 and 23.");


            RuleFor(x => x.TimeMinute)
                .InclusiveBetween(0, 59)
                .WithMessage("Time minute must be between 0 and 59.");


            RuleFor(x => x.UserId)
                   .NotEmpty()
                   .WithMessage("User is required.")
                   .Must(x => ctx.Users.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("User does not exists.");


            RuleFor(x => x.RestaurantId)
                   .NotEmpty()
                   .WithMessage("Restaurant is required.")
                   .Must(x => ctx.Restaurants.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("Restaurant does not exists.");

            RuleFor(x => x.NumberOfGuests)
                   .NotEmpty()
                   .WithMessage("Number of guests is required.")
                   .InclusiveBetween(1, 255)
                   .WithMessage("Number of guests must be between 0 and 255.");


            RuleFor(x => x.Note)
                .MaximumLength(300)
                .WithMessage("Note maximum length is 300.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Dates is required")
                .Must(x => x > DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Date must be in future.");

     
        }
    }
}
