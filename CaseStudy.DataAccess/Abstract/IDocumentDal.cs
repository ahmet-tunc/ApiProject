using CaseStudy.Core.DataAccess;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.DataAccess.Abstract
{
    public interface IDocumentDal:IEntityRepository<Document>
    {
    }
}
