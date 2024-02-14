using BuildingBlock.Base.Abstractions;
using MediatR;
using Services.LocationService.Aggregate;
using Services.LocationService.Aggregate.ValueObjects;

namespace Services.LocationService.Features.Queries.Location.AirPollution
{
    public class AirPollutionQueryHandler : IRequestHandler<AirPollutionQueryRequest, AirPollutionQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AirPollutionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AirPollutionQueryResponse> Handle(AirPollutionQueryRequest request, CancellationToken cancellationToken)
        {
            City? city = await _unitOfWork.GetReadRepository<City, CityId>().GetAsync(c => c.CityName == request.city);
            return new(city.Latitude, city.Longitude);
        }
    }
}
