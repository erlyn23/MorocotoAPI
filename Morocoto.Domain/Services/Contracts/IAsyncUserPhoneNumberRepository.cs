using System;
using System.Collections.Generic;
using System.Text;

using Morocoto.Domain.Models;

namespace Morocoto.Domain.Services.Contracts
{
    public interface IAsyncUserPhoneNumberRepository: IAsyncGenericRepository<UserPhoneNumber>
    {
    }
}
