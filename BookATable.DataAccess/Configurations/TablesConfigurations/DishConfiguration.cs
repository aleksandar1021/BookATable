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
    public class DishConfiguration : EntityConfiguration<Dish>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Dish> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(70);


            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Price)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.Dishs)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
