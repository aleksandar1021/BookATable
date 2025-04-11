using BookATable.Application.DTO;
using BookATable.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookATable.Implementation.UseCases.Queries.Saved
{
    public interface IGetSavedQuery : IQuery<ResponseSavedDTO, int>
    {
    }
}
