using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;

namespace MVCLibrary.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        MVCContext DbContext { get; }

        ILibraryRepository LibraryRepository { get; }

        IUserRepository UserRepository { get; }

        IUserRoleRepository UserRoleRepository { get; }

        IBookRepository BookRepository { get; }
        IBookDetailRepository BookDetailRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        ICityRepository CityRepository { get; }
    }
}
