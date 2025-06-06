﻿using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Reservations;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Reservations
{
    public class EfGetReservationQuery : EfFindUseCase<ResponseReservationDTO, Reservation>, IGetReservationQuery
    {
        public EfGetReservationQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 64;

        public override string Name => "Find reservation by Id";
    }
}
