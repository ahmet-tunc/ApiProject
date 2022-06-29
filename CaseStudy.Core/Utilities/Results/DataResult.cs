using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data,bool success, string message, StatusCodeEnum statusCode) : base(success, message, statusCode)
        {
            Data = data;
        }

        public DataResult(T data, bool success, StatusCodeEnum statusCode) : base(success,statusCode)
        {
            Data = data;
        }

        public DataResult(T data, bool success):base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
