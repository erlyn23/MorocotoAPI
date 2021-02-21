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
        private readonly CustomerTaxesRepository _taxes;
        private readonly ILogger _logger;
        private readonly TaxManager _tax;

        public BuyCreditRepository(MorocotoDbContext dbContext, 
                                    BuildConfirmations confirmations,
                                    CustomerTaxesRepository taxes,
                                    ILogger logger,
                                    TaxManager tax): base(dbContext)
        {
            this._dbContext = dbContext;
            this._confirmations = confirmations;
            this._taxes = taxes;
            this._logger = logger;
            this._tax = tax;
        }
        public BuyCreditRepository(MorocotoDbContext dbContext):base(dbContext)
        {

        }


        public async Task<string> SellCredit(Business business, Customer customer, int creditSelled, string PIN)
        {
            //PLEASE!: USE PERCENTAGES FOR TAXES FIELDS!
            var tax = await _tax.CalculateTax(creditSelled) ;
            decimal commission = creditSelled * tax.Tax; 

            if (business.Partner.IdNavigation.Pin.Equals(Encryption.Encrypt(PIN)))
            {
                try
                {
                    BuyCredit Credit = new BuyCredit();
                    Credit.CustomerId = customer.Id;
                    Credit.BusinessId = business.Id;
                    Credit.CreditBought = creditSelled;
                    Credit.CustomerTaxId = tax.Id;
                    Credit.TransactionNumber=_confirmations.BuildConfirmationCode();
                    await _dbContext.BuyCredits.AddAsync(Credit);
                    customer.CreditAvailable = customer.CreditAvailable + (creditSelled - commission);

                    return Credit.TransactionNumber;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return "La transaccion ha fallado!";
                }
            }
                return "El PIN ingresado es incorrecto, intentalo otra vez!";
          
        }


    }
}
