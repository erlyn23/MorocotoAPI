using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Morocoto.Infraestructure.Implementations
{
    public class PartnerRepository: GenericRepository<Partner>, IAsyncPartnerRepository
    {
        private readonly MorocotoDbContext _dbContext;

        public PartnerRepository(MorocotoDbContext dbContext): base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Partner> SearchByBusinesses(Business business)
        {
            return await _dbContext.Partners.Where(x => x.Businesses.Contains(business)).FirstOrDefaultAsync();
        }
    }
}
