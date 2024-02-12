using Microsoft.OpenApi.Models;
using Services.ClientAndServerService.Constants;

namespace Services.ClientAndServerService.Api.Registrations
{
    public static class SwaggerRegistration
    {
        public static IServiceCollection SwaggerServiceRegistration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = Constant.Application.Name,
                    Version = Constant.Application.Version,
                    Description = Constant.Application.Description,
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static WebApplication SwaggerApplicationRegistration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Constant.Application.Name} API {Constant.Application.Version}");
                });
            }

            return app;
        }
    }
}
