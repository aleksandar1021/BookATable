using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Contact;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Contact
{
    public class EfGetContactsQuery : EfUseCase, IGetContactsQuery
    {
        private readonly IMapper _mapper;
        public EfGetContactsQuery(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 86;

        public string Name => "Search contacts";

        public PagedResponse<ResponseContactDTO> Execute(SearchContactDTO data)
        {
            var query = Context.Contacts.Where(x => x.IsActive).AsQueryable();

            if (data.Id.HasValue)
            {
                query = query.Where(x => x.Id == data.Id);
            }

            if (!string.IsNullOrEmpty(data.Keyword))
            {
                query = query.Where(x => x.Email.ToLower().Contains(data.Keyword.ToLower()) || x.Name.ToLower().Contains(data.Keyword.ToLower()) || x.Subject.ToLower().Contains(data.Keyword.ToLower()));
            }

            

            return query.AsPagedReponse<Domain.Tables.Contact, ResponseContactDTO>(data, _mapper);
        }
    }
}
