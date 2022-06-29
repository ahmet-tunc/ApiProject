using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.CrossCuttingConcerns.Logging
{
    public class ExceptionLogDetail:LogDetail
    {
        public string ExceptionMessage { get; set; }
    }
}
