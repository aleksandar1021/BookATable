using BookATable.Application;
using BookATable.Application.DTO;
using BookATable.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class AdminUpdateUserValidator : BaseUserUpdateValidator<UpdateUserDTO>
    {
        public AdminUpdateUserValidator(Context ctx, IApplicationActor actor) : base(ctx, actor)
        {
        }
    }
}
