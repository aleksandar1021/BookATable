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
    public class UpdateMealCategoryValidator : UpdateNamedValidator<MealCategory>
    {
        public UpdateMealCategoryValidator(Context ctx, Func<Context, DbSet<MealCategory>> dbSetFunc, Func<MealCategory, string> getNameFunc, Func<MealCategory, int> getIdFunc) : base(ctx, dbSetFunc, getNameFunc, getIdFunc)
        {
        }
    }
}
