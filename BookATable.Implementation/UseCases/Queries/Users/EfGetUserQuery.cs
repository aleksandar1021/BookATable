using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Users;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Users
{
    public class EfGetUserQuery : EfFindUseCase<UserResultDTO, User>, IGetUserQuery
    {
        public EfGetUserQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 7;

        public override string Name => "Get user by Id";
    }
}
