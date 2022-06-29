using CaseStudy.Business.Abstract.SharedServices;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Concrete;
using CaseStudy.Entities.Concrete.ApiResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface ISoapDocumentService:ISharedService
    {
        IDataResult<FileData> GetFileData(string filename, string pdfpath);
    }
}
