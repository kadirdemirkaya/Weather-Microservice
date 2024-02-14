using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Services.Grpc;

namespace Services.DataProcessService.Registrations
{
    public static class GrpcRegistration
    {
        public static IServiceCollection GrpcServiceRegistration(this IServiceCollection services)
        {
            services.AddGrpc();

            return services;
        }

        public static WebApplication GrpcApplicationRegistration(this WebApplication app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<WeatherService>();

                endpoints.MapGet("/Protos/weather.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Protos/weather.proto"));
                });
            });

            return app;
        }
    }
}
