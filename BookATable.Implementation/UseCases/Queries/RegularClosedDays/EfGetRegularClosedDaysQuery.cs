using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.RegularClosedDays;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.RegularClosedDays
{
    public class EfGetRegularClosedDaysQuery : EfUseCase, IGetRegularClosedDaysQuery
    {
        private readonly IMapper _mapper;
        public EfGetRegularClosedDaysQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 79;

        public string Name => "Search regular closed days";

        public PagedResponse<ResponseRegularClosedDaysDTO> Execute(SearchRegularClosedDays data)
        {
            var query = Context.RegularClosedDays.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId);
            }

            

            return query.AsPagedReponse<Domain.Tables.RegularClosedDays, ResponseRegularClosedDaysDTO>(data, _mapper);
        }
    }
}
