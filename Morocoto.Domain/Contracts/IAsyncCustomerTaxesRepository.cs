using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncCustomerTaxesRepository: IAsyncGenericRepository<CustomerTaxis>
    {
        Task<CustomerTaxis> CalculateTax(int creditSelled);
        Task<CustomerTaxis> TaxesPeerToPeer();
    }
}
