using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Models
{
    public class DocumentViewModel
    {
        public string DocumentName { get; set; }
        public string PolicyNumber { get; set; }
        public string Status { get; set; } // - - > Enum oluşturulacak
        public string CreateUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
