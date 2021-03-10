using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.DAL.Entities;
using Yangtze.DAL.Repositories;

namespace Yangtze.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<(int statusCode, UserForRegisterDto Value)> Register(UserForRegisterDto userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();

            if (await _repo.UserExists(userForRegister.Username))
            {
                return (404, null);
            }

            var createdUser = await RegisterUser(userForRegister, userForRegister.Password);

            return (201, _mapper.Map<UserForRegisterDto>(createdUser));
        }


        public async Task<(int statusCode, UserForRegisterDto Value)> Login(string username, string password)
        {
            var user = await _repo.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return (404, null);
            }

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return (404, null);
            }

            return (200, _mapper.Map<UserForRegisterDto>(user));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<User> RegisterUser(UserForRegisterDto userForRegister, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePaswwordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userForRegister);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var addedUser = await _repo.AddUserAsync(user);

            return user;
        }

        private void CreatePaswwordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
             using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        
    }
}
