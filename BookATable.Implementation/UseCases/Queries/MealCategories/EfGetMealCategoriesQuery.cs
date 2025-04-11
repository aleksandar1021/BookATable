using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.MealCategories
{
    public class EfGetMealCategoriesQuery : EfUseCase, IGetMealCategoriesQuery
    {
        private readonly IMapper _mapper;

        public EfGetMealCategoriesQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 13;

        public string Name => "Search meal categories";

        public PagedResponse<ResponseMealCategoryDTO> Execute(SearchNamedEntityDTO data)
        {
            var query = Context.MealCategories.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (!string.IsNullOrEmpty(data.Name))
            {
                query = query.Where(x => x.Name.Contains(data.Name));
            }

            return query.AsPagedReponse<MealCategory, ResponseMealCategoryDTO>(data, _mapper);
        }
    }
}
