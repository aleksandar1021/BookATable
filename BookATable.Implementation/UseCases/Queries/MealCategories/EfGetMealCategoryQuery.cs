using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.MealCategories
{
    public class EfGetMealCategoryQuery : EfFindUseCase<ResponseNamedEntityDTO, MealCategory>, IGetMealCategoryQuery
    {
        public EfGetMealCategoryQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 12;

        public override string Name => "Find meal category by id";
    }
}
