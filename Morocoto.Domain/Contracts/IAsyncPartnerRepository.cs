using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncPartnerRepository: IAsyncGenericRepository<Partner>
    {
        Task<Partner> SearchByBusinesses(Business business);
    }
}
