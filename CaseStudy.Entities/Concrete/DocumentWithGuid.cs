using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Concrete
{
    public class DocumentWithGuid:IEntity
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string DocumentName { get; set; }
    }
}
