using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Dish;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Dish
{
    public class EfGetDishQuery : EfFindUseCase<ResponseDishDTO, Domain.Tables.Dish>, IGetDishQuery
    {
        public EfGetDishQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 54;

        public override string Name => "Find dish by Id";
    }
}
