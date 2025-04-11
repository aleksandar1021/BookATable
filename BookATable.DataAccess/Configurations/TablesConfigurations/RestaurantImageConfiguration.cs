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
    public class RestaurantImageConfiguration : EntityConfiguration<RestaurantImage>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<RestaurantImage> builder)
        {
            builder.Property(x => x.Path)
                   .IsRequired();

            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.RestaurantImages)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
