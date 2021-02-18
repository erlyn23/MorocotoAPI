using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Implementations
{
    public class UnitOfWork
    {
        private IAsyncUserRepository _userRepository;
        private IAsyncUserAddressRepository _userAddressRepository;
        private IAsyncUserPhoneNumberRepository _userPhoneNumberRepository;
        private IAsyncBusinessRepository _businessRepository;
        private IAsyncBusinessBillRepository _businessBillRepository;
        private IAsyncBusinessPhoneNumberRepository _businessPhoneNumberRepository;
        private IAsyncBusinessAddressRepository _businessAddressRepository;
        private IAsyncBuyCreditRepository _buyCreditRepository;
        private IAsyncCustomerRepository _customerRepository;
        private IAsyncCustomerTaxesRepository _customerTaxesRepository;
        private IAsyncPartnerRepository _partnerRepository;
        private IAsyncRequestRepository _requestRepository;
        private IAsyncTransactionRepository _transactionRepository;
        private MorocotoDbContext _dbContext;
        public UnitOfWork(MorocotoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);
                return (UserRepository)_userRepository;
            }
        }

        public UserAddressRepository UserAddressRepository
        {
            get
            {
                if (_userAddressRepository == null)
                    _userAddressRepository = new UserAddressRepository(_dbContext);
                return (UserAddressRepository)_userAddressRepository;
            }
        }
        public UserPhoneNumberRepository UserPhoneNumberRepository
        {
            get
            {
                if (_userPhoneNumberRepository == null)
                    _userPhoneNumberRepository = new UserPhoneNumberRepository(_dbContext);
                return (UserPhoneNumberRepository)_userPhoneNumberRepository;
            }
        }

        public BusinessRepository BusinessRepository
        {
            get
            {
                if (_businessRepository == null)
                    _businessRepository = new BusinessRepository(_dbContext);
                return (BusinessRepository)_businessRepository;
            }
        }
        public BusinessBillRepository BusinessBillRepository
        {
            get
            {
                if (_businessBillRepository == null)
                    _businessBillRepository = new BusinessBillRepository(_dbContext);
                return (BusinessBillRepository)_businessBillRepository;
            }
        }
        public BusinessPhoneNumberRepository BusinessPhoneNumberRepository
        {
            get
            {
                if (_businessPhoneNumberRepository == null)
                    _businessPhoneNumberRepository = new BusinessPhoneNumberRepository(_dbContext);
                return (BusinessPhoneNumberRepository)_businessPhoneNumberRepository;
            }
        }
        public BusinessAddressRepository BusinessAddressRepository
        {
            get
            {
                if (_businessAddressRepository == null)
                    _businessAddressRepository = new BusinessAddressRepository(_dbContext);
                return (BusinessAddressRepository)_businessAddressRepository;
            }
        }
        public BuyCreditRepository BuyCreditRepository
        {
            get
            {
                if (_buyCreditRepository == null)
                    _buyCreditRepository = new BuyCreditRepository(_dbContext);
                return (BuyCreditRepository)_buyCreditRepository;
            }
        }
        public CustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_dbContext);
                return (CustomerRepository)_customerRepository;
            }
        }
        public CustomerTaxesRepository CustomerTaxesRepository
        {
            get
            {
                if (_customerTaxesRepository == null)
                    _customerTaxesRepository = new CustomerTaxesRepository(_dbContext);
                return (CustomerTaxesRepository)_customerTaxesRepository;
            }
        }
        public PartnerRepository PartnerRepository
        {
            get
            {
                if (_partnerRepository == null)
                    _partnerRepository = new PartnerRepository(_dbContext);
                return (PartnerRepository)_partnerRepository;
            }
        }
        public RequestRepository RequestRepository
        {
            get
            {
                if (_requestRepository == null)
                    _requestRepository = new RequestRepository(_dbContext);
                return (RequestRepository)_requestRepository;
            }
        }

        public TransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                    _transactionRepository = new TransactionRepository(_dbContext);
                return (TransactionRepository)_transactionRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
