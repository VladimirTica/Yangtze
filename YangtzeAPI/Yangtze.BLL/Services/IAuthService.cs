using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.DAL.Entities;

namespace Yangtze.BLL.Services
{
    public interface IAuthService
    {

        Task<(int statusCode, UserForRegisterDto Value)> Register(UserForRegisterDto userForRegister);
        Task<(int statusCode, UserForRegisterDto Value)> Login(string username, string password);
    }
}
