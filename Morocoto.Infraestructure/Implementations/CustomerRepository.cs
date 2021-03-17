using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Morocoto.Infraestructure.Implementations
{
    public class CustomerRepository: GenericRepository<Customer>, IAsyncCustomerRepository
    {
        private readonly MorocotoDbContext _dbContext;

        public CustomerRepository(MorocotoDbContext dbContext): base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> IsAbleForTransfer(string accountNumber, double creditRequested)
        {
            var response = await _dbContext.Users.Where(x => x.AccountNumber == accountNumber).FirstOrDefaultAsync(); ;
            var customerSender = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == response.Id);
            if (customerSender.CreditAvailable >= (int)creditRequested)
            {
                return true;
            }
            return false;

        }
    }
}
