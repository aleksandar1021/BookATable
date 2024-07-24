using BookATable.Application.UseCases.Queries.Users;
using BookATable.DataAccess;
using BookATable.Domain.Tables;
using BookATable.Implementation.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.UseCases.Queries.Users
{
    public class EfIsActivateUserQuery : EfUseCase, IIsActivateUserQuery
    {
        public EfIsActivateUserQuery(Context context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "Is activate user";

        public string Execute(string data)
        {
            User user = Context.Users.FirstOrDefault(x => x.Email == data);

            if(user == null)
            {
                throw new NotFoundException(nameof(User),data);
            }

            string isActivate = "false";

            if (user.IsActivatedUser)
            {
                isActivate = "true";
            }

            return isActivate;
        }
    }
}
