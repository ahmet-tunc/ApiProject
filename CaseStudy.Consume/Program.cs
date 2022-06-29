using CaseStudy.Consume.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;

namespace CaseStudy.Consume
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration Configuration = hostContext.Configuration;

                    services.AddSingleton<ClientService>();
                    services.AddSingleton(sp => new ConnectionFactory() { 
                        HostName = Configuration.GetConnectionString("RabbitMQ"), 
                        DispatchConsumersAsync = true 
                    });
                    services.AddHostedService<PdfCreatorWorker>();
                });
    }
}
