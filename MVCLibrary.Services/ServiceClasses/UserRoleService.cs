using MVCLibrary.Data.Models;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Services.ServiceInterfaces;


namespace MVCLibrary.Services.ServiceClasses
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<UserRoleDTO> AddUserRole(UserRoleDTO userRole)
        {
            var data = await MapToEntity(userRole);
            await _unitOfWork.UserRoleRepository.Add(data);
            await _unitOfWork.Commit();
            return userRole;
        }

        public async Task<IEnumerable<UserRoleDTO>> GetAllUserRoleWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var userRoleies = await _unitOfWork.UserRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                userRoleies = userRoleies.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    userRoleies = userRoleies.OrderByDescending(s => s.Name);
                    break;
                default:
                    userRoleies = userRoleies.OrderBy(s => s.Name);
                    break;
            }
            pageSize = pageSize.HasValue ? pageSize : 0;
            int pageNumber = (page ?? 1);
            userRoleies = userRoleies.Skip(pageSize.Value * pageNumber).Take(pageSize.Value);
           var ttt= userRoleies != null && userRoleies.Any() ? userRoleies.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<UserRoleDTO>();
            return ttt;
        }

        public async Task<UserRoleDTO> GetUserRole(int id)
        {
            var data = await _unitOfWork.UserRoleRepository.GetById(id);
            return await MapToDTO(data);
        }

        public async Task<IEnumerable<UserRoleDTO>> GetUserRoleies()
        {
            var datas = await _unitOfWork.UserRoleRepository.GetAll();
            var ttt= datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<UserRoleDTO>();
            return ttt;
        }

        public async Task<UserRoleDTO> MapToDTO(UserRole userRole)
        {
            return new UserRoleDTO()
            {
                Id = userRole.Id,
                Name = userRole.Name,
            };
        }

        public async Task<UserRole> MapToEntity(UserRoleDTO userRole)
        {
            return await Task.Run(() => new UserRole()
            {
                Id = userRole.Id,
                Name = userRole.Name,
            });
        }

        public async Task RemoveUserRole(int UserRoleId)
        {
            await _unitOfWork.UserRoleRepository.Delete(await _unitOfWork.UserRoleRepository.GetById(UserRoleId));
            await _unitOfWork.Commit();
        }

        public async Task<UserRoleDTO> UpdatUserRole(UserRoleDTO userRole)
        {
            var data = await _unitOfWork.UserRoleRepository.Add(await MapToEntity(userRole));
            await _unitOfWork.Commit();
            return await MapToDTO(data);
        }
    }
}
