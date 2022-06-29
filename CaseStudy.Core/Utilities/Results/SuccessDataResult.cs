using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data, string message, StatusCodeEnum statusCode) : base(data, true, message, statusCode)
        {
        }

        public SuccessDataResult(T data, string message) : base(data, true, message, StatusCodeEnum.Success)
        {
        }

        public SuccessDataResult(T data) : base(data, true, StatusCodeEnum.Success)
        {
        }

        public SuccessDataResult(T data, StatusCodeEnum statusCode) : base(data, true, statusCode)
        {
        }

        public SuccessDataResult(string message) : base(default, true, message, StatusCodeEnum.Success)
        {

        }

        public SuccessDataResult(StatusCodeEnum statusCode) : base(default, true, statusCode)
        {

        }

        public SuccessDataResult() : base(default, true, StatusCodeEnum.Success)
        {

        }
    }
}
