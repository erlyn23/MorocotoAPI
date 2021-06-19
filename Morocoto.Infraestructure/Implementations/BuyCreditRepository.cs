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
        private readonly UserRepository _userRepository;
        private readonly CustomerTaxesRepository _taxes;
        //private readonly TaxManager _tax;
        private readonly CustomerRepository _customerRepository;
        private readonly PartnerRepository _partnerRepository;
        private readonly BusinessRepository _businessRepository;

        public BuyCreditRepository(MorocotoDbContext dbContext, 
                                    IAsyncCustomerTaxesRepository taxes,
                                    //TaxManager tax,
                                    IAsyncCustomerRepository customerRepository,
                                    IAsyncBusinessRepository businessRepository,
                                    IAsyncPartnerRepository partner,
                                    IAsyncUserRepository user) : base(dbContext)
        {
            this._dbContext = dbContext;
            this._userRepository = (UserRepository)user;
            this._taxes = (CustomerTaxesRepository)taxes;
            this._partnerRepository = (PartnerRepository)partner;
            this._customerRepository = (CustomerRepository)customerRepository;
            this._businessRepository = (BusinessRepository)businessRepository;
        }
     


        public async Task<string> SellCredit(string accountBusiness, string accountCustomer, int creditSelled, string PIN)
        {
            //PLEASE!: USE PERCENTAGES FOR TAXES FIELDS! 100 => 5 5=0.05;
            var tax = await _taxes.CalculateTax(creditSelled);

            decimal commission = creditSelled * tax.Tax;
            var customerResponse = await _userRepository.FirstOrDefaultAsync(x=>x.AccountNumber==accountCustomer);
            var businessResponse = await _businessRepository.FirstOrDefaultAsync(x => x.BusinessNumber == accountBusiness);
            var partner = await _partnerRepository.SearchByBusinesses(businessResponse);
            var customer = await _customerRepository.FirstOrDefaultAsync(x => x.Id == customerResponse.Id);
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == partner.Id);
            if (user.Pin.Equals(Encryption.Encrypt(PIN)))
            {
                try
                {
                    BuyCredit Credit = new BuyCredit
                    {
                        CustomerId = customerResponse.Id,
                        BusinessId = businessResponse.Id,
                        CreditBought = creditSelled,
                        CustomerTaxId = tax.Id,
                        TransactionNumber = BuildConfirmations.BuildConfirmationCode(),
                        CreditBoughtDate = DateTime.Today
                    };

                    await _dbContext.BuyCredits.AddAsync(Credit);
                    customer.CreditAvailable += (creditSelled - commission);
                    businessResponse.BusinessCreditAvailable -= creditSelled;
                    // 20,000 => 2,000 comision:100

                    return Credit.TransactionNumber;
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("La transacción ha fallado"+ex.Message);
                }
            }
            throw new Exception("El PIN ingresado es incorrecto, intentalo otra vez!");

        }


    }
}
