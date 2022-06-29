using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Core.Utilities.Results
{
    public class Result:IResult
    {
        public Result(bool success, string message, StatusCodeEnum statusCode):this(success, statusCode)
        {
            Message = message;
        }

        public Result(bool success, StatusCodeEnum statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
        public StatusCodeEnum StatusCode { get; }
    }
}
