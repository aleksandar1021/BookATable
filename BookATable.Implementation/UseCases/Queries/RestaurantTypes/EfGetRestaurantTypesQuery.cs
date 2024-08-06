using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.RestaurantTypes;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.RestaurantTypes
{
    public class EfGetRestaurantTypesQuery : EfUseCase, IGetRestaurantTypesQuery
    {
        private readonly IMapper _mapper;

        public EfGetRestaurantTypesQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 28;

        public string Name => "Search restaurant types";

        public PagedResponse<ResponseNamedEntityDTO> Execute(SearchNamedEntityDTO data)
        {
            var query = Context.RestaurantTypes.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (!string.IsNullOrEmpty(data.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(data.Name.ToLower()));
            }

            
            return query.AsPagedReponse<RestaurantType, ResponseNamedEntityDTO>(data, _mapper);
        }
    }
}
