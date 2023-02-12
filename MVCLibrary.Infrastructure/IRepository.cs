using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MVCLibrary.Infrastructure
{
    public interface IRepository<T, IdType> where T : class where IdType : struct
    {
        Task<T> GetById(IdType id);
        Task<IQueryable<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<IQueryable<T>> GetWithPredicate(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<TResult>> GetWithPredicateThenInclude<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includeNavigationProperty, bool disableTracking = true);
    }
}
