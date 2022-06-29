using CaseStudy.Business.Abstract.SharedServices;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.ComplexTypes;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CaseStudy.Business.Abstract
{
    public interface IRestDocumentService:ISharedService
    {
        IDataResult<List<Document>> GetDailyDocumentsByCreateUser(string createUser, string url);
        IDataResult<List<Document>> GetDailyDocumentsByDate(int startDate, int? endDate, string url);
    }
}


//Restsharp API işlemlerinde kullanılabilir.