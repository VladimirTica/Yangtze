using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace YangtzeAPI.Helper
{
    public class ResultFactory : ControllerBase
    {
        protected ActionResult GetResponse<T>((int statusCode, T value) result, string message)
        {
            switch (result.statusCode) {
                case 200:
                    {
                        return StatusCode(StatusCodes.Status200OK, result.value);
                    }
                case 201:
                    {
                        return StatusCode(StatusCodes.Status201Created, result.value);
                    }
                case 400:
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, message);
                    }
                case 404:
                    {
                        return StatusCode(StatusCodes.Status404NotFound, message);
                    }
                default:
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, message);
                    }
            }

        }
    }
}
