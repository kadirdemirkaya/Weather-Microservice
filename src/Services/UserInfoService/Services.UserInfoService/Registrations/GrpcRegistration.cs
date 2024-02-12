using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Services.Grpc;

namespace Services.UserInfoService.Registrations
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
                endpoints.MapGrpcService<UserService>();

                endpoints.MapGet("/Protos/user.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Protos/user.proto"));
                });
            });

            return app;
        }
    }
}
