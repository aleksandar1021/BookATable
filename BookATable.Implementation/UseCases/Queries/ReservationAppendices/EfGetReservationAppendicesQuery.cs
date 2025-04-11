using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.ReservationAppendices;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.ReservationAppendices
{
    public class EfGetReservationAppendicesQuery : EfUseCase, IGetReservationAppendicesQuery
    {
        private readonly IMapper _mapper;
        public EfGetReservationAppendicesQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 69;

        public string Name => "Search restaurant appendices";

        public PagedResponse<ResponseReservationAppendiceDTO> Execute(SearchReservationAppendiceDTO data)
        {
            var query = Context.ReservationAppendices.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (data.AppendiceId.HasValue)
            {
                query = query.Where(x => x.AppendiceId == data.AppendiceId);
            }

            if (data.ReservationId.HasValue)
            {
                query = query.Where(x => x.ReservationId == data.ReservationId);
            }

            if (data.AppendiceId.HasValue)
            {
                query = query.Where(x => x.AppendiceId == data.AppendiceId);
            }

            if (!string.IsNullOrEmpty(data.RestaurantName))
            {
                query = query.Where(x => x.Reservation.Restaurant.Name.ToLower().Contains(data.RestaurantName.ToLower()));
            }

            if (!string.IsNullOrEmpty(data.AppendiceName))
            {
                query = query.Where(x => x.Appendice.Name.Contains(data.AppendiceName));
            }

            

            return query.AsPagedReponse<ReservationAppendice, ResponseReservationAppendiceDTO>(data, _mapper);
        }
    }
}
