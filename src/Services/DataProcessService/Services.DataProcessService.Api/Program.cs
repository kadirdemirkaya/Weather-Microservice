using Services.DataProcessService;
using Services.DataProcessService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
                .DataProcessApiServiceRegistration()
                .DataProcessServiceRegistration(builder.Configuration);

builder.DataProcessServiceBuilderRegistration(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection()
    .UseRouting();

app.DataProcessApiApplicationRegistration()
   .DataProcessServiceApplicationRegistration(builder.Services.BuildServiceProvider());

app.Run();
