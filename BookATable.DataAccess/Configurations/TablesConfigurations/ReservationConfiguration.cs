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
    public class ReservationConfiguration : EntityConfiguration<Reservation>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.NumberOfGuests)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(x => x.Time)
                   .IsRequired();

            builder.Property(x => x.IsAccepted)
                   .HasDefaultValue(false);

            builder.Property(x => x.ReservationCode)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
