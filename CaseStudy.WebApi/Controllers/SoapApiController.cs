using AutoMapper;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.WebApi.Helpers;
using CaseStudy.WebApi.Models.ApiResultsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace CaseStudy.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [DataContract]
    public class SoapApiController : LogHelper
    {
        private IMapper _mapper;
        private IConfiguration _configuration;
        private ISoapDocumentService _soapDocumentService;
        private string _getFileUrl;
        private string _pdfPathUrl;


        public SoapApiController(IConfiguration configuration, ISoapDocumentService soapDocumentService, IMapper mapper, ILogService logService):base(logService)
        {
            _configuration = configuration;
            _soapDocumentService = soapDocumentService;
            _getFileUrl = _configuration.GetSection("GetFileSoap").Value;
            _pdfPathUrl = _configuration.GetSection("PdfPath").Value;
            _mapper = mapper;
        }


        [HttpGet("GetFileData")]
        [OperationContract]
        public IActionResult GetFileData(string filename)
        {
            try
            {
                var result = _soapDocumentService.GetFileData(filename, _pdfPathUrl);
                AddLog((int)result.StatusCode, result.Message, MethodBase.GetCurrentMethod().Name);

                if (result.Success)
                {
                    return Ok(_mapper.Map<FileDataModel>(result.Data));
                }
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                AddLog((int)StatusCodeEnum.BadRequest, e.Message, MethodBase.GetCurrentMethod().Name);
                return BadRequest(e.Message);
            }

        }
    }
}
