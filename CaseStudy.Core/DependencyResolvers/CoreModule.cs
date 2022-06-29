using CaseStudy.Core.CrossCuttingConcerns.Caching;
using CaseStudy.Core.CrossCuttingConcerns.Caching.Microsoft;
using CaseStudy.Core.Helpers;
using CaseStudy.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStudy.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
