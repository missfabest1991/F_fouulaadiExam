using Microsoft.EntityFrameworkCore;
using MVCLibrary.Data;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;

namespace MVCLibrary.Infrastructure.RepositoryClasses
{
    public class ProvinceRepository : Repository<Province, int>, IProvinceRepository
    {
        public ProvinceRepository(MVCContext dbContext) : base(dbContext) { }
    }
}
