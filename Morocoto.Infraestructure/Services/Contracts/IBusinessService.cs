using Morocoto.Infraestructure.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Services.Contracts
{
    public interface IBusinessService
    {
        Task<int> SaveBusinessAsync(BusinessRequest businessRequest);
    }
}
