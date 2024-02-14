namespace Services.LocationService.Features.Queries.Location.CurrentWeather
{
    public record CurrentWeatherQueryResponse(
        double Latitude,
        double Longitude
    );
}
