using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Services
{
    public class UnitOfWork
    {
        private IAsyncUserRepository _userRepository;
        private MorocotoDbContext _dbContext;
        public UnitOfWork(MorocotoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);
                return (UserRepository)_userRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
