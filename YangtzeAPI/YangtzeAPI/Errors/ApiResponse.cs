using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangtzeAPI.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public ApiResponse(int statusCode, string message = null, string details=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Details = details;
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                401 => "Authorization failed",
                400 => "You have made a bad request.",
                404 => "Resource not found.",
                405 => "Method not allowed",
                500 => "Server error occured",
                _ => null,
            };
        }
    }
}
