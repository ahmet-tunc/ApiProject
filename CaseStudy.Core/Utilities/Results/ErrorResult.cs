using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message, StatusCodeEnum statusCode) : base(false, message, statusCode)
        {
        }

        public ErrorResult(string message) : base(false, message, StatusCodeEnum.BadRequest)
        {
        }

        public ErrorResult() : base(false, StatusCodeEnum.BadRequest)
        {
        }
    }
}
