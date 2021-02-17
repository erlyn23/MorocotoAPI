using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Morocoto.Domain.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
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

        public async Task SaveElementAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void UpdateElement(TEntity entity)
        {
           _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task DeleteElementAsync(int entityId)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(entityId);
            if(entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
