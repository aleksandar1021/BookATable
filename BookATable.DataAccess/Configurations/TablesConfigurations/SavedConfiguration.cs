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
    public class SavedConfiguration : EntityConfiguration<Saved>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Saved> builder)
        {
            builder.Property(x => x.RestaurantId)
                   .IsRequired();

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Saved)
                                       .HasForeignKey(x => x.UserId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Restaurant).WithMany(x => x.Saved)
                                       .HasForeignKey(x => x.RestaurantId)
                                       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
