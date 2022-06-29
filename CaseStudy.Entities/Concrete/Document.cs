using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Concrete
{
    public class Document:IEntity
    {
        public string DocumentName { get; set; }
        public string PolicyNumber { get; set; }
        public string Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
