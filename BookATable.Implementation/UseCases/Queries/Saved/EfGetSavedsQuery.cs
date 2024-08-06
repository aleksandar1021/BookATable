using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Saved;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Saved
{
    public class EfGetSavedsQuery : EfUseCase, IGetSavedsQuery
    {
        private readonly IMapper _mapper;
        public EfGetSavedsQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 72;

        public string Name => "Search saveds";

        public PagedResponse<ResponseSavedDTO> Execute(SearchSavedDTO data)
        {
            var query = Context.Saved.Where(x => x.IsActive).AsQueryable();

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

            if (!string.IsNullOrEmpty(data.UserFirstName))
            {
                query = query.Where(x => x.User.FirstName.ToLower().Contains(data.UserFirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(data.UserLastName))
            {
                query = query.Where(x => x.User.LastName.ToLower().Contains(data.UserLastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(data.UserEmail))
            {
                query = query.Where(x => x.User.Email.ToLower().Contains(data.UserEmail.ToLower()));
            }

            if (!string.IsNullOrEmpty(data.RestaurantName))
            {
                query = query.Where(x => x.Restaurant.Name.ToLower().Contains(data.RestaurantName.ToLower()));
            }

            return query.AsPagedReponse<Domain.Tables.Saved, ResponseSavedDTO>(data, _mapper);
        }
    }
}
