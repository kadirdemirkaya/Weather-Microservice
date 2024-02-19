using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Concretes;
using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using BuildingBlock.Redis;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Abstractions;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.Entities;
using Services.DataProcessService.Aggregate.Air.ValueObjects;
using Services.DataProcessService.Aggregate.Current.Entities;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using Services.DataProcessService.Aggregate.ValueObjects;
using Services.DataProcessService.Services;
using Services.DataProcessService.Services.Background;

namespace Services.DataProcessService.Registrations
{
    public static class Service
    {
        public static IServiceCollection ServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IAirPollutionService, AirPollutionService>();

            services.AddScoped<ICurrentWeatherService, CurrentWeatherService>();

            services.AddScoped<IDailyWeatherService, DailyWeatherService>();

            services.AddHostedService<LogCleanupService>();

            services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

            var option = services.GetOptions<InMemoryOptions>(nameof(InMemoryOptions));

            services.AddScoped<IRedisService<CurrentWeather, CurrentWeatherId>>(sp =>
            {
                return new RedisService<CurrentWeather, CurrentWeatherId>(option, sp);
            });

            services.AddScoped<IRedisService<CWeather, Aggregate.Current.ValueObjects.WeatherId>>(sp =>
            {
                return new RedisService<CWeather, Aggregate.Current.ValueObjects.WeatherId>(option, sp);
            });

            services.AddScoped<IRedisService<DailyWeather, DailyWeatherId>>(sp =>
            {
                return new RedisService<DailyWeather, DailyWeatherId>(option, sp);
            });

            services.AddScoped<IRedisService<DWeather, Aggregate.Daily.ValueObjects.WeatherId>>(sp =>
            {
                return new RedisService<DWeather, Aggregate.Daily.ValueObjects.WeatherId>(option, sp);
            });

            services.AddScoped<IRedisService<DList, DListId>>(sp =>
            {
                return new RedisService<DList, DListId>(option, sp);
            });

            services.AddScoped<IRedisService<AirPollutionWeather, AirPollutionWeatherId>>(sp =>
            {
                return new RedisService<AirPollutionWeather, AirPollutionWeatherId>(option, sp);
            });

            services.AddScoped<IRedisService<AList, AListId>>(sp =>
            {
                return new RedisService<AList, AListId>(option, sp);
            });


            return services;
        }
    }
}
