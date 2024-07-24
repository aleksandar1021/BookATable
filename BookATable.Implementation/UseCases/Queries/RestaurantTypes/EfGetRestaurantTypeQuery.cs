using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.RestaurantTypes;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.RestaurantTypes
{
    public class EfGetRestaurantTypeQuery : EfFindUseCase<ResponseNamedEntityDTO, RestaurantType>, IGetRestaurantTypeQuery
    {
        public EfGetRestaurantTypeQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 27;

        public override string Name => "Find restaurant type by Id";
    }
}
