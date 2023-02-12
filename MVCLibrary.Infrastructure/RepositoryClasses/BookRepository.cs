using MVCLibrary.Data;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Infrastructure.RepositoryClasses
{
    public class BookRepository: Repository< Book,int >,   IBookRepository
    {
        public BookRepository(MVCContext dbContext) : base(dbContext) 
        {
        }
    }
}
