using System;
using System.Collections.Generic;
using System.Text;

namespace Yangtze.BLL.Helpers
{
    public class ApiResponseException : Exception
    {
        public int StatusCode { get; set; }

        public ApiResponseException()
        {
            StatusCode = 0;
        }

        public ApiResponseException(int exType)
        {
            StatusCode = exType;
        }

        public ApiResponseException(ApiExceptionType exType)
        {
            StatusCode = (int)exType;
        }


        public ApiResponseException(int statusCode, string message): base(message)
        {
            StatusCode = statusCode;
        }

        public ApiResponseException(ApiExceptionType exType, string message) : base(message)
        {
            StatusCode = (int)exType;
        }
    }

    public enum ApiExceptionType
    {
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        MethodNotAllowed = 405,
        InternalError = 500,
        Custom = 900
    }
}
