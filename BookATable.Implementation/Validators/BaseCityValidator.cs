using BookATable.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class BaseCityValidator<T> : AbstractValidator<T> where T : CreateCityDTO
    {
        public BaseCityValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("City name is required.")
                                .Matches("^[A-Z][a-zA-Z1-9\\s]{2,49}$")
                                .WithMessage("The City name must start with a capital letter and contain a minimum of 3 characters and a maximum of 50.");


            RuleFor(x => x.ZipCode).NotEmpty()
                                   .WithMessage("City Zip code is required.")
                                   .Must(x => x > 9999 && x < 100000)
                                   .WithMessage("Zip code must be in range 10000 - 100000.");
        }
    }
}
