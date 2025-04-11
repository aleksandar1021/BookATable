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
    public class BaseRestaurantValidator<T> : AbstractValidator<T> where T : CreateRestaurantDTO
    {
        public BaseRestaurantValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
            .NotEmpty()
            .Matches("^[A-ZŠĐČĆŽ][a-zšđčćžA-ZŠĐČĆŽ1-9\\s]{2,69}$")
            .WithMessage("The name must start with a capital letter and contain a minimum of 3 characters and a maximum of 70.");

            RuleFor(x => x.WorkFromHour)
                .InclusiveBetween(0, 23)
                .WithMessage("Work from hour must be between 0 and 23.");

            RuleFor(x => x.WorkUntilHour)
                .InclusiveBetween(0, 23)
                .WithMessage("Work until hour must be between 0 and 23.");

            RuleFor(x => x.WorkFromMinute)
                .InclusiveBetween(0, 59)
                .WithMessage("Work from minute must be between 0 and 59.");

            RuleFor(x => x.WorkUntilMinute)
                .InclusiveBetween(0, 59)
                .WithMessage("Work until minute must be between 0 and 59.");


            //RuleFor(x => x.AddressId)
            //       .NotEmpty()
            //       .WithMessage("Address is required.")
            //       .Must(x => ctx.Addresses.Any(a => a.Id == x && a.IsActive))
            //       .WithMessage("Address does not exists.");


            //RuleFor(x => x.UserId)
            //       .NotEmpty()
            //       .WithMessage("User is required.")
            //       .Must(x => ctx.Users.Any(a => a.Id == x && a.IsActive))
            //       .WithMessage("User does not exists.");

            RuleFor(x => x.RestaurantTypeId)
                   .NotEmpty()
                   .WithMessage("Restaurant type is required.")
                   .Must(x => ctx.RestaurantTypes.Any(a => a.Id == x && a.IsActive))
                   .WithMessage("Restaurant type does not exists.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description maximum length is 500.");

            RuleFor(x => x.MaxNumberOfGuests)
                .NotEmpty()
                .WithMessage("Max number of guests is required")
                .InclusiveBetween(0, 5000)
                .WithMessage("Max number of guests must be between 0 and 5000.");

            RuleFor(x => x.TimeInterval)
                .InclusiveBetween(0, 60)
                .WithMessage("Time interval must be between 0 and 60.");

            //RuleFor(x => x.Images).NotEmpty()
            //        .WithMessage("Minimum one image is required.")
            //        .DependentRules(() =>
            //        {
            //            RuleForEach(x => x.Images).Must((x, fileName) =>
            //            {
            //                var path = Path.Combine("wwwroot", "temp", fileName);

            //                var exists = Path.Exists(path);

            //                return exists;
            //            }).WithMessage("File not exist.");
            //        });

           


        }
    }
}
