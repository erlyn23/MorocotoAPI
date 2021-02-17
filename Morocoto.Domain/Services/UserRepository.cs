using Morocoto.Domain.DbContexts;
using Morocoto.Domain.Models;
using Morocoto.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Services
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(MorocotoDbContext dbContext): base(dbContext)
        {

        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await GetAllElementsAsync();
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await GetElementByIdAsync(userId);
        }

        public async Task SaveUserAsync(User user)
        {
            await SaveElementAsync(user);
        }

        public void UpdateUser(User user)
        {
            UpdateElement(user);
        }
        public async Task DeleteUserAsync(int userId)
        {
            await DeleteElementAsync(userId);
        }
    }
}
