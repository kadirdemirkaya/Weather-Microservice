using MediatR;
using Microsoft.Extensions.Hosting;
using Services.DataCaptureService.Features.Commands.Air.AirWeather;
using Services.DataCaptureService.Models;

namespace Services.DataCaptureService.Services.Background
{
    public class AirWeatherService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private int _count = 0;
        private readonly IMediator _mediator;
        public AirWeatherService(HttpClient httpClient, IMediator mediator)
        {
            _httpClient = httpClient;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //await SendRequest(Constant.Urls.airUrl);
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }

        private async Task SendRequest(string url)
        {
            if (_count == 0)
            {
                _count++;
                return;
            }
            if (_count < 2)
            {
                if (!string.IsNullOrEmpty(url))
                {
                    if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        await Console.Out.WriteLineAsync("Invalid URL: " + url);
                        _count++;
                        return;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                _count++;

                var getAndLots = GetLatAndLotModels();
                foreach (var latAndLotModel in getAndLots)
                {
                    double lat = latAndLotModel.Lat;
                    double lot = latAndLotModel.Lot;
                    string _url = $"http://api.openweathermap.org/data/2.5/air_pollution?lat{lat}=&lon{lot}=&appid={"APIKEY"}";
                    if (!Uri.TryCreate(_url, UriKind.Absolute, out Uri _uri))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        await Console.Out.WriteLineAsync("Invalid URL: " + url);
                        _count++;
                        return;
                    }
                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        await _mediator.Send(new AirWeatherCommandRequest(responseContent));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        await Console.Out.WriteLineAsync("Failed to get response from the server. Status code: " + response.StatusCode);
                    }
                }
                #region test code
                //string responseContent = Constant.Urls.exampleAirData;
                //await _mediator.Send(new AirWeatherCommandRequest(responseContent));
                //}
                #endregion
            }
        }

        public List<LatAndLotModel> GetLatAndLotModels()
        {
            List<LatAndLotModel> latAndLotModels = new();
            latAndLotModels.Add(new() { City = "Istanbul", Lat = 41.0138, Lot = 28.9497 });
            latAndLotModels.Add(new() { City = "Ankara", Lat = 39.925533, Lot = 32.866287 });
            latAndLotModels.Add(new() { City = "Izmir", Lat = 38.423733, Lot = 27.142826 });
            latAndLotModels.Add(new() { City = "Antalya", Lat = 36.9081, Lot = 30.6956 });
            return latAndLotModels;
        }
    }
}
