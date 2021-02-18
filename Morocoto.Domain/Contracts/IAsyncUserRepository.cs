using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Morocoto.Domain.Models;

namespace Morocoto.Domain.Contracts
{
    public interface IAsyncUserRepository: IAsyncGenericRepository<User>
    {
        //Here we code methods for complementary searching that the generic repository would not include. All the generic methods are coded
        // in GenericRepository class, encapsulate those here is unnecesary.
    }
}
