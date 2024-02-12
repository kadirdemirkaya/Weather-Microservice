using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using static Services.LocationService.Constants.Constant;

namespace Services.LocationService.Services.Background
{
    public class LogCleanupService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(1);

        public LogCleanupService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (i == 0)
                {
                    i++;
                    return;
                }
                Log.CloseAndFlush();
                try
                {
                    Log.Information("Clearing the Log File...");
                    if (FilePaths.txtLogFiles is not null)
                    {
                        foreach (var file in FilePaths.txtLogFiles)
                            File.Delete(file);
                        Log.Information("Log file cleared.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Error formed clearing the log file: {ex.Message}");
                }
                Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(_configuration).CreateLogger();
                i++;
                await Task.Delay(_cleanupInterval, stoppingToken);
            }
        }
    }
}
