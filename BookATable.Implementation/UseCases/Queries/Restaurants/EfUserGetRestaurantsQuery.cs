using AutoMapper;
using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Restaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Restaurants
{
    public class EfUserGetRestaurantsQuery : EfUseCase, IUserGetRestourants
    {
        private readonly IMapper _mapper;
        private IApplicationActor _actor;

        public EfUserGetRestaurantsQuery(Context context, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 92;

        public string Name => "Search users restaurants";

        public PagedResponse<ResponseRestaurantDTOAdmin> Execute(SearchRestaurantDTO data)
        {
            if(_actor.Id != data.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            var query = Context.Restaurants.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id.Value);
            }

            if (!string.IsNullOrEmpty(data.Name))
            {
                query = query.Where(x => x.Name.Contains(data.Name));
            }

            if (data.WorkFromHour.HasValue)
            {
                query = query.Where(x => x.WorkFromHour == data.WorkFromHour);
            }

            if (data.WorkUntilHour.HasValue)
            {
                query = query.Where(x => x.WorkUntilHour == data.WorkUntilHour.Value);
            }

            if (data.WorkFromMinute.HasValue)
            {
                query = query.Where(x => x.WorkFromMinute == data.WorkFromMinute.Value);
            }

            if (data.WorkUntilMinute.HasValue)
            {
                query = query.Where(x => x.WorkUntilMinute == data.WorkUntilMinute.Value);
            }

            if (data.AddressId.HasValue)
            {
                query = query.Where(x => x.AddressId == data.AddressId.Value);
            }

            if (data.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == data.UserId.Value);
            }

            if (data.RestaurantTypeId.HasValue)
            {
                query = query.Where(x => x.RestaurantTypeId == data.RestaurantTypeId.Value);
            }

            if (!string.IsNullOrEmpty(data.Description))
            {
                query = query.Where(x => x.Description.Contains(data.Description));
            }

            if (data.MaxNumberOfGuests.HasValue)
            {
                query = query.Where(x => x.MaxNumberOfGuests == data.MaxNumberOfGuests.Value);
            }

            if (data.TimeInterval.HasValue)
            {
                query = query.Where(x => x.TimeInterval == data.TimeInterval.Value);
            }

            if (data.IsActivated.HasValue)
            {
                query = query.Where(x => x.IsActivated == data.IsActivated.Value);
            }

            if (!string.IsNullOrEmpty(data.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(data.Keyword.ToLower()) || x.User.Email.ToLower().Contains(data.Keyword.ToLower()));
            }

            return query.AsPagedReponse<Restaurant, ResponseRestaurantDTOAdmin>(data, _mapper);
        }
    }
}
