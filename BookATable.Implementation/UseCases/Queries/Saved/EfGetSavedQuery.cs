using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookATable.Implementation.UseCases.Queries.Saved
{
    public class EfGetSavedQuery : EfFindUseCase<ResponseSavedDTO, Domain.Tables.Saved>, IGetSavedQuery
    {
        public EfGetSavedQuery(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 71;

        public override string Name => "Find saved by Id";
    }
}
