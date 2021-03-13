using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Implementations
{
    public class CustomerTaxesRepository: GenericRepository<CustomerTaxis>, IAsyncCustomerTaxesRepository
    {
        private readonly MorocotoDbContext _dbContext;

        public CustomerTaxesRepository(MorocotoDbContext dbContext): base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<CustomerTaxis> CalculateTax(int creditRequested)
        {
            if (creditRequested >= 50 && 100 >= creditRequested)
            {
                var taxApplied = await _dbContext.CustomerTaxes.FindAsync(1);
                return taxApplied;
            }
            if (creditRequested >= 101 && 500 >= creditRequested)
            {
                var taxApplied = await _dbContext.CustomerTaxes.FindAsync(2);
                return taxApplied;
            }
            if (creditRequested >= 501 && 1000 >= creditRequested)
            {
                var taxApplied = await _dbContext.CustomerTaxes.FindAsync(3);
                return taxApplied;
            }
            if (creditRequested >= 1001 && 2000 > creditRequested)
            {
                var taxApplied = await _dbContext.CustomerTaxes.FindAsync(4);
                return taxApplied;
            }
            if (creditRequested >= 2001 && 5000 >= creditRequested)
            {
                var taxApplied = await _dbContext.CustomerTaxes.FindAsync(5);
                return taxApplied;
            }
            if (creditRequested >= 5001 && 10000 >= creditRequested)
            {
                var taxApplied = await _dbContext.CustomerTaxes.FindAsync(6);
                return taxApplied;
            }
            return null;

        }


    }
}
