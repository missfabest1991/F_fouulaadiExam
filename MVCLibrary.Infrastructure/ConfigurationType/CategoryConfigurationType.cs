using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCLibrary.Data;
using System.Reflection.Emit;

namespace MVCLibrary.Infrastructure.Entities
{
    public class CategoryConfigurationType : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(e => e.Id).HasName("PK__Category__3214EC0708EE70E5");
            builder.Property(e => e.Name).HasMaxLength(150);
           

        }
    }
}