using MVCLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Infrastructure.RepositoryInterfaces
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
      
    }
}
