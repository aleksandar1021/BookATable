using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Addresses
{
    public class EfGetAddresesQuery : EfUseCase, IGetAddressesQuery
    {
        private readonly IMapper _mapper;

        public EfGetAddresesQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 23;

        public string Name => "Search addresses";

        public PagedResponse<ResponseAddressDTO> Execute(SearchAddressDTO data)
        {
            var query = Context.Addresses.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (data.CityId.HasValue)
            {
                query = query.Where(x => x.CityId == data.CityId);
            }

            if (!string.IsNullOrEmpty(data.Place))
            {
                query = query.Where(x => x.Place.Contains(data.Place));
            }

            if (!string.IsNullOrEmpty(data.Address))
            {
                query = query.Where(x => x.AddressOfPlace.Contains(data.Address));
            }

            if (!string.IsNullOrEmpty(data.Description))
            {
                query = query.Where(x => x.Description.Contains(data.Description));
            }

            if (!string.IsNullOrEmpty(data.Number))
            {
                query = query.Where(x => x.Number.Contains(data.Number));
            }

            if (data.Latitude.HasValue)
            {
                query = query.Where(x => x.Latitude == data.Latitude);
            }

            if (data.Floor.HasValue)
            {
                query = query.Where(x => x.Floor == data.Floor);
            }

            if (data.Longitude.HasValue)
            {
                query = query.Where(x => x.Longitude == data.Longitude);
            }

            return query.AsPagedReponse<Address, ResponseAddressDTO>(data, _mapper);
        }
    }
}
