using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yangtze.DAL.Entities;

namespace YangtzeAPI.Helper
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponseWrapper : ResultFactory
    {
        protected ActionResult<T> ResponseGet<T>((int statusCode, T value) result, string message = "")
        {
            return GetResponse(result, message);
        }
    }
}
