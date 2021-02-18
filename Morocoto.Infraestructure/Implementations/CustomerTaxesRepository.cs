﻿using System;
using System.Collections.Generic;
using System.Text;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Contracts;

namespace Morocoto.Infraestructure.Implementations
{
    public class CustomerTaxesRepository: GenericRepository<CustomerTaxis>, IAsyncCustomerTaxesRepository
    {
        public CustomerTaxesRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
    }
}
