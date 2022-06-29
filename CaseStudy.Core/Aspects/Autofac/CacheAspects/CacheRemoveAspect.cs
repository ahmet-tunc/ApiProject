using CaseStudy.Core.CrossCuttingConcerns.Caching;
using CaseStudy.Core.Utilities.IoC;
using System;
using Castle.DynamicProxy;
using CaseStudy.Core.Utilities.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStudy.Core.Aspects.Autofac.CacheAspects
{
    [Serializable]
    public class CacheRemoveAspect: MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
