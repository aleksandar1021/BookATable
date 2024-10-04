using AutoMapper;
using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Reservations;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Reservations
{
    public class EfGetReservationsForRestaurantQuery : EfUseCase, IGetReservationsForRestaurantQuery
    {
        private IApplicationActor _actor;
        private readonly IMapper _mapper;

        public EfGetReservationsForRestaurantQuery(Context context, IApplicationActor actor, IMapper mapper) : base(context)
        {
            _actor = actor;
            _mapper = mapper;
        }

        public int Id => 93;

        public string Name => "Get reservations for restaurants";

        public PagedResponse<ResponseReservationDTO> Execute(SearchReservationDTO data)
        {

            data.UserId = _actor.Id;

            if (_actor.Id != data.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            var query = Context.Reservations.Where(x => x.IsActive).AsQueryable();

            //if (data.UserId.HasValue)
            //{
            //    query = query.Where(x => x.UserId == data.UserId);
            //}

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId);
            }

            if (!string.IsNullOrEmpty(data.Keyword))
            {
                query = query.Where(x => x.User.FirstName.ToLower().Contains(data.Keyword.ToLower()) || x.User.Email.ToLower().Contains(data.Keyword.ToLower()) || x.User.LastName.ToLower().Contains(data.Keyword.ToLower()));
            }


            return query.AsPagedReponse<Reservation, ResponseReservationDTO>(data, _mapper);
        }
    }
}
