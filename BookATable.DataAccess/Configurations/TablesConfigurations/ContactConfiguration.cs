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
    public class ContactConfiguration : EntityConfiguration<Contact>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Subject)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.FirstName)
                   .IsRequired();

            builder.Property(x => x.LastName)
                   .IsRequired();

            //builder.HasIndex(x => new { x.Email, x.Name });
        }
    }
}
