using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncTransactionRepository: IAsyncGenericRepository<Transaction>
    {
    }
}
