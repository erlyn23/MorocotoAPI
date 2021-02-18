using Morocoto.Domain.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Morocoto.Domain.Contracts;

namespace Morocoto.Infraestructure.Implementations
{
    public class GenericRepository<TEntity> : IAsyncGenericRepository<TEntity> where TEntity : class
    {
        private readonly MorocotoDbContext _dbContext;

        public GenericRepository(MorocotoDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }


        public async Task<IEnumerable<TEntity>> GetAllElementsAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetElementByIdAsync(int entityId)
        {
            return await _dbContext.Set<TEntity>().FindAsync();
        }

        public async Task AddElementAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddElementsAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public void UpdateElement(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void RemoveElementAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveElementsAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public async  Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}
