using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncUnitOfWork:IDisposable
    {
        Task<int> CompleteAsync();
        Task DisposeAsync();
    }
}
