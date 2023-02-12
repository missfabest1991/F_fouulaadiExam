//using Microsoft.EntityFrameworkCore;
using MVCLibrary.Data;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;

namespace MVCLibrary.Infrastructure.RepositoryClasses
{
    public class LibraryRepository : Repository<Library, int>, ILibraryRepository
    {
        public LibraryRepository(MVCContext dbContext) : base(dbContext) { }
    }
}
