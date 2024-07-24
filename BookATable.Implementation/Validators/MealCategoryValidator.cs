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
    public class MealCategoryValidator : NamedEntityValidator<MealCategory>
    {
        public MealCategoryValidator(Context ctx)
            : base(ctx, c => c.MealCategories, c => c.Name)
        {
        }
    }
}
