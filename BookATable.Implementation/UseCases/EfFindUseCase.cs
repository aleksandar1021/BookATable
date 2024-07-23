using AutoMapper;
using BookATable.Application.UseCases;
using BookATable.DataAccess;
using BookATable.Domain;
using BookATable.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases
{
    public abstract class EfFindUseCase<TResult, TEntity> : EfUseCase, IQuery<TResult, int>
        where TResult : class
        where TEntity : Entity
    {
        private readonly IMapper _mapper;

        protected EfFindUseCase(Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public TResult Execute(int search)
        {
            var item = Context.Set<TEntity>().Find(search);

            if (item == null)
            {
                throw new NotFoundException(nameof(TEntity), search);
            }

            return _mapper.Map<TResult>(item);
        }
    }
}
