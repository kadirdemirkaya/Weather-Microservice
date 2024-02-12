namespace Services.UserInfoService.Api.Registrations
{
    public static class ConsulRegistration
    {
        public static IServiceCollection ConsulServiceRegistration(this IServiceCollection services)
        {
            //var consulOpt = services.GetOptions<ConsulOptions>(nameof(ConsulOptions));

            //services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            //{
            //    consulConfig.Address = new Uri(consulOpt.Address);
            //}));

            return services;
        }

        public static IApplicationBuilder ConsulApplicationBuilderRegsitration(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            //var consulOpt = configuration.GetOptions<ConsulOptions>(nameof(ConsulOptions));

            //var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            //var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            //var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            //Log.Information("Registering with Consul");
            //consulClient.Agent.ServiceDeregister(consulOpt.Id).Wait();
            //consulClient.Agent.ServiceRegister(GetConfigs.GetAgentServiceRegistration()).Wait();

            //lifetime.ApplicationStopped.Register(() =>
            //{
            //    Log.Information("Deregistering from Consul");
            //    consulClient.Agent.ServiceDeregister(GetConfigs.GetAgentServiceRegistration().ID);
            //});


            return app;
        }
    }
}
