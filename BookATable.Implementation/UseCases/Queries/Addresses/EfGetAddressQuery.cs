using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Addresses;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Addresses
{
    public class EfGetAddressQuery : EfFindUseCase<ResponseAddressDTO, Address>, IGetAddressQuery
    {
        public EfGetAddressQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 22;

        public override string Name => "Find address by Id";
    }
}
