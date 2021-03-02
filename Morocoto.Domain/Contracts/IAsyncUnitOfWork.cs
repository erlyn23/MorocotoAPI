using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncUnitOfWork:IDisposable
    {
        IAsyncUserRepository UserRepository { get; }
        IAsyncUserAddressRepository UserAddressRepository { get; }
        IAsyncUserPhoneNumberRepository UserPhoneNumberRepository { get; }
        IAsyncBusinessRepository BusinessRepository { get; }
        IAsyncBusinessBillRepository BusinessBillRepository { get; }
        IAsyncBusinessPhoneNumberRepository BusinessPhoneNumberRepository { get; }
        IAsyncBusinessAddressRepository BusinessAddressRepository { get; }
        IAsyncBuyCreditRepository BuyCreditRepository { get; }
        IAsyncCustomerRepository CustomerRepository { get; }
        IAsyncCustomerTaxesRepository CustomerTaxesRepository { get; }
        IAsyncPartnerRepository PartnerRepository { get; }
        IAsyncRequestRepository RequestRepository { get; }
        IAsyncTransactionRepository TransactionRepository { get; }
        Task<int> CompleteAsync();
        Task DisposeAsync();
    }
}
