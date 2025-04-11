using AutoMapper;
using BookATable.Application.DTO;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.MealCategoryRestaurants
{
    public class EfGetMealCategoryRestaurantQuery : EfFindUseCase<ResponseMealCategoryRestaurantDTO, MealCategoryRestaurant>, IGetMealCategoryRestaurantQuery
    {
        public EfGetMealCategoryRestaurantQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 44;

        public override string Name => "Find meal category rastaurant by id";
    }
}
