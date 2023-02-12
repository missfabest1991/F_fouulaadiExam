using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure;
using MVCLibrary.Services.ServiceInterfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Services.ServiceClasses
{
    public class CityService:ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CityDTO> Create(CityDTO city)
        {
            var data = await MapToEntity(city);
            await _unitOfWork.CityRepository.Add(data);
            await _unitOfWork.Commit();
            return city;
        }

        public async Task<IEnumerable<CityDTO>> GetCities()
        {
            IQueryable<City> datas = await _unitOfWork.CityRepository.GetAll();
            return datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : null;
        }

        public async Task<CityDTO?> GetCity(int cityId)
        {
            var data = await _unitOfWork.CityRepository.GetById(cityId);
            return await MapToDTO(data);
        }

        public async Task<CityDTO> MapToDTO(City city)
        {
            return new CityDTO
            {
                CityId = city.Id,
                CityName = city.CityName
                

            };
        }

        public async Task<City> MapToEntity(CityDTO city)
        {
            return await Task.Run(() => new City()
            {

                Id = city.CityId,
                CityName = city.CityName
            });
        }

        public async Task Remove(int cityId)
        {
            await _unitOfWork.CityRepository.Delete(await _unitOfWork.CityRepository.GetById(cityId));
            await _unitOfWork.Commit();
        }

        public async Task<CityDTO> Update(CityDTO city)
        {
            var bdata = await _unitOfWork.CityRepository.Add(await MapToEntity(city));
            _unitOfWork.Commit();
            return await MapToDTO(bdata);
        }
    }
}
