using BookATable.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Application
{
    public interface IUseCaseLogger
    {
        void Log(UseCaseLog log);
    }
}
