using Morocoto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Domain.Services.Contracts
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int userId);
        Task SaveUserAsync(User user);
        void UpdateUser(User user);
        Task DeleteUserAsync(int userId);
        Task<int> SaveChangesAsync();
    }
}
