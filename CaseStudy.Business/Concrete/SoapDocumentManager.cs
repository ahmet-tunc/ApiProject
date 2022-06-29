using CaseStudy.Business.Abstract;
using CaseStudy.Business.Concrete.BaseManagers;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Concrete;
using CaseStudy.Entities.Concrete.ApiResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaseStudy.Business.Concrete
{
    public class SoapDocumentManager:DocumentBaseManager,ISoapDocumentService
    {
        private readonly SoapService.Service1SoapClient _connection;
        public SoapDocumentManager()
        {
            _connection = new SoapService.Service1SoapClient(SoapService.Service1SoapClient.EndpointConfiguration.Service1Soap);
        }


        public override IDataResult<List<DocumentWithGuid>> PdfCreateTrigger(string createUser, DateTime startDate, DateTime endDate, string dataurl, string url)
        {
            var list = this.GetData(dataurl);
            if (list.Result.Success)
            {
                var documents = list.Result.Data.DocumentList
                   .Where(x => (!String.IsNullOrEmpty(createUser) ? x.CreateUser == createUser : true) &&
                   (startDate != default ? startDate < x.CreatedDate : true)
                   && (endDate != default ? endDate > x.CreatedDate : true)).ToList();

                var dailyDocumentWithGuidList = new List<DocumentWithGuid>();
                foreach (var document in documents)
                {
                    dailyDocumentWithGuidList.Add(new DocumentWithGuid
                    {
                        DocumentName = document.DocumentName
                    });
                }
                return new SuccessDataResult<List<DocumentWithGuid>>(dailyDocumentWithGuidList);
            }
            return new ErrorDataResult<List<DocumentWithGuid>>();
        }


        public IDataResult<FileData> GetFileData(string filename, string pdfpath)
        {
            var list = _connection.GetAllArchive(filename);

            SoapService.VEDocument data = _connection.GetFileData(filename);
            var model = new FileData
            {
                Data = data.data,
                Message = data.message,
                Success = data.success
            };

            if (model.Success)
            {
                File.WriteAllBytes(pdfpath + Guid.NewGuid().ToString() + ".pdf", model.Data);
            }

            return new SuccessDataResult<FileData>(model);
        }
    }
}
