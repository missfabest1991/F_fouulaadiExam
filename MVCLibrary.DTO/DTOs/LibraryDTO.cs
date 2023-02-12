using System.Collections.Generic;

namespace MVCLibrary.DTO
{
    public class LibraryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public short LibraryNumber { get; set; }

        public List<BookDTO>? Books { get; set; }


    }
}
