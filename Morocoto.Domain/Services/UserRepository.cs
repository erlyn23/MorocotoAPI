using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Services
{
    public class UserRepository: GenericRepository<User>, IAsyncUserRepository
    {
        public UserRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
   
    }
}
