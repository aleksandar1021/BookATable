using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.SpecificClosedDays;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.SpecificClosedDays
{
    public class EfGetSpecificClosedDayQuery : EfFindUseCase<ResponseSpecificClosedDays, Domain.Tables.SpecificClosedDays>, IGetSpecificClosedDayQuery
    {
        public EfGetSpecificClosedDayQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 83;

        public override string Name => "Find specific closed days by Id";
    }
}
