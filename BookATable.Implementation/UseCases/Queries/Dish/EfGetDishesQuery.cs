using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Dish;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookATable.Implementation.UseCases.Queries.Dish
{
    public class EfGetDishesQuery : EfUseCase, IGetDishesQuery
    {
        private readonly IMapper _mapper;
        public EfGetDishesQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 55;

        public string Name => "Search dishes";

        public PagedResponse<ResponseDishDTO> Execute(SearchDishDTO data)
        {
            var query = Context.Dishs.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (!string.IsNullOrEmpty(data.Name))
            {
                query = query.Where(x => x.Name.Contains(data.Name));
            }

            if (!string.IsNullOrEmpty(data.Description))
            {
                query = query.Where(x => x.Description.Contains(data.Description));
            }

            if (!string.IsNullOrEmpty(data.RestaurantName))
            {
                query = query.Where(x => x.Restaurant.Name.Contains(data.RestaurantName));
            }

            if (data.Price.HasValue)
            {
                query = query.Where(x => x.Price < data.Price);
            }

            if (data.RestaurantId.HasValue)
            {
                query = query.Where(x => x.RestaurantId == data.RestaurantId);
            }

            return query.AsPagedReponse<Domain.Tables.Dish, ResponseDishDTO>(data, _mapper);
        }
    }
}
