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
    public class AddressConfiguration : EntityConfiguration<Address>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Place)
                   .HasMaxLength(70);

            builder.Property(x => x.AddressOfPlace)
                   .IsRequired()
                   .HasMaxLength(70);

            builder.Property(x => x.Number)
                   .HasColumnType("tinyint");

            builder.Property(x => x.Floor)
                   .HasColumnType("tinyint");

            builder.Property(x => x.Latitude)
                   .HasColumnType("decimal(9, 6)");

            builder.Property(x => x.Longitude)
                   .HasColumnType("decimal(9, 6)");

            builder.HasOne(x => x.City)
                   .WithMany(x => x.Addresses)
                   .HasForeignKey(x => x.CityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
