using System;
using System.Collections.Generic;

namespace MVCLibrary.Data;

public class Library
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public short LibraryNumber { get; set; }

    public int FCityId { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
    public City City { get; set; }
}
