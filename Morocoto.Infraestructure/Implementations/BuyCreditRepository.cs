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
        private readonly CustomerRepository _customerRepository;
        private readonly BusinessRepository _businessRepository;

        public BuyCreditRepository(MorocotoDbContext dbContext, 
                                    BuildConfirmations confirmations,
                                    CustomerTaxesRepository taxes,
                                    ILogger logger,
                                    TaxManager tax,
                                    CustomerRepository customerRepository,
                                    BusinessRepository businessRepository) : base(dbContext)
        {
            this._dbContext = dbContext;
            this._confirmations = confirmations;
            this._taxes = taxes;
            this._logger = logger;
            this._tax = tax;
            this._customerRepository = customerRepository;
            this._businessRepository = businessRepository;
        }
        public BuyCreditRepository(MorocotoDbContext dbContext):base(dbContext)
        {

        }


        public async Task<string> SellCredit(string accountBusiness, string accountCustomer, int creditSelled, string PIN)
        {
            //PLEASE!: USE PERCENTAGES FOR TAXES FIELDS! 100 => 5 5=0.05;
            var tax = await _tax.CalculateTax(creditSelled);

            decimal commission = creditSelled * tax.Tax;
            var customerResponse = await _customerRepository.FirstOrDefaultAsync(x => x.IdNavigation.AccountNumber == Encryption.Encrypt(accountCustomer));
            var businessResponse = await _businessRepository.FirstOrDefaultAsync(x => x.BusinessNumber == Encryption.Encrypt(accountBusiness));
            if (businessResponse.Partner.IdNavigation.Pin.Equals(Encryption.Encrypt(PIN)))
            {
                try
                {
                    BuyCredit Credit = new BuyCredit();
                    Credit.CustomerId = customerResponse.Id;
                    Credit.BusinessId = businessResponse.Id;
                    Credit.CreditBought = creditSelled;
                    Credit.CustomerTaxId = tax.Id;
                    Credit.TransactionNumber=_confirmations.BuildConfirmationCode();
                    await _dbContext.BuyCredits.AddAsync(Credit);
                    customerResponse.CreditAvailable = customerResponse.CreditAvailable + (creditSelled - commission);
                    // 20,000 => 2,000 comision:100

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
