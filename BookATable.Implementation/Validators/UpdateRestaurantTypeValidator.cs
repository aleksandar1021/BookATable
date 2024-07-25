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
    public class UpdateRestaurantTypeValidator : UpdateNamedValidator<RestaurantType>
    {
        public UpdateRestaurantTypeValidator(Context ctx, Func<Context, DbSet<RestaurantType>> dbSetFunc, Func<RestaurantType, string> getNameFunc, Func<RestaurantType, int> getIdFunc) : base(ctx, dbSetFunc, getNameFunc, getIdFunc)
        {
        }
    }
}
