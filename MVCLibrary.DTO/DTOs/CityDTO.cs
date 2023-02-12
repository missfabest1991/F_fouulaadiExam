using System.Collections.Generic;

namespace MVCLibrary.DTO
{
    public class CityDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int ProvinceId { get; set; }
        public virtual ProvinceDTO? Province { get; set; }
    }
}
