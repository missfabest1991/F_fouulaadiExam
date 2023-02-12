using MVCLibrary.Data;
using MVCLibrary.DTO;

namespace MVCLibrary.Services.ServiceInterfaces
{
    public interface IProvinceService
    {
        Task<Province> MapToEntity(ProvinceDTO province);
        Task<ProvinceDTO> MapToDTO(Province province);
       
        Task<ProvinceDTO> GetProvince(int provinceId);
        Task<IEnumerable<ProvinceDTO>> GetProvinces();
        Task<ProvinceDTO> AddProince(ProvinceDTO province);
       
        Task RemoveProvince(int provinceId);
        Task<ProvinceDTO> UpdateProvince(ProvinceDTO province);
        Task<IEnumerable<ProvinceDTO>> GetAllProvinceWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize);
    }
}
