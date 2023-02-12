using MVCLibrary.Data.Models;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;
using Microsoft.EntityFrameworkCore;
using MVCLibrary.Data;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCLibrary.Infrastructure.RepositoryClasses
{
    public class UserRoleRepository : Repository<UserRole, int>, IUserRoleRepository
    {
        public UserRoleRepository(MVCContext dbContext) : base(dbContext)
        {
        }
    }
}
