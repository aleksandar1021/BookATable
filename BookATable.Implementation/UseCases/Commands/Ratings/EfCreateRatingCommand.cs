using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Ratings;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Ratings
{
    public class EfCreateRatingCommand : EfUseCase, ICreateRatingCommand
    {
        private CreateRatingValidator _validator;
        public EfCreateRatingCommand(Context context, CreateRatingValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 56;

        public string Name => "Create rating";

        public void Execute(CreateRatingDTO data)
        {
            _validator.ValidateAndThrow(data);

            Rating rating = new Rating
            {
                Rate = data.Rate,
                UserId = data.UserId,
                Message = data.Message,
                RestaurantId = data.RestaurantId
            };
            Context.Ratings.Add(rating);
            Context.SaveChanges();
        }
    }
}
