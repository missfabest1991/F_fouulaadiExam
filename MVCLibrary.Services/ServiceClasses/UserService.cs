using MVCLibrary.Data.Models;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Services.ServiceInterfaces;


namespace MVCLibrary.Services.ServiceClasses
{
    public class UserService : IUserService
    {


        private readonly IUnitOfWork _UnitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        
        public async Task<UserDTO> AddUser(UserDTO user)
        {
            var data = await MapToEntity(user);
            await _UnitOfWork.UserRepository.Add(data);
            await _UnitOfWork.Commit();
            return  user;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUserWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var useries = await _UnitOfWork.UserRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                useries = useries.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    useries = useries.OrderByDescending(s => s.Name);
                    break;
                default:
                    useries = useries.OrderBy(s => s.Name);
                    break;
            }
            pageSize = pageSize.HasValue ? pageSize : 0;
            int pageNumber = (page ?? 1);
            useries = useries.Skip(pageSize.Value * pageNumber).Take(pageSize.Value);
            var ttt = useries != null && useries.Any() ? useries.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<UserDTO>();
            return ttt;
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var data = await _UnitOfWork.UserRepository.GetById(id);
            return await MapToDTO(data);
        }

        public async Task<IEnumerable<UserDTO>> GetUseries()
        {
            var datas = await _UnitOfWork.UserRepository.GetAll();
            var ttt= datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<UserDTO>();
            return ttt;
        }

        public async Task<UserDTO> MapToDTO(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                
            };
        }

        public async Task<User> MapToEntity(UserDTO user)
        {
            return await Task.Run(() => new User()
            {
                Id = user.Id,
                Name = user.Name,
               
            });
        }

        public async Task RemoveUser(int UserId)
        {
            await _UnitOfWork.UserRepository.Delete(await _UnitOfWork.UserRepository.GetById(UserId));
            await _UnitOfWork.Commit();
        }

        public async Task<UserDTO> UpdatUser(UserDTO user)
        {
            var data = await _UnitOfWork.UserRepository.Add(await MapToEntity(user));
            await _UnitOfWork.Commit();
            return await MapToDTO(data);
        }
    }
}
