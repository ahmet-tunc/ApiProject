using Autofac;
using Autofac.Extras.DynamicProxy;
using CaseStudy.Business.Abstract;
using CaseStudy.Business.Concrete;
using CaseStudy.Core.Utilities.Interceptors;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.DataAccess.Concrete.Dapper;
using CaseStudy.DataAccess.Concrete.EntityFramework;
using Castle.DynamicProxy;
using RabbitMQ.Client;

namespace CaseStudy.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RestDocumentManager>().As<IRestDocumentService>();
            builder.RegisterType<EfDocumentDal>().As<IDocumentDal>();

            builder.RegisterType<LogManager>().As<ILogService>();
            builder.RegisterType<DpLogDal>().As<ILogDal>();

            builder.RegisterType<SoapDocumentManager>().As<ISoapDocumentService>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>();


            //Aspect tetikleyici
            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); //Mevcut assemblye ulaşıldı
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //Assemblyde ki bütün tipleri kayıt et
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
