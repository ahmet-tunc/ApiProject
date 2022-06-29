using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Models.ApiResultsModels
{
    public class DocumentWithGuidViewModel
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string DocumentName { get; set; }

    }
}
