using BookATable.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.UseCases.Queries.Auth
{
    public interface IIsLogged : IQuery<LoggedDTO, SearchNamedEntityDTO>
    {
    }
}
