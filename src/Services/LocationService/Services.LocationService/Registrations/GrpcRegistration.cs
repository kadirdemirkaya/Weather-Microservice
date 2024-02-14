using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Services.LocationService.Registrations
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
                endpoints.MapGrpcService<Services.Grpc.LocationService>();

                endpoints.MapGet("/Protos/location.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Protos/location.proto"));
                });
            });

            return app;
        }
    }
}
