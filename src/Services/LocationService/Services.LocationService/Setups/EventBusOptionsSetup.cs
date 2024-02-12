using BuildingBlock.Base.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Services.LocationService.Setups
{
    public class EventBusOptionsSetup : IConfigureOptions<EventBusOptions>
    {
        private readonly IConfiguration _configuration;
        public void Configure(EventBusOptions options)
        {
            _configuration.GetSection(nameof(EventBusOptions)).Bind(options);
        }
    }
}
