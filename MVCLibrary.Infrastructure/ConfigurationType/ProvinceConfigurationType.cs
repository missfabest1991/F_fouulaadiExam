using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCLibrary.Data;

namespace MVCLibrary.Infrastructure.Entities
{
    public class ProvinceConfigurationType : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Province");
            builder.HasKey(e => e.ProvinceId).HasName("PK__Province__3214EC07853ADEDD");
            builder.Property(e => e.ProvinceName).HasMaxLength(150);
        }
    }
}