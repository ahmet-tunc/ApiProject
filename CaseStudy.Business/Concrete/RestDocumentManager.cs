using CaseStudy.Business.Abstract;
using CaseStudy.Business.Concrete.BaseManagers;
using CaseStudy.Business.MagicString;
using CaseStudy.Core.Aspects.Autofac.CacheAspects;
using CaseStudy.Core.Aspects.Autofac.LogAspects;
using CaseStudy.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CaseStudy.Core.Helpers;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.ComplexTypes;
using CaseStudy.Entities.Concrete;
using CaseStudy.Entities.Concrete.ApiResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class RestDocumentManager : DocumentBaseManager, IRestDocumentService
    {


        [LogAspect(typeof(FileLogger), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheAspect(duration: 10,Priority = 3)]
        public IDataResult<List<Document>> GetDailyDocumentsByCreateUser(string createUser, string url)
        {
            var list = this.GetData(url);
            if (list.Result.Success)
            {
                return new SuccessDataResult<List<Document>>(list.Result.Data.DocumentList.Where(x => x.CreateUser == createUser).ToList(), Messages.SuccessMessage);
            }
            return new ErrorDataResult<List<Document>>();
        }



        [LogAspect(typeof(FileLogger), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheAspect(duration: 10, Priority = 3)]
        public IDataResult<List<Document>> GetDailyDocumentsByDate(int startTime, int? endTime, string url)
        {
            var list = this.GetData(url);
            if (list.Result.Success)
            {
                return new SuccessDataResult<List<Document>>(list.Result.Data.DocumentList
                    .Where(x => startTime < x.CreatedDate.Hour && (endTime != null || endTime != 0 ? endTime > x.CreatedDate.Hour : true)).ToList());
            }
            return new ErrorDataResult<List<Document>>();
        }

    }
}
