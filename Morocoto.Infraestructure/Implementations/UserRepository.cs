using Morocoto.Domain.Contracts;
using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Implementations
{
    public class UserRepository: GenericRepository<User>, IAsyncUserRepository
    {
        public UserRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
   
    }
}
