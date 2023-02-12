using Microsoft.EntityFrameworkCore;
using MVCLibrary.Data;
using MVCLibrary.Data.Models;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MVCLibrary.Infrastructure.RepositoryClasses
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(MVCContext dbContext) : base(dbContext)
        {
        }
    }
}
