using CaseStudy.Core.Entities;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.ComplexTypes
{
    public class DocumentApiResult:IEntity
    {
        public List<Document> DocumentList { get; set; }
        public string Success { get; set; }
        public string Exception { get; set; }
    }
}
