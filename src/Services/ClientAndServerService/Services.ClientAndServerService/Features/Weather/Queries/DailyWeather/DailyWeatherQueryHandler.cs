using AutoMapper;
using MediatR;
using Services.ClientAndServerService.Abstractions;
using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.Weather.Queries.DailyWeather
{
    public class DailyWeatherQueryHandler : IRequestHandler<DailyWeatherQueryRequest, DailyWeatherQueryResponse>
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public DailyWeatherQueryHandler(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }



        public async Task<DailyWeatherQueryResponse> Handle(DailyWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reply = await _locationService.GetDailyWeatherModelResponse(request.City);

                DailyWeatherDataModel dailyWeatherDataModel = new();
                dailyWeatherDataModel.DailyListModel = new();

                var dailyCity = _mapper.Map<Models.Daily.City>(reply.DailyWeatherDataModel.DailyCity);
                var cityCoord = _mapper.Map<Models.Daily.Coord>(reply.DailyWeatherDataModel.DailyCity.DailyCoord);
                dailyCity.coord = cityCoord;
                List<DailyListModel> dailyListModels = new List<DailyListModel>();
                foreach (var dListModel in reply.DailyWeatherDataModel.DailyListModel)
                {
                    var dailyList = _mapper.Map<Models.DailyListModel>(dListModel);
                    var dailyMain = _mapper.Map<Models.Daily.Main>(dListModel.DailyMain);
                    var dailyRain = _mapper.Map<Models.Daily.Rain>(dListModel.DailyRain);
                    var dailyCloud = _mapper.Map<Models.Daily.Cloud>(dListModel.DailyCloud);

                    dailyList.Cloud = dailyCloud;
                    dailyList.Main = dailyMain;
                    dailyList.Rain = dailyRain;

                    dailyList.DWeatherModels = new();
                    foreach (var dWeatherModel in dListModel.DailyWeatherModel)
                    {
                        var dailyWeatherModel = _mapper.Map<DWeatherModel>(dWeatherModel);

                        //dailyWeatherModels.Add(dailyWeatherModel);
                        dailyList.DWeatherModels.Add(dailyWeatherModel);
                    }

                    dailyListModels.Add(dailyList);
                }

                dailyWeatherDataModel.City = dailyCity;
                dailyWeatherDataModel.DailyListModel.AddRange(dailyListModels);

                return new(dailyWeatherDataModel);
            }
            catch (Exception)
            {
                return new(default);
            }
        }
    }
}
