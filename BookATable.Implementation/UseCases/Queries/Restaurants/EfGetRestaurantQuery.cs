using AutoMapper;
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
    public class EfGetRestaurantQuery : EfFindUseCase<ResponseRestaurantDTO, Restaurant>, IGetRestaurantQuery
    {
        public EfGetRestaurantQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 39;

        public override string Name => "Find restaurant by Id";
    }
}
