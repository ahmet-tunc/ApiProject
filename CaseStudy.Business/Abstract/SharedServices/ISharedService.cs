using CaseStudy.Core.SharedModels;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.ComplexTypes;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract.SharedServices
{
    public interface ISharedService
    {
        Task<IDataResult<DocumentApiResult>> GetData(string url);
        IResult CheckPdfCreated(Guid guid, string url);
        IDataResult<string> GetPdfCreated(Guid guid, string url);
        IDataResult<PdfFile> GetFile(Guid guid, string url);
        IDataResult<List<DocumentWithGuid>> PdfCreateTrigger(string createUser, DateTime startDate, DateTime endDate, string dataurl, string url);
    }
}
