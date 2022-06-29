using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Models.ApiResultsModels
{
    public class FileDataModel
    {
        public string Message { get; set; }
        public byte[] Data { get; set; }
        public bool Success { get; set; }
    }
}
