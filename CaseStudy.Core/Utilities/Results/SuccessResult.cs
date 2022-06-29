using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message, StatusCodeEnum statusCode) : base(true, message, statusCode)
        {
        }

        public SuccessResult(string message) : base(true, message, StatusCodeEnum.Success)
        {
        }

        public SuccessResult() : base(true, StatusCodeEnum.Success)
        {
        }
    }
}
