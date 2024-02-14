using BuildingBlock.Base.Abstractions;
using MediatR;
using Services.LocationService.Aggregate;
using Services.LocationService.Aggregate.ValueObjects;

namespace Services.LocationService.Features.Queries.Location.DailyWeather
{
    public class DailyWeatherQueryHandler : IRequestHandler<DailyWeatherQueryRequest, DailyWeatherQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DailyWeatherQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DailyWeatherQueryResponse> Handle(DailyWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            City? city = await _unitOfWork.GetReadRepository<City, CityId>().GetAsync(c => c.CityName == request.city);
            return new(city.Latitude, city.Longitude);
        }
    }
}
