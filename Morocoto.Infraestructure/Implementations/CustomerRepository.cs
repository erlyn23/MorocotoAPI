using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;

namespace Morocoto.Infraestructure.Implementations
{
    public class CustomerRepository: GenericRepository<Customer>, IAsyncCustomerRepository
    {
        public CustomerRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
    }
}
