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
    public class AppendiceRestaurantConfiguration : EntityConfiguration<AppendiceRestaurant>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<AppendiceRestaurant> builder)
        {
            builder.HasOne(x => x.Appendice)
                   .WithMany(x => x.AppendiceRestaurants)
                   .HasForeignKey(x => x.AppendiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.AppendiceRestaurants)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
