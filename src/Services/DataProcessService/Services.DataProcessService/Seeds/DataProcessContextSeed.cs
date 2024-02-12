using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using Serilog;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.ValueObjects;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using Services.DataProcessService.Aggregate.ValueObjects;
using Services.DataProcessService.Data;
using System.Text.Json;

namespace Services.DataProcessService.Seeds
{
    public class DataProcessContextSeed
    {
        bool res = false;
        public async Task SeedAsync(WeatherDbContext context)
        {
            var policy = CreatePolicy(nameof(WeatherDbContext));

            await policy.ExecuteAsync(async () =>
            {
                try
                {
                    if (!context.CurrentWeathers.Any())
                    {
                        await context.CurrentWeathers.AddRangeAsync(GetSeedCurrentWeatherDatas());
                        res = await context.SaveChangesAsync() > 0;
                        Log.Information($"Seed Data CurrentWeathers Process :  {res}");
                    }
                    if (!context.DailyWeathers.Any())
                    {
                        await context.DailyWeathers.AddRangeAsync(GetSeedDailyWeatherDatas());
                        res = await context.SaveChangesAsync() > 0;
                        Log.Information($"Seed Data Lists Process :  {res}");
                    }
                    if (!context.AirPollutionWeathers.Any())
                    {
                        await context.AirPollutionWeathers.AddRangeAsync(GetSeedAirPollutionWeatherDatas());
                        res = await context.SaveChangesAsync() > 0;
                        Log.Information($"Seed Data Lists Process :  {res}");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Seed Process Not Succesfully : " + ex.Message);
                    throw new Exception("Seed Process Not Succesfully : " + ex.Message);
                }
            });
        }

        private List<AirPollutionWeather> GetSeedAirPollutionWeatherDatas()
        {
            List<AirPollutionWeather> airPollutionWeathers = new();
            var exampleDatas = GetExampleAirPollutionDatas();
            foreach (var exData in exampleDatas)
            {
                Models.Air.WeatherData weatherData = JsonSerializer.Deserialize<Models.Air.WeatherData>(exData);

                var airPollutionWeather = AirPollutionWeather.Create(Aggregate.Air.ValueObjects.Coord.Create(weatherData.coord.lat, weatherData.coord.lon));

                foreach (var lst in weatherData.list)
                {
                    airPollutionWeather.AddList(AListId.CreateUnique(), lst.dt, Aggregate.Air.ValueObjects.Main.Create(lst.main.aqi), Aggregate.Air.ValueObjects.Component.Create(lst.components.co, lst.components.no, lst.components.no2, lst.components.o3, lst.components.so2, lst.components.pm2, lst.components.pm10, lst.components.nh3), airPollutionWeather.Id);
                }
                airPollutionWeathers.Add(airPollutionWeather);
            }
            return airPollutionWeathers;
        }

        private List<string> GetExampleAirPollutionDatas()
        {
            List<string> airPollutions = new();
            airPollutions.Add("{\r\n  \"coord\":[\r\n    41.0082,\r\n    28.9784\r\n  ],\r\n  \"list\":[\r\n    {\r\n      \"dt\":1605182400,\r\n      \"main\":{\r\n        \"aqi\":2\r\n      },\r\n      \"components\":{\r\n        \"co\":300.94053649902344,\r\n        \"no\":0.02877197064459324,\r\n        \"no2\":0.8711350917816162,\r\n        \"o3\":58.66455078125,\r\n        \"so2\":0.7407499313354492,\r\n        \"pm2_5\":0.8,\r\n        \"pm10\":0.840438711643219,\r\n        \"nh3\":0.22369127571582794\r\n      }\r\n    }\r\n  ]\r\n}");
            airPollutions.Add("{\r\n  \"coord\":[\r\n    39.9334,\r\n    32.8597\r\n  ],\r\n  \"list\":[\r\n    {\r\n      \"dt\":1605182400,\r\n      \"main\":{\r\n        \"aqi\":3\r\n      },\r\n      \"components\":{\r\n        \"co\":250.94053649902344,\r\n        \"no\":0.03877197064459324,\r\n        \"no2\":0.9711350917816162,\r\n        \"o3\":48.66455078125,\r\n        \"so2\":0.8407499313354492,\r\n        \"pm2_5\":1.0,\r\n        \"pm10\":1.040438711643219,\r\n        \"nh3\":0.32369127571582794\r\n      }\r\n    }\r\n  ]\r\n}");
            airPollutions.Add("{\r\n  \"coord\":[\r\n    38.4192,\r\n    27.1287\r\n  ],\r\n  \"list\":[\r\n    {\r\n      \"dt\":1605182400,\r\n      \"main\":{\r\n        \"aqi\":2\r\n      },\r\n      \"components\":{\r\n        \"co\":280.94053649902344,\r\n        \"no\":0.03177197064459324,\r\n        \"no2\":0.8511350917816162,\r\n        \"o3\":63.66455078125,\r\n        \"so2\":0.7607499313354492,\r\n        \"pm2_5\":0.7,\r\n        \"pm10\":0.740438711643219,\r\n        \"nh3\":0.21369127571582794\r\n      }\r\n    }\r\n  ]\r\n}");
            airPollutions.Add("{\r\n  \"coord\":[\r\n    36.8969,\r\n    30.7133\r\n  ],\r\n  \"list\":[\r\n    {\r\n      \"dt\":1605182400,\r\n      \"main\":{\r\n        \"aqi\":3\r\n      },\r\n      \"components\":{\r\n        \"co\":260.94053649902344,\r\n        \"no\":0.04177197064459324,\r\n        \"no2\":0.9511350917816162,\r\n        \"o3\":53.66455078125,\r\n        \"so2\":0.8207499313354492,\r\n        \"pm2_5\":0.9,\r\n        \"pm10\":0.940438711643219,\r\n        \"nh3\":0.28369127571582794\r\n      }\r\n    }\r\n  ]\r\n}");

            return airPollutions;
        }

        private List<DailyWeather> GetSeedDailyWeatherDatas()
        {
            List<DailyWeather> dailyWeathers = new();
            var listExp = GetExampleDailyWeatherDatas();

            foreach (var list in listExp)
            {
                int i = 0;
                List<double> rains = new();
                Models.Daily.WeatherData weatherData = JsonSerializer.Deserialize<Models.Daily.WeatherData>(list);

                JObject jsonObject = JObject.Parse(list);
                foreach (var item in jsonObject["list"])
                {
                    JToken rainToken = item["rain"]?["3h"];
                    double rainValue = rainToken?.Value<double>() ?? 0;
                    rains.Add(rainValue);
                }
                foreach (var lst in weatherData.list)
                {
                    if (rains[i] != 0)
                        lst.rain._3h = rains[i];
                    else
                        lst.rain = new();
                    i++;
                }

                var dailyWeather = DailyWeather.Create(DailyWeatherId.CreateUnique(), weatherData.cod, weatherData.message, weatherData.cnt, City.Create(weatherData.city.id, weatherData.city.name, weatherData.city.coord.lon, weatherData.city.coord.lat, weatherData.city.country, weatherData.city.population, weatherData.city.timezone, weatherData.city.sunrise, weatherData.city.sunset));

                foreach (var lst in weatherData.list)
                {
                    var main = Aggregate.Daily.ValueObjects.Main.Create(lst.main.temp, lst.main.feels_like, lst.main.temp_min, lst.main.temp_max, lst.main.pressure, lst.main.humidity, lst.main.sea_level, lst.main.grnd_level, lst.main.temp_kf);
                    var cloud = Aggregate.Daily.ValueObjects.Cloud.Create(lst.clouds.all);
                    var wind = Aggregate.Daily.ValueObjects.Wind.Create(lst.wind.speed, lst.wind.deg, lst.wind.gust);
                    var rain = Aggregate.Daily.ValueObjects.Rain.Create(lst.rain._3h);
                    var sys = Aggregate.Daily.ValueObjects.Sys.Create(lst.sys.pod);

                    var ListID = DListId.CreateUnique();
                    DWeather dweather = new();
                    foreach (var weat in lst.weather)
                    {
                        dweather = DWeather.Create(weat.id, weat.main, weat.description, weat.icon, ListID);
                    }

                    dailyWeather.AddListWithDWeather(ListID, lst.dt, main, cloud, wind, lst.visibility, lst.pop, rain, sys, lst.dt_txt, dailyWeather.Id, dweather);

                    dailyWeathers.Add(dailyWeather);
                }
            }

            return dailyWeathers;
        }

        private List<string> GetExampleDailyWeatherDatas()
        {
            List<string> lists = new();
            #region datas
            lists.Add("{\r\n  \"cod\": \"200\",\r\n  \"message\": 0,\r\n  \"cnt\": 5,\r\n  \"list\": [\r\n    {\r\n      \"dt\": 1661871600,\r\n      \"main\": {\r\n        \"temp\": 296.76,\r\n        \"feels_like\": 296.98,\r\n        \"temp_min\": 296.76,\r\n        \"temp_max\": 297.87,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 69,\r\n        \"temp_kf\": -1.11\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10d\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 100\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 0.62,\r\n        \"deg\": 349,\r\n        \"gust\": 1.18\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.32,\r\n      \"rain\": {\r\n        \"3h\": 0.26\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"d\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 15:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661882400,\r\n      \"main\": {\r\n        \"temp\": 295.45,\r\n        \"feels_like\": 295.59,\r\n        \"temp_min\": 292.84,\r\n        \"temp_max\": 295.45,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 71,\r\n        \"temp_kf\": 2.61\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 96\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 1.97,\r\n        \"deg\": 157,\r\n        \"gust\": 3.39\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.33,\r\n      \"rain\": {\r\n        \"3h\": 0.57\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 18:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661893200,\r\n      \"main\": {\r\n        \"temp\": 292.46,\r\n        \"feels_like\": 292.54,\r\n        \"temp_min\": 290.31,\r\n        \"temp_max\": 292.46,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 80,\r\n        \"temp_kf\": 2.15\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 68\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 2.66,\r\n        \"deg\": 210,\r\n        \"gust\": 3.58\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.7,\r\n      \"rain\": {\r\n        \"3h\": 0.49\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 21:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661904000,\r\n      \"main\": {\r\n        \"temp\": 290.11,\r\n        \"feels_like\": 290.18,\r\n        \"temp_min\": 287.41,\r\n        \"temp_max\": 290.11,\r\n        \"pressure\": 1016,\r\n        \"sea_level\": 1016,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 89,\r\n        \"temp_kf\": 2.7\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 91\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.1,\r\n        \"deg\": 215,\r\n        \"gust\": 3.65\r\n      },\r\n      \"visibility\": 9964,\r\n      \"pop\": 0.95,\r\n      \"rain\": {\r\n        \"3h\": 2.08\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 00:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661914800,\r\n      \"main\": {\r\n        \"temp\": 288.42,\r\n        \"feels_like\": 288.47,\r\n        \"temp_min\": 286.52,\r\n        \"temp_max\": 288.42,\r\n        \"pressure\": 1017,\r\n        \"sea_level\": 1017,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 94,\r\n        \"temp_kf\": 1.9\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 98\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.12,\r\n        \"deg\": 222,\r\n        \"gust\": 3.62\r\n      },\r\n      \"visibility\": 9656,\r\n      \"pop\": 0.98,\r\n      \"rain\": {\r\n        \"3h\": 1.68\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-31 03:00:00\"\r\n    }\r\n  ],\r\n  \"city\": {\r\n    \"id\": 745044,\r\n    \"name\": \"Istanbul\",\r\n    \"coord\": {\r\n      \"lat\": 41.0082,\r\n      \"lon\": 28.9784\r\n    },\r\n    \"country\": \"TR\",\r\n    \"population\": 15460000,\r\n    \"timezone\": 10800,\r\n    \"sunrise\": 1661834187,\r\n    \"sunset\": 1661882248\r\n  }\r\n}");

            lists.Add("{\r\n  \"cod\": \"200\",\r\n  \"message\": 0,\r\n  \"cnt\": 5,\r\n  \"list\": [\r\n    {\r\n      \"dt\": 1661871600,\r\n      \"main\": {\r\n        \"temp\": 296.76,\r\n        \"feels_like\": 296.98,\r\n        \"temp_min\": 296.76,\r\n        \"temp_max\": 297.87,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 69,\r\n        \"temp_kf\": -1.11\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10d\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 100\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 0.62,\r\n        \"deg\": 349,\r\n        \"gust\": 1.18\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.32,\r\n      \"rain\": {\r\n        \"3h\": 0.26\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"d\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 15:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661882400,\r\n      \"main\": {\r\n        \"temp\": 295.45,\r\n        \"feels_like\": 295.59,\r\n        \"temp_min\": 292.84,\r\n        \"temp_max\": 295.45,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 71,\r\n        \"temp_kf\": 2.61\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 96\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 1.97,\r\n        \"deg\": 157,\r\n        \"gust\": 3.39\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.33,\r\n      \"rain\": {\r\n        \"3h\": 0.57\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 18:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661893200,\r\n      \"main\": {\r\n        \"temp\": 292.46,\r\n        \"feels_like\": 292.54,\r\n        \"temp_min\": 290.31,\r\n        \"temp_max\": 292.46,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 80,\r\n        \"temp_kf\": 2.15\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 68\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 2.66,\r\n        \"deg\": 210,\r\n        \"gust\": 3.58\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.7,\r\n      \"rain\": {\r\n        \"3h\": 0.49\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 21:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661904000,\r\n      \"main\": {\r\n        \"temp\": 290.11,\r\n        \"feels_like\": 290.18,\r\n        \"temp_min\": 287.41,\r\n        \"temp_max\": 290.11,\r\n        \"pressure\": 1016,\r\n        \"sea_level\": 1016,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 89,\r\n        \"temp_kf\": 2.7\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 91\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.1,\r\n        \"deg\": 215,\r\n        \"gust\": 3.65\r\n      },\r\n      \"visibility\": 9964,\r\n      \"pop\": 0.95,\r\n      \"rain\": {\r\n        \"3h\": 2.08\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 00:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661914800,\r\n      \"main\": {\r\n        \"temp\": 288.42,\r\n        \"feels_like\": 288.47,\r\n        \"temp_min\": 286.52,\r\n        \"temp_max\": 288.42,\r\n        \"pressure\": 1017,\r\n        \"sea_level\": 1017,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 94,\r\n        \"temp_kf\": 1.9\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 98\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.12,\r\n        \"deg\": 222,\r\n        \"gust\": 3.62\r\n      },\r\n      \"visibility\": 9656,\r\n      \"pop\": 0.98,\r\n      \"rain\": {\r\n        \"3h\": 1.68\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-31 03:00:00\"\r\n    }\r\n  ],\r\n  \"city\": {\r\n    \"id\": 745044,\r\n    \"name\": \"Ankara\",\r\n    \"coord\": {\r\n      \"lat\": 39.9334,\r\n      \"lon\": 32.8597\r\n    },\r\n    \"country\": \"TR\",\r\n    \"population\": 15460000,\r\n    \"timezone\": 10800,\r\n    \"sunrise\": 1661834187,\r\n    \"sunset\": 1661882248\r\n  }\r\n}");

            lists.Add("{\r\n  \"cod\": \"200\",\r\n  \"message\": 0,\r\n  \"cnt\": 5,\r\n  \"list\": [\r\n    {\r\n      \"dt\": 1661871600,\r\n      \"main\": {\r\n        \"temp\": 296.76,\r\n        \"feels_like\": 296.98,\r\n        \"temp_min\": 296.76,\r\n        \"temp_max\": 297.87,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 69,\r\n        \"temp_kf\": -1.11\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10d\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 100\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 0.62,\r\n        \"deg\": 349,\r\n        \"gust\": 1.18\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.32,\r\n      \"rain\": {\r\n        \"3h\": 0.26\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"d\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 15:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661882400,\r\n      \"main\": {\r\n        \"temp\": 295.45,\r\n        \"feels_like\": 295.59,\r\n        \"temp_min\": 292.84,\r\n        \"temp_max\": 295.45,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 71,\r\n        \"temp_kf\": 2.61\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 96\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 1.97,\r\n        \"deg\": 157,\r\n        \"gust\": 3.39\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.33,\r\n      \"rain\": {\r\n        \"3h\": 0.57\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 18:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661893200,\r\n      \"main\": {\r\n        \"temp\": 292.46,\r\n        \"feels_like\": 292.54,\r\n        \"temp_min\": 290.31,\r\n        \"temp_max\": 292.46,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 80,\r\n        \"temp_kf\": 2.15\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 68\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 2.66,\r\n        \"deg\": 210,\r\n        \"gust\": 3.58\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.7,\r\n      \"rain\": {\r\n        \"3h\": 0.49\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 21:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661904000,\r\n      \"main\": {\r\n        \"temp\": 290.11,\r\n        \"feels_like\": 290.18,\r\n        \"temp_min\": 287.41,\r\n        \"temp_max\": 290.11,\r\n        \"pressure\": 1016,\r\n        \"sea_level\": 1016,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 89,\r\n        \"temp_kf\": 2.7\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 91\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.1,\r\n        \"deg\": 215,\r\n        \"gust\": 3.65\r\n      },\r\n      \"visibility\": 9964,\r\n      \"pop\": 0.95,\r\n      \"rain\": {\r\n        \"3h\": 2.08\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 00:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661914800,\r\n      \"main\": {\r\n        \"temp\": 288.42,\r\n        \"feels_like\": 288.47,\r\n        \"temp_min\": 286.52,\r\n        \"temp_max\": 288.42,\r\n        \"pressure\": 1017,\r\n        \"sea_level\": 1017,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 94,\r\n        \"temp_kf\": 1.9\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 98\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.12,\r\n        \"deg\": 222,\r\n        \"gust\": 3.62\r\n      },\r\n      \"visibility\": 9656,\r\n      \"pop\": 0.98,\r\n      \"rain\": {\r\n        \"3h\": 1.68\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-31 03:00:00\"\r\n    }\r\n  ],\r\n  \"city\": {\r\n    \"id\": 745044,\r\n    \"name\": \"Izmir\",\r\n    \"coord\": {\r\n      \"lat\": 38.4192,\r\n      \"lon\": 27.1287\r\n    },\r\n    \"country\": \"TR\",\r\n    \"population\": 15460000,\r\n    \"timezone\": 10800,\r\n    \"sunrise\": 1661834187,\r\n    \"sunset\": 1661882248\r\n  }\r\n}");

            lists.Add("{\r\n  \"cod\": \"200\",\r\n  \"message\": 0,\r\n  \"cnt\": 5,\r\n  \"list\": [\r\n    {\r\n      \"dt\": 1661871600,\r\n      \"main\": {\r\n        \"temp\": 296.76,\r\n        \"feels_like\": 296.98,\r\n        \"temp_min\": 296.76,\r\n        \"temp_max\": 297.87,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 69,\r\n        \"temp_kf\": -1.11\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10d\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 100\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 0.62,\r\n        \"deg\": 349,\r\n        \"gust\": 1.18\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.32,\r\n      \"rain\": {\r\n        \"3h\": 0.26\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"d\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 15:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661882400,\r\n      \"main\": {\r\n        \"temp\": 295.45,\r\n        \"feels_like\": 295.59,\r\n        \"temp_min\": 292.84,\r\n        \"temp_max\": 295.45,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 71,\r\n        \"temp_kf\": 2.61\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 96\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 1.97,\r\n        \"deg\": 157,\r\n        \"gust\": 3.39\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.33,\r\n      \"rain\": {\r\n        \"3h\": 0.57\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 18:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661893200,\r\n      \"main\": {\r\n        \"temp\": 292.46,\r\n        \"feels_like\": 292.54,\r\n        \"temp_min\": 290.31,\r\n        \"temp_max\": 292.46,\r\n        \"pressure\": 1015,\r\n        \"sea_level\": 1015,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 80,\r\n        \"temp_kf\": 2.15\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 68\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 2.66,\r\n        \"deg\": 210,\r\n        \"gust\": 3.58\r\n      },\r\n      \"visibility\": 10000,\r\n      \"pop\": 0.7,\r\n      \"rain\": {\r\n        \"3h\": 0.49\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 21:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661904000,\r\n      \"main\": {\r\n        \"temp\": 290.11,\r\n        \"feels_like\": 290.18,\r\n        \"temp_min\": 287.41,\r\n        \"temp_max\": 290.11,\r\n        \"pressure\": 1016,\r\n        \"sea_level\": 1016,\r\n        \"grnd_level\": 931,\r\n        \"humidity\": 89,\r\n        \"temp_kf\": 2.7\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 91\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.1,\r\n        \"deg\": 215,\r\n        \"gust\": 3.65\r\n      },\r\n      \"visibility\": 9964,\r\n      \"pop\": 0.95,\r\n      \"rain\": {\r\n        \"3h\": 2.08\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-30 00:00:00\"\r\n    },\r\n    {\r\n      \"dt\": 1661914800,\r\n      \"main\": {\r\n        \"temp\": 288.42,\r\n        \"feels_like\": 288.47,\r\n        \"temp_min\": 286.52,\r\n        \"temp_max\": 288.42,\r\n        \"pressure\": 1017,\r\n        \"sea_level\": 1017,\r\n        \"grnd_level\": 933,\r\n        \"humidity\": 94,\r\n        \"temp_kf\": 1.9\r\n      },\r\n      \"weather\": [\r\n        {\r\n          \"id\": 500,\r\n          \"main\": \"Rain\",\r\n          \"description\": \"light rain\",\r\n          \"icon\": \"10n\"\r\n        }\r\n      ],\r\n      \"clouds\": {\r\n        \"all\": 98\r\n      },\r\n      \"wind\": {\r\n        \"speed\": 3.12,\r\n        \"deg\": 222,\r\n        \"gust\": 3.62\r\n      },\r\n      \"visibility\": 9656,\r\n      \"pop\": 0.98,\r\n      \"rain\": {\r\n        \"3h\": 1.68\r\n      },\r\n      \"sys\": {\r\n        \"pod\": \"n\"\r\n      },\r\n      \"dt_txt\": \"2022-08-31 03:00:00\"\r\n    }\r\n  ],\r\n  \"city\": {\r\n    \"id\": 323776,\r\n    \"name\": \"Antalya\",\r\n    \"coord\": {\r\n      \"lat\": 36.8969,\r\n      \"lon\": 30.7133\r\n    },\r\n    \"country\": \"TR\",\r\n    \"population\": 1594941,\r\n    \"timezone\": 10800,\r\n    \"sunrise\": 1661834187,\r\n    \"sunset\": 1661882248\r\n  }\r\n}");
            #endregion
            return lists;
        }

        private List<CurrentWeather> GetSeedCurrentWeatherDatas()
        {
            List<CurrentWeather> currentWeathers = new();
            var cities = GetExampleCityDatas();

            foreach (var city in cities)
            {
                Models.WeatherData weatherData = JsonSerializer.Deserialize<Models.WeatherData>(city);

                JObject jsonObject = JObject.Parse(city);

                var item = jsonObject["rain"];
                weatherData.rain._1h = item["1h"]?.Value<double>() ?? 0;

                var cord = Aggregate.Current.ValueObjects.Coord.Create(weatherData.coord.lon, weatherData.coord.lat);
                var main = Aggregate.Current.ValueObjects.Main.Create(weatherData.main.temp, weatherData.main.feels_like, weatherData.main.temp_min, weatherData.main.temp_max, weatherData.main.pressure, weatherData.main.humidity, weatherData.main.sea_level, weatherData.main.grnd_level);
                var rain = Aggregate.Current.ValueObjects.Rain.Create(weatherData.rain._1h);
                var wind = Aggregate.Current.ValueObjects.Wind.Create(weatherData.wind.speed, weatherData.wind.deg, weatherData.wind.gust);
                var cloud = Aggregate.Current.ValueObjects.Cloud.Create(weatherData.clouds.all);
                var sys = Aggregate.Current.ValueObjects.Sys.Create(weatherData.sys.type, weatherData.sys.id, weatherData.sys.country, weatherData.sys.sunrise);

                CurrentWeather currentWeatherEnt = CurrentWeather.CreateCurrentWeather(CurrentWeatherId.CreateUnique(), cord, weatherData.@base, main, weatherData.visibility, wind, rain, cloud, weatherData.dt, sys, weatherData.timezone, weatherData.id, weatherData.name, weatherData.cod);

                foreach (var weather in weatherData.weather)
                {
                    currentWeatherEnt.AddWeather(Aggregate.Current.ValueObjects.WeatherId.CreateUnique(), weather.id, weather.main, weather.description, weather.icon, currentWeatherEnt.Id);
                }
                currentWeathers.Add(currentWeatherEnt);
            }
            return currentWeathers;
        }

        private List<string> GetExampleCityDatas()
        {
            #region EXAMPLES
            List<string> cities = new();
            cities.Add("{\r\n  \"coord\": {\r\n    \"lon\": 10.99,\r\n    \"lat\": 44.34\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 501,\r\n      \"main\": \"Rain\",\r\n      \"description\": \"moderate rain\",\r\n      \"icon\": \"10d\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 298.48,\r\n    \"feels_like\": 298.74,\r\n    \"temp_min\": 297.56,\r\n    \"temp_max\": 300.05,\r\n    \"pressure\": 1015,\r\n    \"humidity\": 64,\r\n    \"sea_level\": 1015,\r\n    \"grnd_level\": 933\r\n  },\r\n  \"visibility\": 10000,\r\n  \"wind\": {\r\n    \"speed\": 0.62,\r\n    \"deg\": 349,\r\n    \"gust\": 1.18\r\n  },\r\n  \"rain\": {\r\n    \"1h\": 3.16\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 100\r\n  },\r\n  \"dt\": 1661870592,\r\n  \"sys\": {\r\n    \"type\": 2,\r\n    \"id\": 2075663,\r\n    \"country\": \"IT\",\r\n    \"sunrise\": 1661834187,\r\n    \"sunset\": 1661882248\r\n  },\r\n  \"timezone\": 7200,\r\n  \"id\": 3163858,\r\n  \"name\": \"Zocca\",\r\n  \"cod\": 200\r\n}");

            cities.Add("{\r\n  \"coord\": {\r\n    \"lon\": 28.97,\r\n    \"lat\": 41.01\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 501,\r\n      \"main\": \"Rain\",\r\n      \"description\": \"moderate rain\",\r\n      \"icon\": \"10d\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 298.48,\r\n    \"feels_like\": 298.74,\r\n    \"temp_min\": 297.56,\r\n    \"temp_max\": 300.05,\r\n    \"pressure\": 1015,\r\n    \"humidity\": 64,\r\n    \"sea_level\": 1015,\r\n    \"grnd_level\": 933\r\n  },\r\n  \"visibility\": 10000,\r\n  \"wind\": {\r\n    \"speed\": 0.62,\r\n    \"deg\": 349,\r\n    \"gust\": 1.18\r\n  },\r\n  \"rain\": {\r\n    \"1h\": 3.16\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 100\r\n  },\r\n  \"dt\": 1661870592,\r\n  \"sys\": {\r\n    \"type\": 2,\r\n    \"id\": 2075663,\r\n    \"country\": \"TR\",\r\n    \"sunrise\": 1661834187,\r\n    \"sunset\": 1661882248\r\n  },\r\n  \"timezone\": 7200,\r\n  \"id\": 745044,\r\n  \"name\": \"Istanbul\",\r\n  \"cod\": 200\r\n}");

            cities.Add("{\r\n  \"coord\": {\r\n    \"lon\": 32.8597,\r\n    \"lat\": 39.9334\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 501,\r\n      \"main\": \"Rain\",\r\n      \"description\": \"moderate rain\",\r\n      \"icon\": \"10d\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 288.12,\r\n    \"feels_like\": 285.71,\r\n    \"temp_min\": 287.15,\r\n    \"temp_max\": 289.82,\r\n    \"pressure\": 1017,\r\n    \"humidity\": 47,\r\n    \"sea_level\": 1017,\r\n    \"grnd_level\": 932\r\n  },\r\n  \"visibility\": 10000,\r\n  \"wind\": {\r\n    \"speed\": 2.68,\r\n    \"deg\": 0\r\n  },\r\n  \"rain\": {\r\n    \"1h\": 3.16\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 100\r\n  },\r\n  \"dt\": 1661870592,\r\n  \"sys\": {\r\n    \"type\": 1,\r\n    \"id\": 6950,\r\n    \"country\": \"TR\",\r\n    \"sunrise\": 1661861185,\r\n    \"sunset\": 1661899974\r\n  },\r\n  \"timezone\": 10800,\r\n  \"id\": 323786,\r\n  \"name\": \"Ankara\",\r\n  \"cod\": 200\r\n}");

            cities.Add("{\r\n  \"coord\": {\r\n    \"lon\": 30.7133,\r\n    \"lat\": 36.8841\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 501,\r\n      \"main\": \"Rain\",\r\n      \"description\": \"moderate rain\",\r\n      \"icon\": \"10d\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 293.15,\r\n    \"feels_like\": 290.56,\r\n    \"temp_min\": 293.15,\r\n    \"temp_max\": 293.15,\r\n    \"pressure\": 1015,\r\n    \"humidity\": 77\r\n  },\r\n  \"visibility\": 8000,\r\n  \"wind\": {\r\n    \"speed\": 3.09,\r\n    \"deg\": 150\r\n  },\r\n  \"rain\": {\r\n    \"1h\": 2.4\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 40\r\n  },\r\n  \"dt\": 1661870592,\r\n  \"sys\": {\r\n    \"type\": 1,\r\n    \"id\": 7011,\r\n    \"country\": \"TR\",\r\n    \"sunrise\": 1661859326,\r\n    \"sunset\": 1661898108\r\n  },\r\n  \"timezone\": 10800,\r\n  \"id\": 323776,\r\n  \"name\": \"Antalya\",\r\n  \"cod\": 200\r\n}");

            cities.Add("{\r\n  \"coord\": {\r\n    \"lon\": 27.1428,\r\n    \"lat\": 38.4192\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 501,\r\n      \"main\": \"Rain\",\r\n      \"description\": \"moderate rain\",\r\n      \"icon\": \"10d\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 291.15,\r\n    \"feels_like\": 289.56,\r\n    \"temp_min\": 291.15,\r\n    \"temp_max\": 291.15,\r\n    \"pressure\": 1015,\r\n    \"humidity\": 70\r\n  },\r\n  \"visibility\": 8000,\r\n  \"wind\": {\r\n    \"speed\": 3.09,\r\n    \"deg\": 150\r\n  },\r\n  \"rain\": {\r\n    \"1h\": 2.4\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 40\r\n  },\r\n  \"dt\": 1661870592,\r\n  \"sys\": {\r\n    \"type\": 1,\r\n    \"id\": 7011,\r\n    \"country\": \"TR\",\r\n    \"sunrise\": 1661859326,\r\n    \"sunset\": 1661898108\r\n  },\r\n  \"timezone\": 10800,\r\n  \"id\": 323776,\r\n  \"name\": \"Izmir\",\r\n  \"cod\": 200\r\n}");

            cities.Add("{\r\n  \"coord\": {\r\n    \"lon\": 35.5018,\r\n    \"lat\": 41.2867\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 501,\r\n      \"main\": \"Rain\",\r\n      \"description\": \"moderate rain\",\r\n      \"icon\": \"10d\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 288.15,\r\n    \"feels_like\": 286.11,\r\n    \"temp_min\": 288.15,\r\n    \"temp_max\": 288.15,\r\n    \"pressure\": 1015,\r\n    \"humidity\": 80\r\n  },\r\n  \"visibility\": 8000,\r\n  \"wind\": {\r\n    \"speed\": 3.09,\r\n    \"deg\": 150\r\n  },\r\n  \"rain\": {\r\n    \"1h\": 2.4\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 40\r\n  },\r\n  \"dt\": 1661870592,\r\n  \"sys\": {\r\n    \"type\": 1,\r\n    \"id\": 7011,\r\n    \"country\": \"TR\",\r\n    \"sunrise\": 1661859326,\r\n    \"sunset\": 1661898108\r\n  },\r\n  \"timezone\": 10800,\r\n  \"id\": 323776,\r\n  \"name\": \"Samsun\",\r\n  \"cod\": 200\r\n}");

            return cities;
            #endregion
        }

        private AsyncRetryPolicy CreatePolicy(string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
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
