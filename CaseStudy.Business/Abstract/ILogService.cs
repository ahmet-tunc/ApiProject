using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface ILogService
    {
        IResult Add(Log log);
    }
}
