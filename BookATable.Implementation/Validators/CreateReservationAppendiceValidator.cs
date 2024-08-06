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
    public class CreateReservationAppendiceValidator : AbstractValidator<CreateReservationAppendiceDTO>
    {
        public CreateReservationAppendiceValidator(Context ctx)
        {
            RuleFor(x => x.ReservationId)
                   .NotEmpty()
                   .WithMessage("Reservation is required.")
                   .Must(x => ctx.Reservations.Any(a => a.Id == x))
                   .WithMessage("Reservation does not exists.");

            RuleFor(x => x.AppendiceIds)
                    .NotEmpty()
                    .WithMessage("Appendices are required.")
                    .Must(ids => ids.All(id => ctx.Appendices.Any(a => a.Id == id)))
                    .WithMessage("One or more appendices do not exist.");
        }
    }
}
