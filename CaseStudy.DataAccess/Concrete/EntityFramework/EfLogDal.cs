using CaseStudy.Core.DataAccess.EntityFramework;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.DataAccess.Concrete.EntityFramework
{
    public class EfLogDal:EfEntityRepositoryBase<Log,AppDbContext>,ILogDal
    {

    }
}
