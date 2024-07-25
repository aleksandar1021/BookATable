using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Appendices;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Appendices
{
    public class EfGetAppendicesQuery : EfUseCase, IGetAppendicesQuery
    {
        private readonly IMapper _mapper;

        public EfGetAppendicesQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 33;

        public string Name => "Search appendices";

        public PagedResponse<ResponseNamedEntityDTO> Execute(SearchNamedEntityDTO data)
        {
            var query = Context.Appendices.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (!string.IsNullOrEmpty(data.Name))
            {
                query = query.Where(x => x.Name.Contains(data.Name));
            }

            return query.AsPagedReponse<Appendice, ResponseNamedEntityDTO>(data, _mapper);
        }
    }
}
