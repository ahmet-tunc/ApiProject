using CaseStudy.Business.Abstract;
using CaseStudy.Business.Concrete;
using CaseStudy.Core.DependencyResolvers;
using CaseStudy.Core.Extensions;
using CaseStudy.Core.Publisher.RabbitMQ;
using CaseStudy.Core.Utilities.IoC;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.DataAccess.Concrete;
using CaseStudy.DataAccess.Concrete.Dapper;
using CaseStudy.WebApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;
using System.Net.Http.Headers;

namespace CaseStudy.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CaseStudy.WebUI", Version = "v1" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            services.AddHttpClient("client", c => {
                //c.BaseAddress = new Uri("https://doc1servicestest.turkiyehayatemeklilik.com.tr/api/confirm/TestFunctions/GetDailyDocuments");
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<AppDbContext>();

            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseOracle(Configuration.GetConnectionString("Oracle")));

            services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, AuthenticationController>("BasicAuthentication", null);


            services.AddSingleton<ClientServices>();
            services.AddSingleton<ClientPublisher>();

            services.AddScoped<ILogDal,DpLogDal>();

            services.AddSingleton(uri => new ConnectionFactory()
            {
                Uri = new Uri(Configuration.GetConnectionString("RabbitMQ"))
            });

            services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaseStudy.WebUI v1"));
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
