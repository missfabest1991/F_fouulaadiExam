using MVCLibrary.Data;
using MVCLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MVCLibrary.Services.ServiceInterfaces
{
    public interface ICityService
    {
        
            Task<City> MapToEntity(CityDTO city);
            Task<CityDTO> MapToDTO(City city); 
            Task<IEnumerable<CityDTO>> GetCities(); 
            Task<CityDTO?> GetCity(int cityId); 
            Task<CityDTO> Create(CityDTO city); 
            Task<CityDTO> Update(CityDTO city); 
            Task Remove(int cityId); 

        
    }
}
