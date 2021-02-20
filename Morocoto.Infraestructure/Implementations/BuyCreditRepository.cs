using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;
using System.Threading.Tasks;
using Morocoto.Infraestructure.Tools;

namespace Morocoto.Infraestructure.Implementations
{
    public class BuyCreditRepository: GenericRepository<BuyCredit>, IAsyncBuyCreditRepository
    {
        private readonly MorocotoDbContext _dbContext;

        public BuyCreditRepository(MorocotoDbContext dbContext): base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> SellCredit(Business business, Customer customer, int creditSelled, string PIN)
        {
            double commission = creditSelled * 0.05;

            try
            {
                if (business.Partner.IdNavigation.Pin.Equals(Encryption.Encrypt(PIN)))
                {
                    customer.CreditAvailable = (decimal)customer.CreditAvailable + (decimal)(creditSelled-commission);
                    int buySuccessful = Math.rando
                }
            }
            catch ()
            {

            }
        }


    }
}
