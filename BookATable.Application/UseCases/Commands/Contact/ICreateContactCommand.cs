using BookATable.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application.UseCases.Commands.Contact
{
    public interface ICreateContactCommand : ICommand<CreateContactDTO>
    {
    }
}
