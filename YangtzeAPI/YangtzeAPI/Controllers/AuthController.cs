using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.BLL.Services;
using YangtzeAPI.Helper;

namespace YangtzeAPI.Controllers
{
    public class AuthController : ResponseWrapper
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserForRegisterDto userForRegisterDto)
        {
            throw new Exception();
        } 
    }
}
