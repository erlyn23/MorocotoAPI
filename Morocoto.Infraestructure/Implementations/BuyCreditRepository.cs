using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;
using System.Threading.Tasks;
using Morocoto.Infraestructure.Tools;
using Morocoto.Infraestructure.Services;
using Microsoft.Extensions.Logging;

namespace Morocoto.Infraestructure.Implementations
{
    public class BuyCreditRepository: GenericRepository<BuyCredit>, IAsyncBuyCreditRepository
    {
        private readonly MorocotoDbContext _dbContext;
        private readonly BuildConfirmations _confirmations;
        private readonly ILogger _logger;

        public BuyCreditRepository(MorocotoDbContext dbContext, BuildConfirmations confirmations, ILogger logger): base(dbContext)
        {
            this._dbContext = dbContext;
            this._confirmations = confirmations;
            this._logger = logger;
        }
        public BuyCreditRepository(MorocotoDbContext dbContext):base(dbContext)
        {

        }


        public async Task<bool> SellCredit(Business business, Customer customer, int creditSelled, string PIN, CustomerTaxis taxes)
        {
            double commission = creditSelled * 0.05;

            if (business.Partner.IdNavigation.Pin.Equals(Encryption.Encrypt(PIN)))
            {
                try
                {
                    customer.CreditAvailable = customer.CreditAvailable + (decimal)(creditSelled - commission);
                    BuyCredit Credit = new BuyCredit();
                    Credit.CustomerId = customer.Id;
                    Credit.BusinessId = business.Id;
                    Credit.CreditBought = creditSelled;
                    Credit.CustomerTaxId = taxes.Id;
                    Credit.TransactionNumber=_confirmations.BuildConfirmationCode();
                    await _dbContext.BuyCredits.AddAsync(Credit);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return false;
                }
            }
                return false;
          
        }


    }
}
