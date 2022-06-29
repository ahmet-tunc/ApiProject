using CaseStudy.Core.CrossCuttingConcerns.Logging;
using CaseStudy.Core.CrossCuttingConcerns.Logging.Log4Net;
using CaseStudy.Core.DataAccess;
using CaseStudy.Core.Helpers;
using CaseStudy.Core.Utilities.Interceptors;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.Aspects.Autofac.LogAspects
{
    [Serializable]
    //[MulticastAttributeUsage(MulticastTargets.Method,TargetMemberAttributes = MulticastAttributes.Instance)]
    public class LogAspect:MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase)) //Tip kontrolü
            {
                throw new Exception("Wrong logger type");
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService); //Dinamik nesne oluşturma
        }


        protected override void OnBefore(IInvocation invocation) //Method çalıştırılmadan hemen önce
        {
            _loggerServiceBase.Info(GetLogDetail(invocation)); //LoggerServiceBase içerisinde try bloğunun hemen öncesi
        }


        private LogDetail GetLogDetail(IInvocation invocation) //İlgili modeller doldurulur
        {

            var logParameters = invocation.Method.GetParameters().Select((t, i) => new LogParameter
            {
                Name = t.Name,
                Type = t.ParameterType.Name,
                Value = invocation.Arguments[i]
            }).ToList();

            var logDetail = new LogDetail
            {
                FullName = invocation.Method.DeclaringType == null ? null : invocation.Method.DeclaringType.Name,
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                CreatedDate = DateTime.Now
            };

            return logDetail;
        }

    }
}
