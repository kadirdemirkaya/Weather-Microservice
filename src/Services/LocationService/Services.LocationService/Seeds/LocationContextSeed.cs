using BuildingBlock.Base.Abstractions;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using Serilog;
using Services.LocationService.Aggregate;
using Services.LocationService.Aggregate.ValueObjects;

namespace Services.LocationService.Seeds
{
    public class LocationContextSeed
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationContextSeed(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SeedAsync()
        {
            var policy = CreatePolicy("MongoDb");

            await policy.ExecuteAsync(async () =>
            {
                try
                {
                    if (!await _unitOfWork.GetReadRepository<City, CityId>().AnyAsync())
                    {
                        ApplySeedCityDatas();
                    }
                    Log.Information("Seed Work is Succesfully");
                }
                catch (Exception ex)
                {
                    Log.Error("Seed Process Not Succesfully : " + ex.Message);
                    throw new Exception("Seed Process Not Succesfully : " + ex.Message);
                }

            });
        }

        private async void ApplySeedCityDatas()
        {
            List<City> cities = GetCityValues();
            foreach (var city in cities)
            {
                await _unitOfWork.GetWriteRepository<City, CityId>().CreateAsync(city);
            }
        }

        private List<City> GetCityValues()
        {
            List<City> cities = new();
            cities.Add(City.Create("Istanbul", "Turkiye", 41.1634, 28.7664));
            cities.Add(City.Create("Ankara", "Turkiye", 39.92077, 32.85411));
            cities.Add(City.Create("Izmir", "Turkiye", 38.41885, 27.12872));
            cities.Add(City.Create("Bursa", "Turkiye", 40.266864, 29.063448));
            cities.Add(City.Create("Adana", "Turkiye", 37, 35.321333));
            cities.Add(City.Create("Antalya", "Turkiye", 36.88414, 30.70563));
            cities.Add(City.Create("Mersin", "Turkiye", 36.8, 34.633333));
            cities.Add(City.Create("Gaziantep", "Turkiye", 37.06622, 37.38332));
            cities.Add(City.Create("Konya", "Turkiye", 37.866667, 32.483333));
            cities.Add(City.Create("Kayseri", "Turkiye", 38.73122, 35.478729));
            cities.Add(City.Create("Mardin", "Turkiye", 37.321163, 40.724477));
            cities.Add(City.Create("Trabzon", "Turkiye", 41.00145, 39.7178));
            cities.Add(City.Create("Sakarya", "Turkiye", 40.693997, 30.435763));

            return cities;
        }

        private AsyncRetryPolicy CreatePolicy(string prefix, int retries = 3)
        {
            return Policy.Handle<MongoException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        Log.Warning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }

}
