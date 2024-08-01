using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Ratings;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using BookATable.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Ratings
{
    public class EfUpdateRatingCommand : EfUseCase, IUpdateRatingCommand
    {
        private CreateRatingValidator _validator;
        public EfUpdateRatingCommand(Context context, CreateRatingValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 57;

        public string Name => "update rating";

        public void Execute(UpdateRatingDTO data)
        {
            Rating rating = Context.Ratings.FirstOrDefault(x => x.Id == data.Id);

            if (rating == null || !rating.IsActive)
            {
                throw new NotFoundException(nameof(Rating), data.Id);
            }

            rating.Rate = data.Rate;
            rating.UserId = data.UserId;
            rating.RestaurantId = data.RestaurantId;
            rating.Message = data.Message;
            rating.UpdatedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
