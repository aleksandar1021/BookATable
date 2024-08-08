using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.RegularClosedDays;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.RegularClosedDays
{
    public class EfGetRegularClosedDayQuery : EfFindUseCase<ResponseRegularClosedDaysDTO, Domain.Tables.RegularClosedDays>, IGetRegularClosedDayQuery
    {
        public EfGetRegularClosedDayQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 78;

        public override string Name => "Find regular closed day by Id";
    }
}
