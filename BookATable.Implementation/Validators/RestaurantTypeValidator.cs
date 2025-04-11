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
    public class RestaurantTypeValidator : NamedEntityValidator<RestaurantType>
    {
        public RestaurantTypeValidator(Context ctx)
            : base(ctx, c => c.RestaurantTypes, c => c.Name)
        {
        }
    }
}
