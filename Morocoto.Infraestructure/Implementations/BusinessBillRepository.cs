using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;

namespace Morocoto.Infraestructure.Implementations
{
    public class BusinessBillRepository: GenericRepository<BusinessBill>, IAsyncBusinessBillRepository
    {
        public BusinessBillRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
    }
}
