using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCLibrary.Data;

namespace MVCLibrary.Infrastructure.Entities
{
    public class BookDetailConfigurationType : IEntityTypeConfiguration<BookDetail>
    {
        public void Configure(EntityTypeBuilder<BookDetail> builder)
        {
            builder.ToTable("BookDetail");
            builder.HasKey(e => e.Id).HasName("PK__BookDeta__3214EC07EC11CE0C");
            builder.Property(e => e.Description).HasMaxLength(400);
            builder.Property(e => e.PublishDateTime).HasColumnType("datetime");
            builder.HasOne(d => d.Book).WithMany(p => p.BookDetails).HasForeignKey(d => d.BookId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Book_BookDetail_FK");
        }
    }
}
