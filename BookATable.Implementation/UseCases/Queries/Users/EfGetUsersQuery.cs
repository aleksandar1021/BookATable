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
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        private readonly IMapper _mapper;
        public EfGetUsersQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public  int Id => 8;
        public string Name => "Search users";

        public PagedResponse<UserResultDTO> Execute(SearchUserDTO data)
        {
            
            var query = Context.Users.Where(x => x.IsActive && x.Email != "administrator@gmail.com").AsQueryable();

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

            if (!string.IsNullOrEmpty(data.Keyword))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(data.Keyword.Trim().ToLower()) ||
                                         x.LastName.ToLower().Contains(data.Keyword.Trim().ToLower()) ||
                                         x.Email.ToLower().Contains(data.Keyword.Trim().ToLower())
                                         );
            }

            return query.AsPagedReponse<User, UserResultDTO>(data, _mapper);
        }
    }
}
