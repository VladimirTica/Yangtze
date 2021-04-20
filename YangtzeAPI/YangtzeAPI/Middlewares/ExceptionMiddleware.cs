using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Yangtze.BLL.Helpers;
using YangtzeAPI.Errors;

namespace YangtzeAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (ApiResponseException ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                var response = _env.IsDevelopment()
                    ? new ApiException(ex.StatusCode, ex.Message, ex.StackTrace.ToString())
                    : new ApiException(ex.StatusCode);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }

            catch (Exception ex)
            {

            }
        }
    }
}
