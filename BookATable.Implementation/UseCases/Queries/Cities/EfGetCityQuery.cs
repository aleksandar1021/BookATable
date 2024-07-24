using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Cities
{
    public class EfGetCityQuery : EfFindUseCase<ResponseCityDTO, City>, IGetCityQuery
    {
        public EfGetCityQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 17;

        public override string Name => "Find city by id";
    }
}
