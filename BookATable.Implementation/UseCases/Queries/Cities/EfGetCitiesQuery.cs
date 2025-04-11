using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Cities
{
    public class EfGetCitiesQuery : EfUseCase, IGetCitiesQuery
    {
        private readonly IMapper _mapper;

        public EfGetCitiesQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 18;

        public string Name => "Search cities";

        public PagedResponse<ResponseCityDTO> Execute(SearchCityDTO data)
        {
            var query = Context.Cities.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (!string.IsNullOrEmpty(data.Name))
            {
                query = query.Where(x => x.Name.Contains(data.Name));
            }

            if (data.ZipCode.HasValue)
            {
                query = query.Where(x => x.ZipCode == data.ZipCode);
            }


            return query.AsPagedReponse<City, ResponseCityDTO>(data, _mapper);
        }
    }
}
