using CaseStudy.Core.CrossCuttingConcerns.Logging;
using CaseStudy.Core.CrossCuttingConcerns.Logging.Log4Net;
using CaseStudy.Core.Utilities.Interceptors;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.Aspects.Autofac.LogAspects
{
    [Serializable]
    public class ExceptionLogAspect:MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase)) //2 tip gelebilir. 1- DatabaseLogger - 2 - FileLogger | Tip kontrolü
            {
                throw new Exception("Wrong logger type");
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService); //İlgili tiple ilgili dinamik olarak nesne oluştur
        }


        protected override void OnException(IInvocation invocation, Exception e) //Methodlarda hata meydana geldiğinde, bu fonksiyon otomatik araya girer
        {
            var exceptionLog = GetLogDetail(invocation);
            exceptionLog.ExceptionMessage = e.Message;
            _loggerServiceBase.Error(exceptionLog); //LoggerServiceBase içerisinde try ifadesinin catch bloğuna düşer.
        }


        private ExceptionLogDetail GetLogDetail(IInvocation invocation) //İlgili modellerin doldurulması
        {

            var logParameters = invocation.Method.GetParameters().Select((t, i) => new LogParameter
            {
                Name = t.Name,
                Type = t.ParameterType.Name,
                Value = invocation.Arguments[i]
            }).ToList();

            var exceptionlogDetail = new ExceptionLogDetail
            {
                FullName = invocation.Method.DeclaringType == null ? null : invocation.Method.DeclaringType.Name,
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                CreatedDate = DateTime.Now
            };

            return exceptionlogDetail;
        }
    }
}
