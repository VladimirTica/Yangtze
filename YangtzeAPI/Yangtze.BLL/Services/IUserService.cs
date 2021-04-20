using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.BLL.Models.DtoModels;
using Yangtze.DAL.Entities;

namespace Yangtze.BLL.Services
{
    public interface IUserService
    {
        Task<UserForDisplayDto> GetUserById(int id);
        Task<UserForDisplayDto> Register(UserForRegisterDto userForRegister);
        Task<UserForDisplayDto> Login(string username, string password);
        Task<UserForDisplayDto> ChangePassword(UserForUpdateDto userForUpdate);
        Task<UserForDisplayDto> DeleteUser(int id);
    }
}
