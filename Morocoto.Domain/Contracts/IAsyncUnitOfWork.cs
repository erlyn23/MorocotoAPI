using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncUnitOfWork:IDisposable
    {
        IAsyncUserRepository User { get; }
        IAsyncUserAddressRepository UserAddress { get; }
        IAsyncUserPhoneNumberRepository UserPhone { get; }
        IAsyncPartnerRepository Partner { get; }
        IAsyncCustomerRepository Customer { get; }
        IAsyncCustomerTaxesRepository CustomerTaxes { get; }
        IAsyncBusinessRepository Business { get; }
        IAsyncBusinessBillRepository BusinessBill { get; }
        IAsyncBusinessAddressRepository BusinessAddress { get; }
        IAsyncBusinessPhoneNumberRepository BusinessPhoneNumber { get; }
        IAsyncBuyCreditRepository BuyCredit { get; }
        IAsyncRequestRepository Request { get; }
        IAsyncTransactionRepository Transactions { get; }
        Task<int> Complete();
        
    }
}
