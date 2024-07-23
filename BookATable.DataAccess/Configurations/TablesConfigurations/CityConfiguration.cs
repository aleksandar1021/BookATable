using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.DataAccess.Configurations.TablesConfigurations
{
    public class CityConfiguration : EntityConfiguration<City>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.ZipCode)
                   .IsRequired();


            builder.Property(x => x.Name)
                   .IsRequired();
        }
    }
}
