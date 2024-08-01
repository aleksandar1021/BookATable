using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Ratings;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Ratings
{
    public class EfGetRatingQuery : EfFindUseCase<ResponseRatingDTO, Rating>, IGetRatingQuery
    {
        public EfGetRatingQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 59;

        public override string Name => "Find rating by Id";
    }
}
