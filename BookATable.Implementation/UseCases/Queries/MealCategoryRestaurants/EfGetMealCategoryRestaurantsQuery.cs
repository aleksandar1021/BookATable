using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.MealCategoryRestaurants;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.MealCategoryRestaurants
{
    public class EfGetMealCategoryRestaurantsQuery : EfUseCase, IGetMealCategoryRestaurantsQuery
    {
        private IMapper _mapper;
        public EfGetMealCategoryRestaurantsQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 45;

        public string Name => "Search meal category restaurants";

        public PagedResponse<ResponseMealCategoryRestaurantDTO> Execute(SearchMealCategoryRestaurantDTO data)
        {
            var query = Context.MealCategoryRestaurants.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id.Value);
            }

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId.Value);
            }

            if (data.MealCategoryId.HasValue)
            {
                query = query.Where(x => x.MealCategoryId == data.MealCategoryId.Value);
            }



            return query.AsPagedReponse<MealCategoryRestaurant, ResponseMealCategoryRestaurantDTO>(data, _mapper);
        }
    }
}
