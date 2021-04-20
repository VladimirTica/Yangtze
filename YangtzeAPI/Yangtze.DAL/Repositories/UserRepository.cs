using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace Yangtze.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {

        public async Task<User> GetUserAsync(int id)
        {
            using (var db = new YangtzeDBContext())
            {
               return await db.User.FirstOrDefaultAsync(u=> u.Id==id);
            }
        }

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

        public async Task<User> UpdatePassword(User user)
        {
            using (var db = new YangtzeDBContext())
            {
                var entity = await db.User.FirstOrDefaultAsync(u => u.Username == user.Username);
                if (entity==null)
                {
                    return null;
                }

                entity.PasswordHash = user.PasswordHash;
                entity.PasswordSalt = user.PasswordSalt;

                await db.SaveChangesAsync();

                return entity;
            }
        }
        public async Task<User> DeleteUser(int userId)
        {
            using (var db = new YangtzeDBContext())
            {
                var entity = await db.User.FirstOrDefaultAsync(u => u.Id == userId);
                if (entity == null)
                {
                    return null;
                }

                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Product.RemoveRange(db.Product.Where(p => p.UserId == userId));
                        db.User.Remove(entity);
                        await db.SaveChangesAsync();
                    }

                    catch(Exception ex)
                    {
                        await dbContextTransaction.RollbackAsync();
                    }                   

                    await dbContextTransaction.CommitAsync();
                }
                
                return entity;
            }
        } 
    }
}
