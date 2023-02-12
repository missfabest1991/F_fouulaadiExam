using System;
using System.Collections.Generic;

namespace MVCLibrary.DTO;

public class CategoryDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

   public List<BookDTO>? Books { get; set; }

    
}
