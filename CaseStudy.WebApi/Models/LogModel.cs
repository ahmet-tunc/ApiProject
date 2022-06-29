using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string LogDetail { get; set; }
        public int StatusCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
