using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.Entities;
using Services.DataProcessService.Aggregate.Current.ValueObjects;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.Entities;

namespace Services.DataProcessService.Constants
{
    public static class Constant
    {
        public static class Application
        {
            public const string Name = "DataProcessService";
            public const string Version = "v1";
            public const string Description = "Data Process Service api description";
        }

        public static class Tables
        {
            public const string CurrentWeathers = $"{nameof(CurrentWeather)}s";
            public const string DailyWeathers = $"{nameof(DailyWeather)}s";
            public const string AirPollutionWeathers = $"{nameof(AirPollutionWeather)}s";
            public const string CWeathers = $"CWeathers";
            public const string DWeathers = $"DWeathers";
            public const string DLists = $"{nameof(DList)}s";
            public const string ALists = $"{nameof(AList)}s";
            public const string Clouds = $"{nameof(Cloud)}s";
            public const string Coords = $"{nameof(Aggregate.Current.ValueObjects.Coord)}s";
            public const string Cities = $"Cities";
            public const string Main = $"Mains";
            public const string Rains = $"{nameof(Rain)}s";
            public const string Syss = $"{nameof(Sys)}s";
            public const string Winds = $"{nameof(Wind)}s";
        }

        public static class FilePaths
        {
            private static string currentDirectory = Directory.GetCurrentDirectory();
            private static string logsPath = Path.Combine(currentDirectory, "Logs".TrimStart('\\', '/'));
            private static IEnumerable<string> txtFiles = Directory.EnumerateFiles(logsPath, "*.txt");

            public static List<string> txtLogFiles = txtFiles.ToList();
        }

        public static class Keys
        {
            public const string AirPollutionModel = "AirPollutionModel";
            public const string CurrentWeatherModel = "CurrentWeatherModel";
            public const string DailyWeatherModel = "DailyWeatherModel";
        }

        public static class Urls
        {
        }
    }
}
