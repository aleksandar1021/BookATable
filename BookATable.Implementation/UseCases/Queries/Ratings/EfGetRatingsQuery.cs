using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Ratings;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Ratings
{
    public class EfGetRatingsQuery : EfUseCase, IGetRatingsQuery
    {
        private readonly IMapper _mapper;
        public EfGetRatingsQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 60;

        public string Name => "Search ratings";

        public PagedResponse<ResponseRatingDTO> Execute(SearchRatingDTO data)
        {
            var query = Context.Ratings.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (data.Rate.HasValue)
            {
                query = query.Where(x => x.Rate == data.Rate);
            }

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId);
            }

            if (data.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == data.UserId);
            }

            if (!string.IsNullOrEmpty(data.Message))
            {
                query = query.Where(x => x.Message.Contains(data.Message));
            }

            if (!string.IsNullOrEmpty(data.RestaurantName))
            {
                query = query.Where(x => x.Restaurant.Name.Contains(data.RestaurantName));
            }

            if (!string.IsNullOrEmpty(data.UserName))
            {
                query = query.Where(x => x.User.FirstName.Contains(data.UserName));
            }

            return query.AsPagedReponse<Rating, ResponseRatingDTO>(data, _mapper);
        }
    }
}
