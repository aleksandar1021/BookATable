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
    public class RestaurantConfiguration : EntityConfiguration<Restaurant>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.WorkFromHour)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(70);

            builder.Property(x => x.WorkUntilHour)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(x => x.WorkFromMinute)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(x => x.IsActivated)
                  .HasDefaultValue(false);

            builder.Property(x => x.WorkUntilMinute)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(x => x.MaxNumberOfGuests)
                   .IsRequired();

            builder.Property(x => x.TimeInterval)
                   .HasDefaultValue(10);

            builder.HasOne(x => x.Address)
                   .WithMany(x => x.Restaurants)
                   .HasForeignKey(x => x.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RestaurantType)
                   .WithMany(x => x.Restaurants)
                   .HasForeignKey(x => x.RestaurantTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.RegularClosedDays)
                   .WithOne(x => x.Restaurant)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.SpecificClosedDays)
                   .WithOne(x => x.Restaurant)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
