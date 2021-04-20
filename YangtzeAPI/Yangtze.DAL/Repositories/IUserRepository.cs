using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace Yangtze.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int id);
        Task<User> AddUserAsync (User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> UserExists(string username);
        Task<User> UpdatePassword(User user);
        Task<User> DeleteUser(int id);
    }
}
