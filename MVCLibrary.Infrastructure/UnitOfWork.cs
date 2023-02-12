using MVCLibrary.Infrastructure.RepositoryClasses;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;

namespace MVCLibrary.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private ILibraryRepository _libraryRepository;
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private IProvinceRepository _provinceRepository;
        private IBookRepository _bookRepository;
        private IBookDetailRepository _bookDetailRepository;
        private ICategoryRepository _categoryRepository;
        private ICityRepository _cityRepository;



        private MVCContext _dbContext;

        public MVCContext DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = new MVCContext();
                return _dbContext;
            }
        }

        public ILibraryRepository LibraryRepository
        {
            get
            {
                if (_libraryRepository == null)

                    _libraryRepository = new LibraryRepository(DbContext);
                return _libraryRepository;
            }
        }

        public ICityRepository CityRepository
        {
            get
            {
                if (_cityRepository == null)
                    _cityRepository = new CityRepository(DbContext);
                return _cityRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(DbContext);
                return _userRepository;
            }
        }

        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                if (_userRoleRepository == null)
                    _userRoleRepository = new UserRoleRepository(DbContext);
                return _userRoleRepository;
            }
        }
        public IProvinceRepository ProvinceRepository
        {
            get
            {
                if (_provinceRepository == null)
                    _provinceRepository = new ProvinceRepository(DbContext);
                return _provinceRepository;
            }
        }

        public IBookRepository BookRepository
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(DbContext);
                return _bookRepository;
            }
        }

        public IBookDetailRepository BookDetailRepository
        {
            get
            {
                if (_bookDetailRepository == null)
                    _bookDetailRepository = new BookDetailRepository(DbContext);
                return _bookDetailRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(DbContext);
                return _categoryRepository;
            }
        }

        public async Task Commit() => await DbContext.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
