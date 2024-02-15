using MediatR;

namespace Services.DataProcessService.Features.Queries.Daily.GetDailyWeatherInMemmory
{
    public record GetDailyWeatherInMemmoryQueryRequest (
        Models.Coord Coord
    ) : IRequest<GetDailyWeatherInMemmoryQueryResponse>;
}
