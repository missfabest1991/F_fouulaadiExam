using MVCLibrary.Data.Models;
using MVCLibrary.DTO;

namespace MVCLibrary.Services.ServiceInterfaces
{
    public interface IUserRoleService
    {
        Task<UserRole> MapToEntity(UserRoleDTO userRole);
        Task<UserRoleDTO> MapToDTO(UserRole userRole);
        Task<IEnumerable<UserRoleDTO>> GetUserRoleies();
        Task<UserRoleDTO> GetUserRole(int id);
        Task<UserRoleDTO> AddUserRole(UserRoleDTO userRole);
        Task RemoveUserRole(int UserRoleId);
        Task<UserRoleDTO> UpdatUserRole(UserRoleDTO userRole);
        Task<IEnumerable<UserRoleDTO>> GetAllUserRoleWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize);
    }
}
