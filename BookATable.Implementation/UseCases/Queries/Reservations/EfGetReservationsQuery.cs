using AutoMapper;
using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Reservations;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Reservations
{
    public class EfGetReservationsQuery : EfUseCase, IGetReservationsQuery
    {
        private readonly IMapper _mapper;
        private IApplicationActor _actor;
        public EfGetReservationsQuery(Context context, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 65;

        public string Name => "Search reservations";

        public PagedResponse<ResponseReservationDTO> Execute(SearchReservationDTO data)
        {
            var query = Context.Reservations.Where(x => x.IsActive).AsQueryable();

            data.UserId = data.UserId == null ? 0 : data.UserId;

            if(_actor.Id != data.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId);
            }

            if (data.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == data.UserId);
            }

            if (data.NumberOfGuests.HasValue)
            {
                query = query.Where(x => x.NumberOfGuests <= data.NumberOfGuests);
            }

            

            if (data.DateFrom.HasValue)
            {
                query = query.Where(x => x.Date >= data.DateFrom);
            }

            if (data.DateTo.HasValue)
            {
                query = query.Where(x => x.Date <= data.DateTo);
            }

            if (!string.IsNullOrEmpty(data.Note))
            {
                query = query.Where(x => x.Note.ToLower().Contains(data.Note.ToLower()));
            }

            if (!string.IsNullOrEmpty(data.RestaurantName))
            {
                query = query.Where(x => x.Restaurant.Name.Contains(data.RestaurantName));
            }

            if (!string.IsNullOrEmpty(data.UserEmail))
            {
                query = query.Where(x => x.User.Email.Contains(data.UserEmail));
            }

            if (!string.IsNullOrEmpty(data.UserFirstName))
            {
                query = query.Where(x => x.User.FirstName.Contains(data.UserFirstName));
            }

            if (!string.IsNullOrEmpty(data.UserLastName))
            {
                query = query.Where(x => x.User.LastName.Contains(data.UserLastName));
            }

            if (data.Sorts.Any(x => x.SortProperty == "createdAt"))
            {
                if (data.Sorts.FirstOrDefault(x => x.SortProperty == "createdAt").Direction == SortDirection.Asc)
                {
                    query = query.OrderBy(x => x.CreatedAt);
                }
                else
                {
                    query = query.OrderByDescending(x => x.CreatedAt);
                }
            }

            return query.AsPagedReponse<Reservation, ResponseReservationDTO>(data, _mapper);
        }
    }
}
