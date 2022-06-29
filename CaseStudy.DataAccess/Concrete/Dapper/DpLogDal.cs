using CaseStudy.Core.DataAccess.Dapper;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.DataAccess.Concrete.Dapper
{
    public class DpLogDal:DpEntityRepositoryBase<Log>,ILogDal
    {
        public DpLogDal(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
