using Serilog;
using Services.LocationService;
using Services.LocationService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
                .LocationApiServiceApiService(builder.Configuration)
                .LocationServiceRegistration(builder.Configuration);

Console.WriteLine("ASPNETCORE MONGODB URL IN PRODUCTION  -=> " + builder.Configuration["DatabaseOptions:ConnectionUrl"]);
Log.Warning("ASPNETCORE MONGODB URL IN PRODUCTION  -=> " + builder.Configuration["DatabaseOptions:ConnectionUrl"]);

builder.LocationServiceBuilderRegistration(builder.Configuration);


var app = builder.Build();

app.UseHttpsRedirection()
   .UseRouting();

app.LocationApiApplicationService(builder.Configuration, app.Services.GetRequiredService<IHostApplicationLifetime>())
   .LocationServiceApplicationRegistration(builder.Configuration);

app.Run();