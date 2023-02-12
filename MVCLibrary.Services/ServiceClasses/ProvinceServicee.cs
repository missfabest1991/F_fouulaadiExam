using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure;
using MVCLibrary.Services.ServiceInterfaces;

namespace MVCLibrary.Services.ServiceClasses
{
    public class ProvinceServicee: IProvinceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProvinceServicee(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ///////////////////////////////////////////////////MapToDTO/////////////////////////////////////////////////

        public async Task<ProvinceDTO> MapToDTO(Province province)
        {
            return new ProvinceDTO
            {
                ProvinceId = province.ProvinceId,
                ProvinceName = province.ProvinceName
            };
        }


        ///////////////////////////////////////////////////MapToEntity/////////////////////////////////////////////////

        public async Task<Province> MapToEntity(ProvinceDTO province)
        {
            return await Task.Run(() => new Province()
            {
                ProvinceId = province.ProvinceId,
                ProvinceName = province.ProvinceName
            });
        }



        ///////////////////////////////////////////////////CREATE/////////////////////////////////////////////////


        public async  Task<ProvinceDTO> AddProince(ProvinceDTO province)
        {
            var data = await MapToEntity(province);
            await _unitOfWork.ProvinceRepository.Add(data);
            await _unitOfWork.Commit();
            return province;
        }


        ///////////////////////////////////////////////////GetAllProvinceWithPagination/////////////////////////////////////////////////

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

            var provinces = await _unitOfWork.ProvinceRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                provinces = provinces.Where(s => s.ProvinceName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    provinces = provinces.OrderByDescending(s => s.ProvinceName);
                    break;
                default:
                    provinces = provinces.OrderBy(s => s.ProvinceName);
                    break;
            }
            pageSize = pageSize.HasValue ? pageSize : 0;
            int pageNumber = (page ?? 1);
            provinces = provinces.Skip(pageSize.Value * pageNumber).Take(pageSize.Value);
            var ttt = provinces != null && provinces.Any() ? provinces.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<ProvinceDTO>();
            return ttt;
        }


        ///////////////////////////////////////////////////GetLibraries/////////////////////////////////////////////////

        //public Task<IEnumerable<ProvinceDTO>> GetLibraries()
        //{
        //    throw new NotImplementedException();
        //}

        ///////////////////////////////////////////////////GetProvince/////////////////////////////////////////////////


        public async Task<ProvinceDTO> GetProvince(int provinceId)
        {
            var data = await _unitOfWork.ProvinceRepository.GetById(provinceId);
            return await MapToDTO(data);
        }

        ///////////////////////////////////////////////////RemoveProvince/////////////////////////////////////////////////

        public async Task RemoveProvince(int provinceId)
        {
            await _unitOfWork.ProvinceRepository.Delete(await _unitOfWork.ProvinceRepository.GetById(provinceId));
            await _unitOfWork.Commit();
        }


        ///////////////////////////////////////////////////Update/////////////////////////////////////////////////

        public async Task<ProvinceDTO> UpdateProvince(ProvinceDTO province)
        {
            var data = await _unitOfWork.ProvinceRepository.Add(await MapToEntity(province));
            _unitOfWork.Commit();
            return await MapToDTO(data);
        }

        public async Task<IEnumerable<ProvinceDTO>> GetProvinces()
        {
            IQueryable<Province> datas = await _unitOfWork.ProvinceRepository.GetAll();
            var ttt = datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<ProvinceDTO>();
            return ttt;
        }

      
    }
}
