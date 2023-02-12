using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCLibrary.Data;

namespace MVCLibrary.Infrastructure.Entities
{
    public class LibraryConfigurationType : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.ToTable("Library");
            builder.HasKey(e => e.Id).HasName("PK__Library__3214EC07853ADEDD");
            builder.Property(e => e.Address).HasMaxLength(150);
            builder.Property(e => e.EmailAddress).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20).IsUnicode(false);
        }
    }
}