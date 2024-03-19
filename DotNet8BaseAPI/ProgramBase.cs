using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Runtime.CompilerServices;

namespace DotNet8BaseAPI
{
    public static class ProgramBase
    {
        public static WebApplicationBuilder RegisterBaseServices(this WebApplicationBuilder builder, string[] args, string caching)
        {
            var env = builder.Environment;

            //Add configuration files
            var config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            builder.Configuration.AddConfiguration(config);

            //Add serilog
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);            

            if(caching.Equals("INMEMORY",StringComparison.InvariantCultureIgnoreCase))
            {
                builder.Services.AddMemoryCache();
            }

            return builder;
        }

        public static WebApplication ConfigureBaseHttpPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
