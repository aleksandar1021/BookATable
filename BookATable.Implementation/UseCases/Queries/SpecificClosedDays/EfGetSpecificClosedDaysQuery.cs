using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.SpecificClosedDays;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.SpecificClosedDays
{
    public class EfGetSpecificClosedDaysQuery : EfUseCase, IGetSpecificClosedDaysQuery
    {
        private readonly IMapper _mapper;
        public EfGetSpecificClosedDaysQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 84;

        public string Name => "Search specific closed days";

        public PagedResponse<ResponseSpecificClosedDays> Execute(SearchSpecificClosedDays data)
        {
            var query = Context.SpecificClosedDays.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id.Value);
            }

            if (!string.IsNullOrEmpty(data.RestaurantName))
            {
                query = query.Where(x => x.Restaurant.Name.ToLower().Contains(data.RestaurantName.ToLower()));
            }

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId);
            }

            if (data.ClosedTo.HasValue)
            {
                query = query.Where(x => x.ClosedTo < data.ClosedTo);
            }

            if (data.ClosedFrom.HasValue)
            {
                query = query.Where(x => x.ClosedFrom > data.ClosedFrom);
            }

            

            return query.AsPagedReponse<Domain.Tables.SpecificClosedDays, ResponseSpecificClosedDays>(data, _mapper);
        }
    }
}
