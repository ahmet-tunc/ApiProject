using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Models.ApiResultsModels
{
    public class DocumentApiResultViewModel
    {
        public List<DocumentViewModel> DocumentList { get; set; }
        public string Success { get; set; }
        public string Exception { get; set; }
    }
}
