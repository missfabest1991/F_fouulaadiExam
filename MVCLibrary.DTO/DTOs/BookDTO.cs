using System.Collections.Generic;

namespace MVCLibrary.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

        public decimal Price { get; set; }

        public int LibraryId { get; set; }

        public int CategoryId { get; set; }

        public List<BookDetailDTO>? BookDetails { get; set; }

        public CategoryDTO? Category { get; set; }

        public LibraryDTO? Library { get; set; }
    }
}
