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
    public class RatingConfiguration : EntityConfiguration<Rating>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Rating> builder)
        {
            builder.Property(x => x.Rate)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.Ratings)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Ratings)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
