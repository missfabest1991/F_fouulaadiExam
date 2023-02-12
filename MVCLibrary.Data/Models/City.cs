using System.Collections.Generic;

namespace MVCLibrary.Data;

public class City
{
    public int Id { get; set; }
    public string CityName { get; set; } = null!;
    public int ProvinceId { get; set; }
    public virtual Province Province { get; set; } = null!;
}
