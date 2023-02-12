using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCLibrary.Data;

namespace MVCLibrary.Infrastructure.Entities
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");

            builder.HasKey(e => e.Id).HasName("PK__Book__3214EC07F8E418B9");

            builder.Property(e => e.AuthorName).HasMaxLength(150);
            builder.Property(e => e.Name).HasMaxLength(150);
            builder.Property(e => e.Price).HasColumnType("decimal(6, 2)");

            builder.HasOne(d => d.Category).WithMany(p => p.Books).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Category_Book_FK");
            builder.HasOne(d => d.Library).WithMany(p => p.Books).HasForeignKey(d => d.LibraryId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Library_Book_FK");
        }
    }
}
