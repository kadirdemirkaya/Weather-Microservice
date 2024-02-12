using BuildingBlock.Base.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Services.LocationService.Setups
{
    public class FakeOptionsSetup : IConfigureOptions<FakeOptions>
    {
        private readonly IConfiguration _configuration;

        public FakeOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(FakeOptions options)
        {
            _configuration.GetSection("OriginalString").Bind(options);
        }
    }
}
