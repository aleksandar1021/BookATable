using BookATable.DataAccess;
using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Validators
{
    public class CreateAppendiceValidator : NamedEntityValidator<Appendice>
    {
        public CreateAppendiceValidator(Context ctx, Func<Context, DbSet<Appendice>> dbSetFunc, Func<Appendice, string> getNameFunc) : base(ctx, dbSetFunc, getNameFunc)
        {
        }
    }
}
