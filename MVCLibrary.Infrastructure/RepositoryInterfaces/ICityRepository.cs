using MVCLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Infrastructure.RepositoryInterfaces
{
    public interface ICityRepository : IRepository<City, int>
    {
    }
}
