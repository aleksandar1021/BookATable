using BookATable.Application.UseCases.Commands.Ratings;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Commands.Ratings
{
    public class EfDeleteRatingCommand : EfUseCase, IDeleteRatingCommand
    {
        public EfDeleteRatingCommand(Context context) : base(context)
        {
        }

        public int Id => 58;

        public string Name => "Delete rating";

        public void Execute(int data)
        {
            Rating rating = Context.Ratings.FirstOrDefault(x => x.Id == data);

            if (rating == null || !rating.IsActive)
            {
                throw new NotFoundException(nameof(Rating), data);
            }

            rating.IsActive = false;

            Context.SaveChanges();
        }
    }
}
