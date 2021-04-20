using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Helpers;
using Yangtze.BLL.Models;
using Yangtze.BLL.Models.DtoModels;
using Yangtze.DAL.Entities;
using Yangtze.DAL.Repositories;

namespace Yangtze.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserForDisplayDto> GetUserById(int id)
        {
            var user = await _repo.GetUserAsync(id);
            if (user == null)
            {
                throw new ApiResponseException(ApiExceptionType.NotFound, "Record does not exist");
            }

            return _mapper.Map<UserForDisplayDto>(user);
        }

        public async Task<UserForDisplayDto> Register(UserForRegisterDto userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();

            if (await _repo.UserExists(userForRegister.Username))
            {
                throw new ApiResponseException(ApiExceptionType.BadRequest, "This username is already taken");
            }

            var createdUser = await RegisterUser(userForRegister, userForRegister.Password);

            return _mapper.Map<UserForDisplayDto>(createdUser);
        }


        public async Task<UserForDisplayDto> Login(string username, string password)
        {
            var user = await _repo.GetUserByUsernameAsync(username);

            if (user == null)
            {
                throw new ApiResponseException(ApiExceptionType.Unauthorized, "Invalid username or password");
            }

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new ApiResponseException(ApiExceptionType.Unauthorized, "Invalid username or password");
            }

            return  _mapper.Map<UserForDisplayDto>(user);
        }

        public async Task<UserForDisplayDto> ChangePassword(UserForUpdateDto userForUpdate)
        {
            var user = await Login(userForUpdate.Username, userForUpdate.OldPassword);

            var updatedUser = await UpdatePassword(userForUpdate, userForUpdate.Password);
            if (updatedUser == null)
            {
                throw new ApiResponseException(ApiExceptionType.BadRequest, "Failed to change password");
            }

            return _mapper.Map<UserForDisplayDto>(updatedUser);
        }

        public async Task<UserForDisplayDto> DeleteUser(int id)
        {
            var user = await _repo.DeleteUser(id);
            if (user == null)
            {
                throw new ApiResponseException(ApiExceptionType.BadRequest, "Failed to delete user");
            }

            return  _mapper.Map<UserForDisplayDto>(user);
            
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

        private async Task<User> RegisterUser(UserForRegisterDto userForRegister, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePaswwordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userForRegister);

            SetPasswordHashAndSalt(user, passwordHash, passwordSalt);
            
            await _repo.AddUserAsync(user);

            return user;
        }

        private async Task<User> UpdatePassword(UserForRegisterDto userForRegister, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePaswwordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userForRegister);

            SetPasswordHashAndSalt(user, passwordHash, passwordSalt);

            var updatedUser = await _repo.UpdatePassword(user);

            return updatedUser;
        }

        private void SetPasswordHashAndSalt(User user, byte[] passwordHash, byte[] passwordSalt)
        {
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
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
