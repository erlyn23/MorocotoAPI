using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncTransactionRepository: IAsyncGenericRepository<Transaction>
    {
        Task<string> Transaction(string accountSender, string accountReciever, int creditSelled, string PIN);
    }
}
