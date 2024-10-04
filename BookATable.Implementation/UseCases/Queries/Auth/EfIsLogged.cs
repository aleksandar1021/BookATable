using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.Application.UseCases.Queries.Auth;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Auth
{
    public class EfIsLogged : EfUseCase, IIsLogged
    {
        private IApplicationActor _actor;
        public EfIsLogged(Context context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 95;

        public string Name => "Is logged";

        public LoggedDTO Execute(SearchNamedEntityDTO data)
        {
            if(_actor.Email != "/")
            {
                return new LoggedDTO
                {
                    IsLogged = true
                };
            }
            return new LoggedDTO { IsLogged = false };

        }
    }
}
