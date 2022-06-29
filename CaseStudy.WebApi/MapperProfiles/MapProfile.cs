using AutoMapper;
using CaseStudy.Entities.ComplexTypes;
using CaseStudy.Entities.Concrete;
using CaseStudy.Entities.Concrete.ApiResults;
using CaseStudy.WebApi.Models;
using CaseStudy.WebApi.Models.ApiResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.MapperProfiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<DocumentViewModel, Document>().ReverseMap();
            CreateMap<DocumentApiResultViewModel, DocumentApiResult>().ReverseMap();
            CreateMap<DocumentWithGuid, DocumentWithGuidViewModel>().ReverseMap();
            CreateMap<FileData, FileDataModel>().ReverseMap();
        }
    }
}
