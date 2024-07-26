using BookATable.Application.DTO;
using BookATable.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.MealCategoryRestaurants
{
    public interface IGetMealCategoryRestaurantQuery : IQuery<ResponseMealCategoryRestaurantDTO, int>
    {
    }
}
