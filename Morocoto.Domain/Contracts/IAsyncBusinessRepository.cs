using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncBusinessRepository: IAsyncGenericRepository<Business>
    {
        Task<IEnumerable<Business>> GetAllPartnerBusinessesAsync(Expression<Func<Business, bool>> predicate);
        Task<Business> GetBusinessByAccountNumberAsync(string businessAccountNumber);
        Task<bool> IsAbleForSell(string businessAccountNumber, double creditRequested);
    }
}
