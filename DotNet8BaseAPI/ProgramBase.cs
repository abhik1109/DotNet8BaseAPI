using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace DotNet8BaseAPI
{
    public static class ProgramBase
    {
        public static WebApplicationBuilder RegisterBaseServices(this WebApplicationBuilder builder)
        {

            return builder;
        }

        public static WebApplication ConfigureBaseHttpPipeline(this WebApplication app)
        {

            return app;
        }
    }
}
