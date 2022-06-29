using AutoMapper;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Publisher.RabbitMQ;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.DataAccess.Concrete;
using CaseStudy.Entities.Concrete;
using CaseStudy.WebApi.Constant;
using CaseStudy.WebApi.Helpers;
using CaseStudy.WebApi.Models;
using CaseStudy.WebApi.Models.ApiResultsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace CaseStudy.WebUI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class RestApiController : LogHelper
    {
        private IMapper _mapper;
        private IConfiguration _configuration;
        private IRestDocumentService _dailyDocumentService;
        private readonly ClientPublisher _clientPublisher;
        private readonly string _getDailyDocumentsUrl;
        private readonly string _getFileDataUrl;
        private readonly string _pdfFilePath;


        public RestApiController(IConfiguration configuration, IRestDocumentService dailyDocumentService, IMapper mapper, ClientPublisher clientPublisher, ILogService logService):base(logService)
        {
            _configuration = configuration;
            _dailyDocumentService = dailyDocumentService;
            _getDailyDocumentsUrl = _configuration.GetSection("GetDailyDocuments").Value;
            _getFileDataUrl = _configuration.GetSection("GetFileData").Value;
            _pdfFilePath = _configuration.GetSection("PdfPath").Value;
            _clientPublisher = clientPublisher;
            _mapper = mapper;
        }


        [HttpGet("GetDailyDocuments"), Authorize]
        public IActionResult GetDailyDocuments()
        {
            try
            {
                var result = _dailyDocumentService.GetData(_getDailyDocumentsUrl).Result;
                AddLog((int)result.StatusCode, result.Message, MethodBase.GetCurrentMethod().Name);

                if (result.Success)
                {
                    return Ok(_mapper.Map<DocumentApiResultViewModel>(result.Data));
                }
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                AddLog((int)StatusCodeEnum.BadRequest, e.Message, MethodBase.GetCurrentMethod().Name);
                return BadRequest(e.Message);
            }
        }



        [HttpGet("GetDailyDocumentsByCreateUser"), Authorize]
        public IActionResult GetDailyDocumentsByCreateUser(string createUser)
        {
            try
            {
                var result = _dailyDocumentService.GetDailyDocumentsByCreateUser(createUser, _getDailyDocumentsUrl);
                AddLog((int)result.StatusCode, result.Message, MethodBase.GetCurrentMethod().Name);
                if (result.Success)
                {
                    return Ok(_mapper.Map<List<DocumentViewModel>>(result.Data));
                }
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                AddLog((int)StatusCodeEnum.BadRequest, e.Message, MethodBase.GetCurrentMethod().Name);
                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetDailyDocumentsByDate"), Authorize]
        public IActionResult GetDailyDocumentsByDate(int startDate, int? endDate)
        {
            try
            {
                var result = _dailyDocumentService.GetDailyDocumentsByDate(startDate, endDate, _getDailyDocumentsUrl);
                AddLog((int)result.StatusCode, result.Message, MethodBase.GetCurrentMethod().Name);

                if (result.Success)
                {
                    return Ok(_mapper.Map<List<DocumentViewModel>>(result.Data));
                }
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                AddLog((int)StatusCodeEnum.BadRequest, e.Message, MethodBase.GetCurrentMethod().Name);
                return BadRequest(e.Message);
            }
        }


        [HttpPost("PdfCreateTrigger"), Authorize]
        public IActionResult PdfCreateTrigger(string createUser, DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = _dailyDocumentService.PdfCreateTrigger(createUser, startDate, endDate, _getDailyDocumentsUrl, _getFileDataUrl);
                AddLog((int)result.StatusCode, result.Message, MethodBase.GetCurrentMethod().Name);
                if (result.Success)
                {
                    var mapResult = _mapper.Map<List<DocumentWithGuidViewModel>>(result.Data);
                    _clientPublisher.PublisherList<DocumentWithGuidViewModel>(mapResult);
                    return Ok(mapResult);
                }
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                AddLog((int)StatusCodeEnum.BadRequest, e.Message, MethodBase.GetCurrentMethod().Name);
                return BadRequest(e.Message);
            }
        }


        [HttpGet("CheckPdfCreated"), Authorize]
        public IActionResult CheckPdfCreated(Guid guid)
        {
            try
            {
                var result = _dailyDocumentService.CheckPdfCreated(guid, _pdfFilePath);
                AddLog((int)result.StatusCode, result.Message, MethodBase.GetCurrentMethod().Name);

                if (result.Success)
                {
                    return Ok(Messages.IfThereIsPDF);
                }
                return NotFound(Messages.IfThereIsNoPDF);
            }
            catch (Exception e)
            {
                AddLog((int)StatusCodeEnum.BadRequest, e.Message, MethodBase.GetCurrentMethod().Name);
                return BadRequest(e.Message);
            }
        }



        [HttpGet("GetPdfCreated"), Authorize]
        public IActionResult GetPdfCreated(Guid guid)
        {
            try
            {
                var result = _dailyDocumentService.GetPdfCreated(guid,_pdfFilePath);
                AddLog((int)result.StatusCode, result.Message, MethodBase.GetCurrentMethod().Name);

                if (result.Success)
                {
                    return Ok(result.Message);
                }
                return NotFound(Messages.IfThereIsNoPDF);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
