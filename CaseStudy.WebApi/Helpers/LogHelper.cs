using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Helpers
{
    public abstract class LogHelper: ControllerBase
    {
        private ILogService _logService;
        public LogHelper(ILogService logService)
        {
            _logService = logService;
        }
        protected void AddLog(int statusCode, string logDetail, string methodName)
        {
            var checkLog = _logService.Add(new Log
            {
                CreatedDate = DateTime.Now,
                StatusCode = statusCode,
                Method = methodName,
                LogDetail = logDetail
            });
        }

    }
}
