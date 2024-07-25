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
    public class UpdateAppendiceValidator : UpdateNamedValidator<Appendice>
    {
        public UpdateAppendiceValidator(Context ctx, Func<Context, DbSet<Appendice>> dbSetFunc, Func<Appendice, string> getNameFunc, Func<Appendice, int> getIdFunc) : base(ctx, dbSetFunc, getNameFunc, getIdFunc)
        {
        }
    }
}
