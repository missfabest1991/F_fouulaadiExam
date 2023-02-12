using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MVCLibrary.Models;
using System.Linq.Expressions;

namespace MVCLibrary.Infrastructure
{
    public class Repository<TEntity, TIdType> : IRepository<TEntity, TIdType> where TEntity : class where TIdType : struct
    {
        protected MVCContext DbContext { get; set; }
        protected DbSet<TEntity> DbSet { get; set; }
        public Repository(MVCContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity) => (await DbSet.AddAsync(entity)).Entity;

        public async Task<TEntity> Delete(TEntity entity) => (await Task.Run(() => DbSet.Remove(entity))).Entity;

        public async Task<IQueryable<TEntity>> GetAll() => await Task.Run(() => DbSet.AsNoTracking());

        public async Task<TEntity> GetById(TIdType id) => await DbSet.FindAsync(id);

        public async Task<TEntity> Update(TEntity entity) => (await Task.Run(() => DbSet.Update(entity))).Entity;

        public async Task<IQueryable<TEntity>> GetWithPredicate(Expression<Func<TEntity, bool>> predicate) => await Task.Run(() => DbSet.Where(predicate).AsQueryable());

        public async Task<IEnumerable<TResult>> GetWithPredicateThenInclude<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeNavigationProperty, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;


            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includeNavigationProperty != null)
            {
                query = includeNavigationProperty(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await Task.Run(() => query.Select(selector));
        }
    }
}