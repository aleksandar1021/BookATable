using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.ReservationAppendices;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.ReservationAppendices
{
    public class EfGetReservationAppendiceQuery : EfFindUseCase<ResponseReservationAppendiceDTO, ReservationAppendice>, IGetReservationAppendiceQuery
    {
        public EfGetReservationAppendiceQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 68;

        public override string Name => "Find restaurant appendice by Id";
    }
}
