using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using plantStatus.api.Entities;
using Swashbuckle.AspNetCore.Swagger;
using NLog;
using plantStatus.api.Services;

namespace plantStatus.api {
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment environment)
        {
            //connectionString.json is of course in .gitignore
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("connectionString.json", optional:false, reloadOnChange:true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Title = "PlantStatusAPI", Version = "v1"
                });
            });

            var connectionString = Configuration["connectionString"];
            services.AddDbContext<SensorInfoContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ISensorInfoRepository, SensorInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory, 
            SensorInfoContext sensorInfoContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            sensorInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            app.UseSwagger();

            app.UseMvc();
        }
    }
}
