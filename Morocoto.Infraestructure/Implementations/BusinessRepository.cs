using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using Morocoto.Infraestructure.Tools;

namespace Morocoto.Infraestructure.Implementations
{
    public class BusinessRepository: GenericRepository<Business>, IAsyncBusinessRepository
    {
        private readonly MorocotoDbContext _dbContext;
     

        public BusinessRepository(MorocotoDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Business>> GetAllPartnerBusinessesAsync(Expression<Func<Business, bool>> predicate)
        {
            return await _dbContext.Businesses.Include(x=>x.BusinessAddresses).Include(x=>x.BusinessBills).Where(predicate).ToListAsync();
        }
        //Note: BusinessAccountNumber is encrypted.
        public async Task<Business> GetBusinessByAccountNumberAsync(string businessAccountNumber)
        { 
            var response = await _dbContext.Businesses.FirstOrDefaultAsync(x => x.BusinessNumber == businessAccountNumber);
            return response;
        }
        public async Task<bool> IsAbleForSell(string businessAccountNumber, double creditRequested)
        {
            var response = await _dbContext.Businesses.FirstOrDefaultAsync(x => x.BusinessNumber == businessAccountNumber);

            if (response.BusinessCreditAvailable >= (int)creditRequested)
            {
                return true;
            }
            return false;
            
        }

    }
}
