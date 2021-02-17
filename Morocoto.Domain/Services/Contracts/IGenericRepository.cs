using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Services.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllElementsAsync();
        Task<TEntity> GetElementByIdAsync(int entityId);
        Task SaveElementAsync(TEntity entity);
        void UpdateElement(TEntity entity);
        Task DeleteElementAsync(int entityId);
        Task<int> SaveChangesAsync();
    }
}
