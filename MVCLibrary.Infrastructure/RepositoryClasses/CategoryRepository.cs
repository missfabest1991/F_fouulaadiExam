//using Microsoft.EntityFrameworkCore;
using MVCLibrary.Data;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;

namespace MVCLibrary.Infrastructure.RepositoryClasses
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(MVCContext dbContext) : base(dbContext) { }
    }
}
