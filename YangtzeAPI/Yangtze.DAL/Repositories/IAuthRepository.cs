using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace Yangtze.DAL.Repositories
{
    public interface IAuthRepository
    {
        Task<User> AddUserAsync (User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> UserExists(string username);
    }
}
