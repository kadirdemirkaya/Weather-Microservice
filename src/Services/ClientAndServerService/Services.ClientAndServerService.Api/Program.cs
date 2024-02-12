using Services.ClientAndServerService;
using Services.ClientAndServerService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.ClientAndServerApiServiceRegistration(builder.Configuration)
                .ClientAndServerServiceRegistration();

builder.ClientAndServerBuilderRegistration(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection()
    .UseRouting();

app.ClientAndServerApiApplicationRegistration(builder.Configuration)
   .ClientAndServerApplicationRegistration();

app.Run();
