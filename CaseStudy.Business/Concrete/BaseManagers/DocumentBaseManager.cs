using CaseStudy.Business.Abstract.BaseServices;
using CaseStudy.Business.MagicString;
using CaseStudy.Core.Aspects.Autofac.CacheAspects;
using CaseStudy.Core.Aspects.Autofac.LogAspects;
using CaseStudy.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CaseStudy.Core.Helpers;
using CaseStudy.Core.SharedModels;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.ComplexTypes;
using CaseStudy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete.BaseManagers
{
    public abstract class DocumentBaseManager:IDocumentBaseService
    {
        [LogAspect(typeof(FileLogger), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheAspect(duration: 10, Priority = 3)]
        public async Task<IDataResult<DocumentApiResult>> GetData(string url)
        {
            return new SuccessDataResult<DocumentApiResult>(await ApiHelper<DocumentApiResult>
                .GetData(url),Messages.SuccessMessage,StatusCodeEnum.Success);
        }


        public IResult CheckPdfCreated(Guid guid, string url)
        {
            var pdfFile = GetFile(guid, url);
            if (pdfFile.Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }



        public IDataResult<PdfFile> GetFile(Guid guid, string url)
        {
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(url);
            var file = hdDirectoryInWhichToSearch.GetFiles("*" + guid + "*.*");
            if (file.Length > 0)
            {
                if (file != null)
                    return new SuccessDataResult<PdfFile>(file[0].FullName);
            }
            return new ErrorDataResult<PdfFile>(StatusCodeEnum.NotFound);
        }




        public IDataResult<string> GetPdfCreated(Guid guid, string url)
        {
            var pdfFile = GetFile(guid, url);
            if (pdfFile.Success)
            {
                return new SuccessDataResult<string>(pdfFile.Message);
            }
            return new ErrorDataResult<string>();
        }




        [LogAspect(typeof(FileLogger), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheAspect(duration: 10, Priority = 3)]
        public virtual IDataResult<List<DocumentWithGuid>> PdfCreateTrigger(string createUser, DateTime startDate, DateTime endDate, string dataurl, string url)
        {
            var list = this.GetData(dataurl);
            if (list.Result.Success)
            {
                var documents = list.Result.Data?.DocumentList
                   .Where(x => (!String.IsNullOrEmpty(createUser) ? x.CreateUser == createUser : true) &&
                   (startDate != default ? startDate < x.CreatedDate : true)
                   && (endDate != default ? endDate > x.CreatedDate : true)).ToList();

                var dailyDocumentWithGuidList = new List<DocumentWithGuid>();
                if (documents != null && documents.Count > 0)
                {
                    foreach (var document in documents)
                    {
                        dailyDocumentWithGuidList.Add(new DocumentWithGuid
                        {
                            DocumentName = document.DocumentName
                        });
                    }
                }
                return new SuccessDataResult<List<DocumentWithGuid>>(dailyDocumentWithGuidList);
            }
            return new ErrorDataResult<List<DocumentWithGuid>>();
        }
    }
}
