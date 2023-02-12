using System;
using System.Collections.Generic;

namespace MVCLibrary.Data;

public class Province
{
    public int ProvinceId { get; set; }

    public string ProvinceName { get; set; } = null!;


    public virtual ICollection<City> Citys { get; } = new List<City>();
}
