using Morocoto.Infraestructure.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Services.Contracts
{
    public interface ICustomerService
    {
        Task<string> ExecuteTransactionAsync(TransactionsRequest model);
    }
}
