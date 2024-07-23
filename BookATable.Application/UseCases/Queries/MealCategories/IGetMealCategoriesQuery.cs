using BookATable.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.UseCases.Queries.MealCategories
{
    public interface IGetMealCategoriesQuery : IQuery<PagedResponse<ResponseNamedEntityDTO>, SearchNamedEntityDTO>
    {
    }
}
