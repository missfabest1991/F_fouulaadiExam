using System.Collections.Generic;

namespace MVCLibrary.DTO
{
    public class ProvinceDTO
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public List<CityDTO> Cities { get; set; }
    }
}