using Services.DataCaptureService;
using Services.DataCaptureService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.DataCaptureApiServiceRegistration()
                .DataCaptureServiceRegistration(builder.Configuration);

builder.DataCaptureBuilderRegistration(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection()
   .UseRouting();

app.DataCaptureApiServiceRegistration()
   .DataCaptureApplicationRegistration(builder.Services.BuildServiceProvider());

app.Run();
