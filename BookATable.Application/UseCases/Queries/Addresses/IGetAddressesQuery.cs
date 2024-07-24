using BookATable.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.UseCases.Queries.Addresses
{
    public interface IGetAddressesQuery : IQuery<PagedResponse<ResponseAddressDTO>, SearchAddressDTO>
    {
    }
}
