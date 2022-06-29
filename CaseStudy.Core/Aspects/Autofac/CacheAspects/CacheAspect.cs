using CaseStudy.Core.CrossCuttingConcerns.Caching;
using CaseStudy.Core.Utilities.Interceptors;
using CaseStudy.Core.Utilities.IoC;
using Castle.DynamicProxy;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStudy.Core.Aspects.Autofac.CacheAspects
{
    [Serializable]
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration; //Cache süresi
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //ServiceTool üzerinden ilgili middleware çekme işlemi
        }


        public override void Intercept(IInvocation invocation) // Method çalışmadan hemen önce araya girer
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key)) // Cache içerisinde daha öncesinde ilgili key bilgisine sahip verinin varlığına dair kontrol
            {
                invocation.ReturnValue = _cacheManager.Get(key);  //Eğer böyle bir veri varsa, geriye döndür
                return;
            }
            invocation.Proceed(); //Yoksa, methodu çalıştır
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //method çıktısını cache olarak ekle
        }
    }
}
