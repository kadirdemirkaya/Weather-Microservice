using Microsoft.AspNetCore.Builder;

namespace BuildingBlock.Logger
{
    public static class ServiceExtension
    {
        public static WebApplicationBuilder AddFileExtensionBuilder(this WebApplicationBuilder app, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            app.AddLogSeriLogFile(configuration);

            return app;
        }

        public static WebApplicationBuilder AddElasticExtensionBuilder(this WebApplicationBuilder app, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            app.AddLogSeriLog(configuration);

            return app;
        }

        public static WebApplicationBuilder AddConsoleExtensionBuilder(this WebApplicationBuilder app, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            app.AddLogSeriLogConsole(configuration);

            return app;
        }

        public static WebApplicationBuilder AddLogSeriLogSeqExtensionBuilder(this WebApplicationBuilder app, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            app.AddLogSeriLogSeq(configuration);

            return app;
        }
    }
}
