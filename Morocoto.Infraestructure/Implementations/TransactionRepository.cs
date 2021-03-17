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
    public class TransactionRepository: GenericRepository<Transaction>, IAsyncTransactionRepository
    {
        private readonly MorocotoDbContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly CustomerTaxesRepository _taxes;
        //private readonly TaxManager _tax;
        private readonly CustomerRepository _customerRepository;
        private readonly PartnerRepository _partnerRepository;
        private readonly BusinessRepository _businessRepository;

        public TransactionRepository(MorocotoDbContext dbContext,
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

        public async Task<string> Transaction(string accountSender, string accountReciever, int creditTransfered, string PIN)
        {
            //PLEASE!: USE PERCENTAGES FOR TAXES FIELDS! 100 => 5 5=0.05;
            var tax = await _taxes.TaxesPeerToPeer();

            decimal commission = creditTransfered * tax.Tax;
            var userSender = await _userRepository.FirstOrDefaultAsync(x => x.AccountNumber == accountSender);
            var customerSender = await _customerRepository.FirstOrDefaultAsync(x => x.Id == userSender.Id);
            
            var userReciever = await _userRepository.FirstOrDefaultAsync(x => x.AccountNumber == accountReciever);
            var customerReciever = await _customerRepository.FirstOrDefaultAsync(x=>x.Id==userReciever.Id);
            
            if (userSender.Pin.Equals(Encryption.Encrypt(PIN)))
            {
                try
                {
                    Transaction Credit = new Transaction
                    {
                        CustomerSenderId = customerSender.Id,
                        CustomerRecieverId = customerReciever.Id,
                        CreditTransfered = creditTransfered,
                        CustomerTaxesId = tax.Id,
                        ConfirmationNumber = BuildConfirmations.BuildConfirmationCode(),
                        DateCreation = DateTime.Today
                    };

                    await _dbContext.Transactions.AddAsync(Credit);
                    customerReciever.CreditAvailable += (creditTransfered - commission);
                    customerSender.CreditAvailable -= creditTransfered;
                    

                    return Credit.ConfirmationNumber;
                }
                catch (Exception ex)
                {

                    throw new Exception("La transacción ha fallado" + ex.Message);
                }
            }
            throw new Exception("El PIN ingresado es incorrecto, intentalo otra vez!");

        }
    }
}
