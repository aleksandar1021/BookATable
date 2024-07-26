using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.AppendiceRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.AppendiceRestaurants
{
    public class EfGetAppendiceRestaurantsQuery : EfUseCase, IGetAppendiceRestaurantsQuery
    {
        private IMapper _mapper;
        public EfGetAppendiceRestaurantsQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 50;

        public string Name => "Search restaurant appendices";

        public PagedResponse<ResponseAppendiceRestaurantDTO> Execute(SearchAppendiceRestaurantDTO data)
        {
            var query = Context.AppendiceRestaurants.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id.Value);
            }

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId.Value);
            }

            if (data.AppendiceId.HasValue)
            {
                query = query.Where(x => x.AppendiceId == data.AppendiceId.Value);
            }



            return query.AsPagedReponse<AppendiceRestaurant, ResponseAppendiceRestaurantDTO>(data, _mapper);
        }
    }
}
