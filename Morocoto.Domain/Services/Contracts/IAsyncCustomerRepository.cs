using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Domain.Services.Contracts
{
    public interface IAsyncCustomerRepository: IAsyncGenericRepository<Customer>
    {
    }
}
