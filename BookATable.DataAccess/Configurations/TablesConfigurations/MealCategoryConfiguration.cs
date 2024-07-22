using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.DataAccess.Configurations.TablesConfigurations
{
    public class MealCategoryConfiguration : NamedEntityConfiguration<MealCategory>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<MealCategory> builder)
        {
        }
    }
}
