using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Implementations
{
    public class UnitOfWork: IAsyncUnitOfWork
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
        private readonly MorocotoDbContext _dbContext;
        public UnitOfWork(MorocotoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);
                return (UserRepository)_userRepository;
            }
        }

        public IAsyncUserAddressRepository UserAddressRepository
        {
            get
            {
                if (_userAddressRepository == null)
                    _userAddressRepository = new UserAddressRepository(_dbContext);
                return (UserAddressRepository)_userAddressRepository;
            }
        }
        public IAsyncUserPhoneNumberRepository UserPhoneNumberRepository
        {
            get
            {
                if (_userPhoneNumberRepository == null)
                    _userPhoneNumberRepository = new UserPhoneNumberRepository(_dbContext);
                return (UserPhoneNumberRepository)_userPhoneNumberRepository;
            }
        }

        public IAsyncBusinessRepository BusinessRepository
        {
            get
            {
                if (_businessRepository == null)
                    _businessRepository = new BusinessRepository(_dbContext);
                return (BusinessRepository)_businessRepository;
            }
        }
        public IAsyncBusinessBillRepository BusinessBillRepository
        {
            get
            {
                if (_businessBillRepository == null)
                    _businessBillRepository = new BusinessBillRepository(_dbContext);
                return (BusinessBillRepository)_businessBillRepository;
            }
        }
        public IAsyncBusinessPhoneNumberRepository BusinessPhoneNumberRepository
        {
            get
            {
                if (_businessPhoneNumberRepository == null)
                    _businessPhoneNumberRepository = new BusinessPhoneNumberRepository(_dbContext);
                return (BusinessPhoneNumberRepository)_businessPhoneNumberRepository;
            }
        }
        public IAsyncBusinessAddressRepository BusinessAddressRepository
        {
            get
            {
                if (_businessAddressRepository == null)
                    _businessAddressRepository = new BusinessAddressRepository(_dbContext);
                return (BusinessAddressRepository)_businessAddressRepository;
            }
        }
        public IAsyncBuyCreditRepository BuyCreditRepository
        {
            get
            {
                if (_buyCreditRepository == null)
                    _buyCreditRepository = new BuyCreditRepository(_dbContext, CustomerTaxesRepository, CustomerRepository, BusinessRepository,PartnerRepository, UserRepository
                        );
                return (BuyCreditRepository)_buyCreditRepository;
            }
        }
        public IAsyncCustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_dbContext);
                return (CustomerRepository)_customerRepository;
            }
        }
        public IAsyncCustomerTaxesRepository CustomerTaxesRepository
        {
            get
            {
                if (_customerTaxesRepository == null)
                    _customerTaxesRepository = new CustomerTaxesRepository(_dbContext);
                return (CustomerTaxesRepository)_customerTaxesRepository;
            }
        }
        public IAsyncPartnerRepository PartnerRepository
        {
            get
            {
                if (_partnerRepository == null)
                    _partnerRepository = new PartnerRepository(_dbContext);
                return (PartnerRepository)_partnerRepository;
            }
        }
        public IAsyncRequestRepository RequestRepository
        {
            get
            {
                if (_requestRepository == null)
                    _requestRepository = new RequestRepository(_dbContext);
                return (RequestRepository)_requestRepository;
            }
        }

        public IAsyncTransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                    _transactionRepository = new TransactionRepository(_dbContext, CustomerTaxesRepository, CustomerRepository,UserRepository);
                return (TransactionRepository)_transactionRepository;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        void IDisposable.Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
