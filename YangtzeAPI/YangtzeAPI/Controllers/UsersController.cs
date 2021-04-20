using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yangtze.BLL.Models;
using Yangtze.BLL.Models.DtoModels;
using Yangtze.BLL.Services;
using Yangtze.DAL.Helpers;
using YangtzeAPI.Helper;

namespace YangtzeAPI.Controllers
{
    [Authorize]
    
    public class UsersController : BaseApiController
    {
        private readonly IUserService _service;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService service, IOptions<AppSettings> appSettings)
        {
            _service = service;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{id}", Name ="GetUserById")]
        public async Task<ActionResult<UserForDisplayDto>> GetUser(int id)
        {
            var result = await _service.GetUserById(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserForDisplayDto>> Register (UserForRegisterDto userForRegisterDto)
        {
            var result = await _service.Register(userForRegisterDto);

            return CreatedAtRoute("GetUserById", new { id = result.Id }, result);
        } 

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserForDisplayDto>> Login(UserForRegisterDto userForRegisterDto)
        {
            var result = await _service.Login(userForRegisterDto.Username, userForRegisterDto.Password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, result.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(
                new
                {
                    Result = result,
                    Token = tokenString
                }
            );

        }

        [AllowAnonymous]
        [HttpPut("changePassword")]
        public async Task<ActionResult<UserForDisplayDto>> ChangePassword(UserForUpdateDto userForUpdateDto, int userId)
        {
            var result = await _service.ChangePassword(userForUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserForDisplayDto>> Delete(int id)
        {
            var result = await _service.DeleteUser(id);
            return Ok(result);
        }
    }
}
