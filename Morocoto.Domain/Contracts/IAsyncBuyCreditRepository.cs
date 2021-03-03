using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncBuyCreditRepository: IAsyncGenericRepository<BuyCredit>
    {
        Task<string> SellCredit(string accountBusiness, string accountCustomer, int creditSelled, string PIN);
    }
}
