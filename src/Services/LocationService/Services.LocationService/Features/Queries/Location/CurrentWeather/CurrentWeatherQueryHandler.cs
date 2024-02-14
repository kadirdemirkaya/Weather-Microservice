using BuildingBlock.Base.Abstractions;
using MediatR;
using Services.LocationService.Aggregate;
using Services.LocationService.Aggregate.ValueObjects;

namespace Services.LocationService.Features.Queries.Location.CurrentWeather
{
    public class CurrentWeatherQueryHandler : IRequestHandler<CurrentWeatherQueryRequest, CurrentWeatherQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrentWeatherQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CurrentWeatherQueryResponse> Handle(CurrentWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            City? city = await _unitOfWork.GetReadRepository<City, CityId>().GetAsync(c => c.CityName == request.city);
            return new(city.Latitude, city.Longitude);
        }
    }
}
