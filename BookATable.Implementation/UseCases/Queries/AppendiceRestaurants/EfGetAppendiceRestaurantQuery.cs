using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.AppendiceRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.AppendiceRestaurants
{
    public class EfGetAppendiceRestaurantQuery : EfFindUseCase<ResponseAppendiceRestaurantDTO, AppendiceRestaurant>, IGetAppendiceRestaurantQuery
    {
        public EfGetAppendiceRestaurantQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 49;

        public override string Name => "Find restaurant appendice by Id";

      
    }
}
