using Services.UserInfoService;
using Services.UserInfoService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
                .UserInfoApiServiceRegistration(builder.Configuration)
                .UserInfoServiceRegistration(builder.Configuration);
builder.UserInfoServiceBuilderRegistration(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection()
   .UseRouting();

app.UserInfoApiServiceApplicationRegistration(builder.Configuration)
   .UserInfoServiceApplicationRegistration(builder.Configuration);

app.Run();
