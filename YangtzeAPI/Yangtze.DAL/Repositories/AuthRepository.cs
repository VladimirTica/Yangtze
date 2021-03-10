using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace Yangtze.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        public async Task<User> AddUserAsync(User user)
        {
            using(var db = new YangtzeDBContext())
            {
                await db.User.AddAsync(user);
                await db.SaveChangesAsync();
                return user;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (var db = new YangtzeDBContext())
            {
                var user = await db.User.FirstOrDefaultAsync(u => u.Username == username);
                return user;
            }
        }


        public async Task<bool> UserExists(string username)
        {
            using (var db = new YangtzeDBContext())
            {
                var user = await db.User.FirstOrDefaultAsync(u => u.Username == username);
                return user == null ? false : true;
            }
        }
    }
}
