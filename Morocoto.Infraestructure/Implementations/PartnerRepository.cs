using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;

namespace Morocoto.Infraestructure.Implementations
{
    public class PartnerRepository: GenericRepository<Partner>, IAsyncPartnerRepository
    {
        public PartnerRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
    }
}
