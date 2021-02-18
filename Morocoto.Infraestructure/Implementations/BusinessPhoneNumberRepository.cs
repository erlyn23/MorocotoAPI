using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;

namespace Morocoto.Infraestructure.Implementations
{
    public class BusinessPhoneNumberRepository: GenericRepository<BusinessPhoneNumber>, IAsyncBusinessPhoneNumberRepository
    {
        public BusinessPhoneNumberRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
    }
}
