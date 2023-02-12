using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Services.ServiceInterfaces;

namespace MVCLibrary.Services.ServiceClasses
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        public ProvinceService(IProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public async Task<ProvinceDTO> MapToDTO(Province province)
        {
            return new LibraryDTO()
            {
                ProvinceId = province.Id,
                ProvinceName = province.Name
            };
        }

        public async Task<Province> MapToEntity(ProvinceDTO province)
        {
            return await Task.Run(() => new Province()
            {
                ProvinceId = province.ProvinceId,
                ProvinceName = province.ProvinceName,
            });
        }

        public async Task<ProvinceDTO> AddLibrary(ProvinceDTO province)
        {
            var data = await MapToEntity(province);
            await _provinceRepository.InsertProvince(data);
            return province;
        }

        public async Task<IEnumerable<ProvinceDTO>> GetProvinces()
        {
            var datas = await _provinceRepository.GetProvinces();
            return datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : null;
        }

        public async Task<ProvinceDTO> GetProvince(int provinceId)
        {
            var data = await _provinceRepository.GetProvinceById(provinceId);
            return await MapToDTO(data);
        }

        public async Task RemoveProvince(int provinceId)
        {
            await _provinceRepository.DeleteProvince(provinceId);
        }

        public async Task<ProvinceDTO> UpdateProvince(ProvinceDTO province)
        {
            var data = await _provinceRepository.UpdateProvince(await MapToEntity(province));
            return await MapToDTO(data);
        }

        public async Task<IEnumerable<ProvinceDTO>> GetAllProvinceWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var provinces = await _provinceRepository.GetProvinces();
            if (!string.IsNullOrEmpty(searchString))
            {
                provinces = provinces.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    provinces = provinces.OrderByDescending(s => s.ProvinceName).ToList();
                    break;
                default:
                    provinces = provinces.OrderBy(s => s.ProvinceName).ToList();
                    break;
            }
            pageSize = pageSize.HasValue ? pageSize : 0;
            int pageNumber = (page ?? 1);
            provinces = provinces.Skip(pageSize.Value * pageNumber).Take(pageSize.Value);
            return provinces != null && provinces.Any() ? provinces.Select(x => MapToDTO(x).Result).AsEnumerable() : null;
        }
    }
}
