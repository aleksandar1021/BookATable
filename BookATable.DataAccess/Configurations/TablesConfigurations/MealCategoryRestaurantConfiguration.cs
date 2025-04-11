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
    public class MealCategoryRestaurantConfiguration : EntityConfiguration<MealCategoryRestaurant>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<MealCategoryRestaurant> builder)
        {
            builder.HasOne(x => x.MealCategory)
                   .WithMany(x => x.MealCategoryRestaurants)
                   .HasForeignKey(x => x.MealCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Restaurant)
                   .WithMany(x => x.MealCategoryRestaurants)
                   .HasForeignKey(x => x.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.MealCategoryId, x.RestaurantId});

        }
    }
}
