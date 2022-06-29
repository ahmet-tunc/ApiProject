using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class LogManager : ILogService
    {
        private ILogDal _logDal;

        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public IResult Add(Log log)
        {
            var addedLog = _logDal.Add(log);
            if (addedLog != null)
                return new SuccessResult();

            return new ErrorResult();
        }
    }
}
