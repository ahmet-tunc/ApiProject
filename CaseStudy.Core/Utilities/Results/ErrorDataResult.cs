using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Core.Utilities.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data, string message, StatusCodeEnum statusCode) : base(data, false, message, statusCode)
        {
        }

        public ErrorDataResult(T data, StatusCodeEnum statusCode) : base(data, false , statusCode)
        {
        }

        public ErrorDataResult(string message) : base(default, false, message, StatusCodeEnum.BadRequest)
        {

        }

        public ErrorDataResult(StatusCodeEnum statusCode) : base(default, false, statusCode)
        {

        }

        public ErrorDataResult() : base(default, false, StatusCodeEnum.BadRequest)
        {

        }
    }
}
