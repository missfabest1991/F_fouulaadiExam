using MVCLibrary.Data.Models;
using MVCLibrary.DTO;

namespace MVCLibrary.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task<User> MapToEntity(UserDTO user);
        Task<UserDTO> MapToDTO(User user);
        Task<IEnumerable<UserDTO>> GetUseries();
        Task<UserDTO> GetUser(int id);
        Task<UserDTO> AddUser(UserDTO user);
        Task RemoveUser(int UserId);
        Task<UserDTO> UpdatUser(UserDTO user);
        Task<IEnumerable<UserDTO>> GetAllUserWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize);

    }
}
