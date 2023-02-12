using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure;
using MVCLibrary.Services.ServiceInterfaces;

namespace MVCLibrary.Services.ServiceClasses
{
    public class LibraryService : ILibraryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LibraryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LibraryDTO> MapToDTO(Library library)
        {
            return new LibraryDTO()
            {
                Id = library.Id,
                Name = library.Name,
                Address = library.Address,
                EmailAddress = library.EmailAddress,
                LibraryNumber = library.LibraryNumber,
                PhoneNumber = library.PhoneNumber,
                //Books = library.Books != null && library.Books.Any() ? library.Books.Select(x => )
            };
        }

        public async Task<Library> MapToEntity(LibraryDTO library)
        {
            return await Task.Run(() => new Library()
            {
                Id = library.Id,
                Address = library.Address,
                EmailAddress = library.EmailAddress,
                LibraryNumber = library.LibraryNumber,
                Name = library.Name,
                PhoneNumber = library.PhoneNumber,
            });
        }

        public async Task<LibraryDTO> AddLibrary(LibraryDTO library)
        {
            var data = await MapToEntity(library);
            await _unitOfWork.LibraryRepository.Add(data);
            await _unitOfWork.Commit();
            return library;
        }

        public async Task<IEnumerable<LibraryDTO>> GetLibraries()
        {
            IQueryable<Library> datas = await _unitOfWork.LibraryRepository.GetAll();
           var ttt= datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<LibraryDTO>();
            return ttt;
        }

        public async Task<LibraryDTO> GetLibrary(int id)
        {
            var data = await _unitOfWork.LibraryRepository.GetById(id);
            return await MapToDTO(data);
        }

        public async Task RemoveLibrary(int libraryId)
        {
            await _unitOfWork.LibraryRepository.Delete(await _unitOfWork.LibraryRepository.GetById(libraryId));
            await _unitOfWork.Commit();
        }

        public async Task<LibraryDTO> UpdateLibrary(LibraryDTO library)
        {
            var data = await _unitOfWork.LibraryRepository.Add(await MapToEntity(library));
            _unitOfWork.Commit();
            return await MapToDTO(data);
        }

        public async Task<IEnumerable<LibraryDTO>> GetAllLibraryWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var libraries = await _unitOfWork.LibraryRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                libraries = libraries.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    libraries = libraries.OrderByDescending(s => s.Name);
                    break;
                default:
                    libraries = libraries.OrderBy(s => s.Name);
                    break;
            }
            pageSize = pageSize.HasValue ? pageSize : 0;
            int pageNumber = (page ?? 1);
            libraries = libraries.Skip(pageSize.Value * pageNumber).Take(pageSize.Value);
            var ttt= libraries != null && libraries.Any() ? libraries.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<LibraryDTO>();
            return ttt;
        }
    }
}
