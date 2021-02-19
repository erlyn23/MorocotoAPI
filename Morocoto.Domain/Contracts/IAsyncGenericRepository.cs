﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllElementsAsync();
        Task<TEntity> GetElementByIdAsync(TEntity entity);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddElementAsync(TEntity entity);
        Task AddElementsAsync(IEnumerable<TEntity> entities);
        void RemoveElementAsync(TEntity entity);
        void RemoveElementsAsync(IEnumerable<TEntity> entities);
    }
}
