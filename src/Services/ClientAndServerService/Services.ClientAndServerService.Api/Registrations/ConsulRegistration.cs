using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using Consul;
using Serilog;
using Services.ClientAndServerService.Constants;

namespace Services.ClientAndServerService.Api.Registrations
{
    public static class ConsulRegistration
    {
        public static IServiceCollection ConsulServiceRegistration(this IServiceCollection services)
        {
            var consulOpt = services.GetOptions<ConsulOptions>(nameof(ConsulOptions));

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(consulOpt.Address);
            }));

            return services;
        }

        public static IApplicationBuilder ConsulApplicationBuilderRegsitration(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            var consulOpt = configuration.GetOptions<ConsulOptions>(nameof(ConsulOptions));

            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            var agentServiceRegist = GetAgentServiceRegistration();
            var agentServiceRegsitId = agentServiceRegist.ID;

            Log.Information("Registering with Consul");
            consulClient.Agent.ServiceDeregister(agentServiceRegsitId).Wait();
            consulClient.Agent.ServiceRegister(agentServiceRegist).Wait();

            lifetime.ApplicationStopped.Register(() =>
            {
                Log.Information("Deregistering from Consul");
                consulClient.Agent.ServiceDeregister(agentServiceRegsitId);
            });

            return app;
        }

        private static AgentServiceRegistration GetAgentServiceRegistration()
            => new()
            {
                ID = Constant.ClientAndServerService.ID,
                Name = Constant.ClientAndServerService.Name,
                Port = int.Parse(Constant.ClientAndServerService.Port),
                Address = Constant.ClientAndServerService.Host,
                Tags = new[] { Constant.ClientAndServerService.Name, Constant.ClientAndServerService.Tag },
            };
    }
}
