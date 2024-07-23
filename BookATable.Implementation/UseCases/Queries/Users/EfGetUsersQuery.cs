using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Users;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Users
{
    public class EfGetUsersQuery : EfFindUseCase<UserResultDTO, User>, IGetUsersQuery
    {
        private readonly IMapper _mapper;
        public EfGetUsersQuery(Context context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override int Id => 8;

        public override string Name => "Search users";

        public PagedResponse<UserResultDTO> Execute(SearchUserDTO data)
        {
            
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(data.Email))
            {
                query = query.Where(x => x.Email.Contains(data.Email));
            }
            if (!string.IsNullOrEmpty(data.FirstName))
            {
                query = query.Where(x => x.FirstName.Contains(data.FirstName));
            }
            if (!string.IsNullOrEmpty(data.LastName))
            {
                query = query.Where(x => x.LastName.Contains(data.LastName));
            }
            if (data.IsActivatedUser.HasValue)
            {
                query = query.Where(x => x.IsActivatedUser == data.IsActivatedUser.Value);
            }

            var users = query
                            .Include(x => x.Restaurants)
                                .ThenInclude(x => x.Address)
                                .ThenInclude(x => x.City)
                            .Include(x => x.Ratings)
                            .Include(x => x.Reservations)
                            .ToList();

            

            return Paginate(query, data);
        }

        protected virtual PagedResponse<UserResultDTO> Paginate(IQueryable<User> query, PagedSearch search)
        {
            return query.AsPagedReponse<User, UserResultDTO>(search, _mapper);
        }
    }



    public class AutomapperSearchUserQuery : EfGetUsersQuery
    {
        private readonly IMapper mapper;
        public AutomapperSearchUserQuery(Context context, IMapper mapper) : base(context, mapper)
        {
            this.mapper = mapper;
        }
        protected override PagedResponse<UserResultDTO> Paginate(IQueryable<User> query, PagedSearch search)
        {
            return query.AsPagedReponse<User, UserResultDTO>(search, mapper);
        }
    }
}
