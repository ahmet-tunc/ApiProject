using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Concrete.ApiResults
{
    public class FileData:IEntity
    {
        public string Message { get; set; }
        public byte[] Data { get; set; }
        public bool Success { get; set; }
    }
}
