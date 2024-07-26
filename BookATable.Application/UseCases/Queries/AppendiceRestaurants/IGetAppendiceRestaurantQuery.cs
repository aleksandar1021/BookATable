using BookATable.Application.DTO;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.UseCases.Queries.AppendiceRestaurants
{
    public interface IGetAppendiceRestaurantQuery : IQuery<ResponseAppendiceRestaurantDTO, int>
    {
    }
}
