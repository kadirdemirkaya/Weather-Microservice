using ApiGateway.Api;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer()
                .ApiGatewayServiceRegistration(builder.Configuration);

builder.ApiGatewayBuilderRegistration(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication();

app.ApiGatewayApplicationRegistration(builder.Configuration);

app.Run();
