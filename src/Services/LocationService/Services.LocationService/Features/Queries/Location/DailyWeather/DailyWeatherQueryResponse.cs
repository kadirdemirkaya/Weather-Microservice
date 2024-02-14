namespace Services.LocationService.Features.Queries.Location.DailyWeather
{
    public record DailyWeatherQueryResponse(
        double Latitude,
        double Longitude
    );
}
